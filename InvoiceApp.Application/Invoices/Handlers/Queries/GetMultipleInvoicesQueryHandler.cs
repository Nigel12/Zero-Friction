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

public class GetMultipleInvoicesQueryHandler : IRequestHandler<GetMultipleInvoicesQuery, IEnumerable<Invoice>>
{
    private IInvoiceService _invoiceService;

    public GetMultipleInvoicesQueryHandler(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task<IEnumerable<Invoice>> Handle(GetMultipleInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceService.GetMultipleAsync(request.Query);
        return invoices;
    }
}