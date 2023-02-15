using Azure.Core;
using InvoiceApp.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using InvoiceApp.Domain.Interfaces;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Entities;
using MediatR;
using InvoiceApp.Application.Invoices.Commands;
using InvoiceApp.Application.Invoices.Queries;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

namespace InvoiceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    //private IInvoiceService _invoiceService;
    private readonly ISender _mediator;

    public InvoiceController(ISender mediator)
    {
        //_invoiceService = invoiceService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoices()
    {
        var response = await _mediator.Send(new GetAllInvoicesQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(string id)
    {
        var response = await _mediator.Send(new GetInvoiceByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice(InvoiceRequest request)
    {
        var invoice = ConvertRequestToInvoice(request);
        var addedInvoice = await _mediator.Send(new CreateInvoiceCommand(invoice));
        return Ok(addedInvoice);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceRequest request)
    {
        var invoice = ConvertRequestToInvoice(request, request.Id);
        await _mediator.Send(new UpdateInvoiceCommand(invoice));
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(string id)
    {
        await _mediator.Send(new DeleteInvoiceCommand(id));
        return NoContent();
    }

    [HttpGet("query/{query}")]
    public async Task<IActionResult> QueryInvoices(string? query = "select * from c")
    {
        var response = await _mediator.Send(new GetMultipleInvoicesQuery(query));
        return Ok(response);
    }


    private Invoice ConvertRequestToInvoice(InvoiceRequest request, string? existingId = null)
    {
        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
        foreach (var item in request.Items)
        {
            invoiceItems.Add(new InvoiceItem(InvoiceItemId.CreateUnique(), item.Quantity, item.UnitPrice));
        }
        var totalAmount = invoiceItems.Sum(x => x.Amount);

        if (string.IsNullOrEmpty(existingId))
        {
            return new Invoice(InvoiceId.CreateUnique(), request.Description, totalAmount, invoiceItems);
        }
        return new Invoice(InvoiceId.CreateExisting(existingId), request.Description, totalAmount, invoiceItems);
    }
}

// chrome://flags/#allow-insecure-localhost