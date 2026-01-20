using System.Data.Common;

namespace InvoiceSystem;
public class ProductQuantityService()
{
    private List<ProductQuantity> _products = DataPersistence.LoadQuantities();

    private int NextId(List<ProductQuantity> products)
    {
        if (products.Count == 0) return 1;

        int maxId = 0;
        foreach (ProductQuantity product in products)
        {
            if (product.Id > maxId) maxId = product.Id;
        }
        return maxId + 1;
    }

    public List<ProductQuantity> ListProducts()
    {
        return _products;
    }

    public ProductQuantity CreateProduct(Product product, int quantity)
    {
        int id = NextId(_products);
        ProductQuantity productq = new(id,product,quantity);
        _products.Add(productq);
        DataPersistence.SaveQuantities(_products);
        return productq;
    }

    public ProductQuantity ModifyProduct(ProductQuantity productq, Product product, int quantity)
    {
        int productIndex = _products.IndexOf(productq);
        _products[productIndex].Product = product;
        _products[productIndex].Quantity = quantity;
        DataPersistence.SaveQuantities(_products);

        return _products[productIndex];
    }

    public bool DeleteProduct(ProductQuantity product)
    {
        bool removed = _products.Remove(product);
        if(removed)
        {
            DataPersistence.SaveQuantities(_products);
        }
        return removed;
    }

    public ProductQuantity? FindById(int id)
    {
        foreach(ProductQuantity product in _products)
        {
            if(product.Id==id)
            {
                return product;
            }
        }
        return null;
    }
}