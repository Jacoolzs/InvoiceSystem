namespace InvoiceSystem;
public class ProductQuantity(int id, Product? product, int quantity)
{
    public ProductQuantity() : this(0,null,0)
    {
        
    }
    public int Id {get; set;} = id;
    public Product? Product {get; set;} = product;
    private int _quantity = quantity >=0 ? quantity : throw new Exception("ERR: Quantity cannot be less than zero!");

    public int Quantity
    {
        get => _quantity;
        set
        {
            if(value<0)
            {
                throw new Exception("ERR: Quantity cannot be less than zero!");
            }
            _quantity = value;
        }
    }
}