namespace InvoiceApp.Contracts;

public class InvoiceItemRequest
{
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}
