﻿using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data;

public class EcommerceContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Size> Sizes { get; set; }
    public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
    public virtual DbSet<SuperCategoryProduct> SuperCategoryProducts { get; set; }
    public virtual DbSet<ProductImages> ProductImages { get; set; }
    public virtual DbSet<Basket> Baskets { get; set; }
    public virtual DbSet<BasketItems> BasketItems { get; set; }

    public virtual DbSet<SizeStock> SizeStocks { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<AddressCustomer> AddressCustomers { get; set; }
    public virtual DbSet<UserAddress> UsersAdresses { get; set; }

    public EcommerceContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Product>().HasKey(product => product.Id);
        modelBuilder.Entity<CategoryProduct>().HasKey(categoryProduct => categoryProduct.Id);
        modelBuilder.Entity<SuperCategoryProduct>().HasKey(superCategoryProduct => superCategoryProduct.Id);
        modelBuilder.Entity<Size>().HasKey(size => size.Id);
        modelBuilder.Entity<Role>().HasKey(role => role.Id);
        modelBuilder.Entity<UserRole>().HasKey(userRole =>
            new
            {
                userRole.UserId,
                userRole.RoleId,
            });
        modelBuilder.Entity<ProductImages>().HasKey(productImages => productImages.Id);
        modelBuilder.Entity<SizeStock>().HasKey(sizeStock => sizeStock.Id);
        modelBuilder.Entity<Basket>().HasKey(basket => basket.Id);
        modelBuilder.Entity<BasketItems>().HasKey(basketItem => basketItem.Id);
        modelBuilder.Entity<Order>().HasKey(order => order.Id);
        modelBuilder.Entity<UserAddress>().HasKey(userAddress => userAddress.Id);



        // Relationships table User,Role,UserRole
        modelBuilder.Entity<UserRole>()
            .HasOne<User>(userRole => userRole.User)
            .WithMany(user => user.Roles)
            .HasForeignKey(userRole => userRole.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne<Role>(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);


        //Relationships table Product,CategoryProduct
        modelBuilder.Entity<Product>(entity => entity.HasOne<CategoryProduct>(product => product.CategoryProduct));

        //Relationships table Product,SuperCategoryProduct
        modelBuilder.Entity<Product>(entity => entity.HasOne<SuperCategoryProduct>(product => product.SuperCategoryProduct));

        //Relationships table Product,Size,SizeStock
        modelBuilder.Entity<SizeStock>(entity =>
        {
            entity.HasOne(sizStock => sizStock.Size)
                .WithMany(size => size.SizesStocks)
                .HasForeignKey(sizeStock => sizeStock.FK_SizeId);

            entity.HasOne(sizeStock => sizeStock.Product)
                .WithMany(product => product.SizeStocks)
                .HasForeignKey(sizeStock => sizeStock.FK_ProductId);
        });

        modelBuilder.Entity<ProductImages>(entity =>
        {
            entity.HasOne(productImage => productImage.Product)
                .WithMany(product => product.ProductImages)
                .HasForeignKey(productImage => productImage.FK_ProductId);
        });

        modelBuilder.Entity<Basket>(entity =>
        entity.HasMany<BasketItems>(basket => basket.Items)
        .WithOne(basket => basket.Basket)
        .HasForeignKey(basketItems => basketItems.BasketId)
        .OnDelete(DeleteBehavior.Cascade)
        );

        //Relationships table Invoice,AddressCustomer
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne<User>(user => user.User);
        });

        //Relationships table Users, UsersAddresses
        // modelBuilder.Entity<UserAddress>(entity =>
        // entity.HasOne<User>(user => user.User));

        // Relationships table User,UserAddress
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(user => user.UserAddresses)
            .WithOne(address => address.User)
            .HasForeignKey(address => address.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        });
    }
}