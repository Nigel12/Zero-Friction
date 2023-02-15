using InvoiceApp.Application.Invoices.Commands;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using InvoiceApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Invoices.Handlers.Commands;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Invoice>
{
    private IInvoiceService _invoiceService;

    public UpdateInvoiceCommandHandler(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }
    public async Task<Invoice> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        await _invoiceService.UpdateAsync(request.Invoice);
        return request.Invoice;
    }
}
