using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Contracts;
using Ecommerce.API.Models;
using Newtonsoft.Json.Linq;

namespace Ecommerce.API.Services;
public class InvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressCustomerRepository _addressCustomerRepository;


    public InvoiceService(IInvoiceRepository invoiceRepository, IUserRepository userRepository, IAddressCustomerRepository addressCustomerRepository)
    {
        this._invoiceRepository = invoiceRepository;
        this._userRepository = userRepository;
        this._addressCustomerRepository = addressCustomerRepository;
    }

    public async Task<Invoice> CreateNewInvoice(RequestRegisterInvoice requestRegisterInvoice)
    {
        var objectDestructured = JObject.Parse(requestRegisterInvoice.DataAddress.ToString());
        var userExist = await this._userRepository.GetUserByEmailAsync(objectDestructured["email"].ToString());
        var addressExist = await this._addressCustomerRepository.GetAddressCustomerByUserIdAsync(userExist.Id);
        
        // AddressCustomer newAddressCustomer = new AddressCustomer()
        // { City = objectDestructured["city"].ToString(), State = objectDestructured["country"].ToString(), Notes = objectDestructured["notes"].ToString(), ZipCode = objectDestructured["zipCode"].ToString(), User = userExist is not null ? userExist : null };


        Invoice newInvoice = new() { User = userExist ,AddressCustomerId=userExist.Id};

        var newInvoiceCreated = await this._invoiceRepository.CreateInvoiceAsync(newInvoice);

        return newInvoiceCreated;
    }
}