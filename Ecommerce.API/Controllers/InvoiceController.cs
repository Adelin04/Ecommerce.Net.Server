using Ecommerce.API.Services;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]

public class InvoiceController : ControllerBase
{
    private InvoiceController _invoiceController;
    private ILogger<InvoiceController> Logger;

    public InvoiceController(InvoiceController invoiceController, ILogger<InvoiceController> logger)
    {
        this._invoiceController = invoiceController;
        this.Logger = logger;
    }

    [HttpPost("datainvoice")]
    public async Task<ActionResult> Invoice(RequestRegisterInvoice requestRegisterInvoice)
    {
        System.Console.WriteLine("-------------------------------------------" + requestRegisterInvoice);
        return Ok(new { Success = true });
    }
}