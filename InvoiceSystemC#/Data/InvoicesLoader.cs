using System.Text.Json;

namespace InvoiceSystem;

public static class DataPersistence
{
    // Nombres de archivos para cada entidad
    private const string ProductsFile = "Data/products.json";
    private const string InvoicesFile = "Data/invoice.json";
    private const string QuantitiesFile = "Data/product_quantities.json";

    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static void SaveProducts(List<Product> products) 
        => File.WriteAllText(ProductsFile, JsonSerializer.Serialize(products, Options));

    public static List<Product> LoadProducts() 
        => File.Exists(ProductsFile) 
            ? JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(ProductsFile)) ?? new() 
            : new();

    public static void SaveInvoices(List<Invoice> invoices) 
        => File.WriteAllText(InvoicesFile, JsonSerializer.Serialize(invoices, Options));

    public static List<Invoice> LoadInvoices() 
        => File.Exists(InvoicesFile) 
            ? JsonSerializer.Deserialize<List<Invoice>>(File.ReadAllText(InvoicesFile)) ?? new() 
            : new();

    public static void SaveQuantities(List<ProductQuantity> quantities) 
        => File.WriteAllText(QuantitiesFile, JsonSerializer.Serialize(quantities, Options));

    public static List<ProductQuantity> LoadQuantities() 
        => File.Exists(QuantitiesFile) 
            ? JsonSerializer.Deserialize<List<ProductQuantity>>(File.ReadAllText(QuantitiesFile)) ?? new() 
            : new();
}