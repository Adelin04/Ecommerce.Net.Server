using Ecommerce.API.Services;
using Ecommerce.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]

public class InvoiceController : ControllerBase
{

    private InvoiceService _invoiceService;
    private ILogger<InvoiceController> Logger;

    public InvoiceController(InvoiceService invoiceService, ILogger<InvoiceController> logger)
    {
        this._invoiceService = invoiceService;
        this.Logger = logger;
    }

    [HttpPost("datainvoice")]
    public async Task<ActionResult> CreateNewInvoice(RequestRegisterInvoice requestRegisterInvoice)
    {
        System.Console.WriteLine("-------------------------------------------" + requestRegisterInvoice);
        return Ok(new { Success = true });
    }
}