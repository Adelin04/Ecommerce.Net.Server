﻿using Ecommerce.API.Config;
using Ecommerce.API.Contracts;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Models;

namespace Ecommerce.API.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryProductRepository _categoryProductRepository;
    private readonly ISuperCategoryProductRepository _superCategoryProductRepository;
    private readonly ISizeStockRepository _sizeStockRepository;
    private readonly ISizeRepository _sizeRepository;
    private readonly IConfiguration _configuration;
    private readonly AwsS3StorageImagesService _imagesService;
    private readonly ProductImagesService _productImagesService;


    public ProductService(IProductRepository productRepository, ICategoryProductRepository categoryProductRepository, ISuperCategoryProductRepository superCategoryProductRepository,
        ISizeStockRepository sizeStockRepository, ISizeRepository sizeRepository, IConfiguration configuration,
        ProductImagesService productImagesService,
        AwsS3StorageImagesService imagesService)
    {
        this._productRepository = productRepository;
        this._categoryProductRepository = categoryProductRepository;
        this._superCategoryProductRepository = superCategoryProductRepository;
        this._sizeStockRepository = sizeStockRepository;
        this._sizeRepository = sizeRepository;
        this._configuration = configuration;
        this._productImagesService = productImagesService;
        this._imagesService = imagesService;
    }

    public async Task<Product?> CreateNewProduct_ServiceAsync(ProductDataRegister productDataRegister)
    {
        const string AWS_BASE_URL_PHOTO = " https://e-commerce-photos.s3.amazonaws.com/";
        Product? newProduct = null;
        List<SizeStock> TMP_listOfSize = new();

        var existCategoryProduct =
            await this._categoryProductRepository.GetCategoryProductByNameAsync(productDataRegister.CategoryName);
        var existSuperCategoryProduct =
            await this._superCategoryProductRepository.GetSuperCategoryProductByNameAsync(productDataRegister.SuperCategoryName);

        var findSizesProduct = await this._sizeRepository.GetAllSizeAsync();

        if (existCategoryProduct is null) return null;

        //  Create the new product
        newProduct = new Product();
        newProduct.Name = productDataRegister.Name;
        newProduct.Brand = productDataRegister.Brand;
        newProduct.Color = productDataRegister.Color;
        newProduct.Description = productDataRegister.Description;
        newProduct.Price = productDataRegister.Price;
        newProduct.ProductCode = productDataRegister.ProductCode;

        //  Add FOREIGNKEY category product
        newProduct.CategoryProductId = existCategoryProduct.Id;
        newProduct.SuperCategoryProductId = existSuperCategoryProduct.Id;

        System.Console.WriteLine(newProduct.CategoryProductId);
        System.Console.WriteLine(newProduct.SuperCategoryProductId);

        var newProductCreated = await this._productRepository.CreateNewProductAsync(newProduct);

        // register size and stock for the new product
        if (newProductCreated is null) return null;
        var verifySizeExist = findSizesProduct.Find(item => item.Name == productDataRegister.Size);

        if (verifySizeExist is null) return null;
        TMP_listOfSize.Add(new SizeStock { Stock = productDataRegister.stock, FK_ProductId = newProductCreated.Id, FK_SizeId = verifySizeExist.Id });

        var newListOfSizeStockRegistered = await this._sizeStockRepository.RegisterListOfNewSizeStockAsync(TMP_listOfSize);

        // submit new product photos to aws S3
        if (newListOfSizeStockRegistered is not null)
        {
            foreach (var file in productDataRegister.Files)
            {
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                // var fileExtention = Path.GetExtension(file.FileName);

                var s3Obj = new S3Object()
                {
                    BucketName = "e-commerce-photos",
                    InputStream = memoryStream,
                    Name = $"{file.FileName}"
                };

                var credentials = new AwsCredentials()
                {
                    AccessKey = this._configuration["AwsConfiguration:AWSAccessKey"],
                    SecretKey = this._configuration["AwsConfiguration:AWSSecretKey"]
                };

                var AWSresult = await this._imagesService.UploadFileAsync(s3Obj, credentials);

                if (AWSresult is not null)
                {
                    ProductImagesDataRegister newImages = new($"{AWS_BASE_URL_PHOTO}{file.FileName}", newProductCreated.Id);
                    await this._productImagesService.CreateNewProductImages_ServiceAsync(newImages);
                }
            }
        }

        return newProduct;
    }

    public async Task<Product?> GetProductById_ServiceAsync(long id) => await this._productRepository.GetProductByIdAsync(id);


    public async Task<List<Product>?> GetAllProducts_ServiceAsync() => await this._productRepository.GetAllProductsAsync();

    public async Task<Product> UpdateProductById_ServiceAsync(long id, ProductDataUpdate productDataUpdate) => await this._productRepository.UpdateProductByIdAsync(id, productDataUpdate);

    public async Task<Product> DeleteProductById_ServiceAsync(long id) => await this._productRepository.DeleteProductByIdAsync(id);

}