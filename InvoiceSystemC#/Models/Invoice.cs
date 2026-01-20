namespace InvoiceSystem;
public class Invoice(int id, string invoiceName, List<ProductQuantity> products)
{
    public int Id {get; set;} = id;
    public string InvoiceName {get; set;} = invoiceName;
    public List<ProductQuantity> Products {get; set;} = products;

    public decimal CalculateSubtotal()
    {
        decimal SubTotal = 0m;
        foreach(ProductQuantity ProductInvoice in Products)
        {
            SubTotal+= ProductInvoice.Product.Price*ProductInvoice.Quantity;
        }
        return SubTotal;
    }

    public decimal CalculateTotalTax()
    {
        decimal TotalTax = 0m;
        foreach(ProductQuantity ProductInvoice in Products)
        {
            TotalTax+= ProductInvoice.Product.CalculateTax()*ProductInvoice.Quantity;
        }

        return TotalTax;
    }

    public decimal CalculateTotalInvoice()
    {
        decimal Total = CalculateSubtotal()+CalculateTotalTax();
        return Total;
    }
}