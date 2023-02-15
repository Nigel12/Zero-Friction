using InvoiceApp.Application.Invoices.Queries;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using InvoiceApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Invoices.Handlers.Queries;

public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, Invoice>
{
    private IInvoiceService _invoiceService;

    public GetInvoiceByIdQueryHandler(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task<Invoice> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceService.GetAsync(request.Id);
        return invoice;
    }
}
