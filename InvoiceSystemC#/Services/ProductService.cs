namespace InvoiceSystem;
public class ProductService()
{
    private List<Product> _products = DataPersistence.LoadProducts();

    private int NextId(List<Product> products)
    {
        if (products.Count == 0) return 1;

        int maxId = 0;
        foreach (Product product in products)
        {
            if (product.Id > maxId) maxId = product.Id;
        }
        return maxId + 1;
    }
    public Product CreateProduct(string name, decimal price, string description, bool applyTax)
    {
        
        int id = NextId(_products);
        Product product = new(id, name,price,description,applyTax);
        _products.Add(product);
        DataPersistence.SaveProducts(_products);
        return product;
    }

    public List<Product> ListProducts()
    {
        return _products;
    }

    public Product? ModifyProduct(Product product, string name, decimal price, string description, bool applyTax)
    {
        int productIndex = _products.IndexOf(product);

        if(productIndex==-1)
        {
            return null;
        }

        _products[productIndex].Name = name;
        _products[productIndex].Price = price;
        _products[productIndex].Description = description;
        _products[productIndex].ApplyTax = applyTax;
        DataPersistence.SaveProducts(_products);
        return _products[productIndex];
    }

    public bool DeleteProduct(Product product)
    {
        bool removed = _products.Remove(product);
        
        if (removed)
        {
            DataPersistence.SaveProducts(_products);
        }
        
        return removed;
    }
    public Product? FindById(int id)
    {
        foreach(Product product in _products)
        {
            if(product.Id==id)
            {
                return product;
            }
        }
        return null;
    }
}