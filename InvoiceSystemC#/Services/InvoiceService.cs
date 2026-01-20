namespace InvoiceSystem;
public class InvoiceService()
{
    private List<Invoice> _invoices = DataPersistence.LoadInvoices();

    private int NextId(List<Invoice> invoices)
    {
        if (invoices.Count == 0) return 1;

        int maxId = 0;
        foreach (Invoice invoice in invoices)
        {
            if (invoice.Id > maxId) maxId = invoice.Id;
        }
        return maxId + 1;
    }
    
    public Invoice CreateInvoice(string invoiceName, List<ProductQuantity> products)
    {
        int id = NextId(_invoices);
        Invoice invoice = new(id,invoiceName,products);
        _invoices.Add(invoice);
        DataPersistence.SaveInvoices(_invoices);
        return invoice;
    }

    public Invoice? ModifyInvoice(Invoice invoice, string name, List<ProductQuantity> products)
    {
        int invoiceIndex = _invoices.IndexOf(invoice);
        if(invoiceIndex == -1)
        {
            return null;
        }

        _invoices[invoiceIndex].InvoiceName = name;
        _invoices[invoiceIndex].Products = products;

        DataPersistence.SaveInvoices(_invoices);
        return _invoices[invoiceIndex];
    }

    public bool DeleteInvoice(Invoice invoice)
    {
        bool removed = _invoices.Remove(invoice);
        if (removed)
        {
            DataPersistence.SaveInvoices(_invoices);
        }
        return removed;
    }

    public List<Invoice> ListInvoices()
    {
        return _invoices;
    }

    public Invoice? FindById(int id)
    {
        foreach(Invoice invoice in _invoices)
        {
            if(invoice.Id==id)
            {
                return invoice;
            }
        }
        return null;
    }
}