﻿using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Invoices.Queries;

public record GetInvoiceByIdQuery(string Id) : IRequest<Invoice>;
