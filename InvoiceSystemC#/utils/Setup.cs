namespace InvoiceSystem;
public class Setup
{
    public MainMenu mainMenu;
    public ProductService productService;
    public InvoiceService invoiceService;
    public ProductQuantityService productQuantityService;
    public InvoiceController invoiceController;
    public ProductController productController;

    public Setup()
    {
        mainMenu = new MainMenu();
        productService = new ProductService();
        invoiceService = new InvoiceService();
        productQuantityService = new ProductQuantityService();
        invoiceController = new InvoiceController(invoiceService, productService, productQuantityService);
        productController = new ProductController(productService);
    }
    
}