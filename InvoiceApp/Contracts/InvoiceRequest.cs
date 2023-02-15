namespace InvoiceApp.Contracts;

public class InvoiceRequest
{
    public string Description { get; set; }
    public List<InvoiceItemRequest> Items { get; set; }
}
