using InvoiceApp.Application.Invoices.Commands;
using InvoiceApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Invoices.Handlers.Commands;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
{

    private IInvoiceService _invoiceService;

    public DeleteInvoiceCommandHandler(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        await _invoiceService.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
