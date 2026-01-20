namespace InvoiceSystem;
public class ProductController(ProductService productService)
{
    public void HandleProductMenu()
    {
        ProductMenu menuProduct = new ProductMenu();
        Console.Clear();
        int opcion = menuProduct.Show();
        switch (opcion)
        {
            case 1:
                Console.Clear();
                CreateProduct();
                Console.ReadKey();
                Console.Clear();
                break;
            case 2:
                Console.Clear();
                ModifyProducts();
                Console.ReadKey();
                Console.Clear();
                break;
            case 3:
                Console.Clear();
                DeleteProducts();
                Console.ReadKey();
                Console.Clear();
                break;
            case 4:
                Console.Clear();
                ListProducts();
                Console.ReadKey();
                Console.Clear();
                break;
            case 5:
                Console.Clear();
                break;
            default:
                System.Console.WriteLine("Elija una opcion valida!");
                break;
        }
    }

    public void ModifyProducts()
    {
        string newName;
        decimal newPrice;
        string newPriceToParse;
        string newDescription;
        bool applyTax;
        string newTax;
        int resultParseTax;

        int selectedProduct;
        int productIndex = 0;

        List<Product> products = productService.ListProducts();

        foreach(Product productToModify in products)
        {
            productIndex++;
            System.Console.WriteLine($"{productIndex}- {productToModify.Name}: {productToModify.Price}");
        }

        System.Console.WriteLine("0- Cancelar");
        System.Console.WriteLine("\nSeleccione el ID del producto a modificar!");

        while(!int.TryParse(Console.ReadLine(),out selectedProduct) || selectedProduct>products.Count || selectedProduct <0)
        {
            System.Console.WriteLine("Introduzca una opcion valida!");
        }

        if(selectedProduct-1==-1)
        {
            return;
        }

        else
        {
            selectedProduct = selectedProduct-1;
            System.Console.WriteLine($"El objeto a editar sera: {products[selectedProduct].Name}");

            System.Console.WriteLine("Introduzca el nuevo nombre (Presione 'Enter' para no cambiar.)");
            newName = Console.ReadLine();
            newName = string.IsNullOrWhiteSpace(newName) ? products[selectedProduct].Name : newName;

            System.Console.WriteLine("Introduzca el nuevo precio (Presione 'Enter' para no cambiar.)");

            newPriceToParse = Console.ReadLine();

            while(true)
            {
                if(string.IsNullOrWhiteSpace(newPriceToParse))
                {
                    newPrice = products[selectedProduct].Price;
                    break;
                }

                if(decimal.TryParse(newPriceToParse, out newPrice))
                {
                    break;
                }
                System.Console.WriteLine("ERR: Precio invalido.\nIntroduzca el nuevo precio (Presione 'Enter' para no cambiar.)");

            }
            newName = string.IsNullOrWhiteSpace(newName) ? products[selectedProduct].Name : newName;

            System.Console.WriteLine("Introduzca la nueva descripcion (Presione 'Enter' para no cambiar.)");
            newDescription = Console.ReadLine();
            newDescription = string.IsNullOrWhiteSpace(newDescription) ? products[selectedProduct].Description : newDescription;

            System.Console.WriteLine(
            """
            Desea que se le aplique ITBIS al producto? (Presione "Enter" para no cambiar)

            1- Si
            2- No
            """);
            newTax = Console.ReadLine();

            while(true)
            {
                if(string.IsNullOrWhiteSpace(newTax))
                {
                    applyTax = products[selectedProduct].ApplyTax;
                    break;
                }
                else
                {
                    if(int.TryParse(newTax, out resultParseTax) && (resultParseTax==1 || resultParseTax==2))
                    {
                        if(resultParseTax == 1)
                        {
                            applyTax = true;
                            break;
                        }
                        else
                        {
                            applyTax = false;
                            break;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Manteniendo estado anterior...");
                        applyTax = products[selectedProduct].ApplyTax;
                        break;
                    }
                }
            }
            
            productService.ModifyProduct(products[selectedProduct],newName,newPrice,newDescription,applyTax);
            System.Console.WriteLine("El producto ha sido editado exitosamente!");
        }
    }
    public void ListProducts()
    {
        List<Product> products = productService.ListProducts();
        System.Console.WriteLine("Productos: \n");
        int productIndex = 0;
        foreach(Product productsCreated in products)
        {
            string description = string.IsNullOrWhiteSpace(productsCreated.Description) ? "" : productsCreated.Description;
            productIndex++;
            System.Console.WriteLine($"\n{productIndex}- {productsCreated.Name} : {productsCreated.Price}$DOP.\n{description}");
        }
        System.Console.WriteLine("Presione una tecla para continuar...");
    }
    public void DeleteProducts()
    {
        int selectedProduct;
        int productIndex = 0;
        bool deletedProduct;

        List<Product> products = productService.ListProducts();

        foreach(Product productToModify in products)
        {
            productIndex++;
            System.Console.WriteLine($"{productIndex}- {productToModify.Name}: {productToModify.Price}");
        }

        System.Console.WriteLine("0- Cancelar");
        System.Console.WriteLine("\nSeleccione el ID del producto a modificar!");

        while(!int.TryParse(Console.ReadLine(),out selectedProduct) || selectedProduct>products.Count || selectedProduct <0)
        {
            System.Console.WriteLine("Introduzca una opcion valida!");
        }

        if(selectedProduct-1==-1)
        {
            return;
        }

        else
        {
            selectedProduct = selectedProduct-1;
            System.Console.WriteLine($"El objeto a eliminar sera: {products[selectedProduct].Name}");
            deletedProduct = productService.DeleteProduct(products[selectedProduct]);
            if(deletedProduct)
            {
                System.Console.WriteLine("Producto eliminado exitosamente");
            }
            else
            {
                System.Console.WriteLine("No se ha podido eliminar el producto.");
            }
        }
    }
    public void CreateProduct()
    {
        string name = "";
        decimal price = 0m;
        string? description = "";
        bool applyTax = false;
        int taxTrueOrFalse;

        Console.Clear();
        
        System.Console.WriteLine("Introduzca el nombre del producto: ");
        
        name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "El nombre del producto no puede estar vacio");
        }

        System.Console.WriteLine("Introduzca el precio del producto: ");
        while(!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
        {
            System.Console.WriteLine("Introduzca un precio valido!");
        }

        System.Console.WriteLine("Introduzca una descripcion para el producto (Opcional): ");
        description = Console.ReadLine();
        
        System.Console.WriteLine(
        """
        Al producto se le debe aplicar impuestos?

        1- Si
        2- No

        """);

        while(!int.TryParse(Console.ReadLine(), out taxTrueOrFalse) || (taxTrueOrFalse != 1 && taxTrueOrFalse != 2))
        {
            System.Console.WriteLine("Introduzca una opcion valida! ");
        }

        if(taxTrueOrFalse == 1)
        {
            applyTax = true;
        }

        Product newProduct = productService.CreateProduct(name,price,description,applyTax);
        System.Console.WriteLine($"Producto creado con exito: {newProduct.Name}");
    }
}