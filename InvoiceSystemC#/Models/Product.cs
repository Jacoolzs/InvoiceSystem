namespace InvoiceSystem;
public class Product(int id, string name, decimal price, string description, bool applyTax)
{
    public Product() : this(0,"",0m,"",false)
    {
    }
    public int Id {get; set;} = id;
    private decimal _price = price >= 0 ? price : throw new Exception("ERR: Price cannot be less than zero");
    public string Name {get; set;} = name;
    public string ?Description {get; set;} = description;
    public bool ApplyTax {get; set;} = applyTax;
    public decimal Price
    {
        get => _price;
        set
        {
            if(value<0)
            {
                throw new Exception ("ERR: Price cannot be less than zero!");
            }
            _price = value;
        }
    }

    public decimal CalculateTax()
    {
        if(ApplyTax)
        {
            return Price*0.18m;
        }
        else
        {
            return 0m;
        }
    }
}