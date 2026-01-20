namespace InvoiceSystem;
public class InvoiceController(InvoiceService invoiceService, ProductService productService, ProductQuantityService productQuantityService)
{
    public void HandleInvoiceMenu()
    {
        InvoiceMenu menuInvoice = new InvoiceMenu();
        Console.Clear();
        int opcion = menuInvoice.Show();
        switch (opcion)
        {
            case 1:
                Console.Clear();
                CreateInvoice();
                Console.ReadKey();
                Console.Clear();
                break;
            case 2:
                Console.Clear();
                ModifyInvoice();
                Console.ReadKey();
                Console.Clear();
                break;
            case 3:
                Console.Clear();
                DeleteInvoice();
                Console.ReadKey();
                Console.Clear();
                break;
            case 4:
                Console.Clear();
                ListInvoices();
                Console.Clear();
                System.Console.WriteLine("Se han acabado las facturas! Presione una tecla para continuar...");
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

    public void ListInvoices()
    {
        Console.Clear();
        List<Invoice> Invoices = invoiceService.ListInvoices();

        if(Invoices.Count==0)
        {
            System.Console.WriteLine("No hay facturas para mostrar!");
            return;
        }

        foreach(Invoice invoice in Invoices)
        {
            Console.Clear();
            System.Console.WriteLine(
                $"""
                ========================================================
                =Nombre de la factura {invoice.InvoiceName}=

                Productos:

                """
            );
            foreach(ProductQuantity product in invoice.Products)
            {
                System.Console.WriteLine($"{product.Product.Name}: {product.Quantity} - {product.Product.Price*product.Quantity}");
            }
            System.Console.WriteLine("\n===TOTAL A PAGAR DE LA FACTURA===");
            Console.WriteLine($"ITBIS: {invoice.CalculateTotalTax()}");
            Console.WriteLine($"SUBTOTAL: {invoice.CalculateSubtotal()}");
            Console.WriteLine($"TOTAL: {invoice.CalculateTotalInvoice()}");
            System.Console.WriteLine("========================================================");
            System.Console.WriteLine("");

            System.Console.WriteLine("Presione una tecla para ver la siguiente factura!");
            Console.ReadKey();
        }
    }
    public void CreateInvoice()
    {
        Console.Clear();
        List<Product> productsCreated = productService.ListProducts();
        ProductQuantity createdProduct;
        List<ProductQuantity> selectedProducts = new();
        string invoiceName;
        int productSelected;
        int quantity;
        int finishSelection;
        bool allProductSelected = false;
        int productNumber = 0;

        Console.Clear();

        if(productsCreated.Count==0)
            {
                System.Console.WriteLine("No hay productos registrados, registre alguno primero para inciar!");
                return;
            }

        System.Console.WriteLine("Introduzca el nombre de la factura: ");
        invoiceName = Console.ReadLine();

        while(!allProductSelected)
        {
            productNumber = 0;
            System.Console.WriteLine("Seleccione el producto que desea agregar: ");
            foreach(Product productCreated in productsCreated)
            {
                productNumber++;
                System.Console.WriteLine($"{productNumber}- {productCreated.Name}");
            }

            while(!int.TryParse(Console.ReadLine(), out productSelected) || productSelected-1<0 || productSelected>productsCreated.Count)
            {
                System.Console.WriteLine("Introduzca una opcion valida!");
            }
            productSelected = productSelected-1;
            System.Console.WriteLine("Introduzca la cantidad que desea agregar: ");
            while(!int.TryParse(Console.ReadLine(), out quantity))
            {
                System.Console.WriteLine("Introduzca un valor valido!");
            }
            
            createdProduct = productQuantityService.CreateProduct(productsCreated[productSelected],quantity);
            selectedProducts.Add(createdProduct);

            System.Console.WriteLine(
                """
                Desea agregar otro producto?

                1- Si
                2- No
                """
            );

            while(!int.TryParse(Console.ReadLine(), out finishSelection) || finishSelection>2 || finishSelection<1)
            {
                System.Console.WriteLine("Introduzca una opcion valida!");
            }

            if(finishSelection == 2)
            {
                allProductSelected = true;
            }
        }
        

        Invoice newInvoice = invoiceService.CreateInvoice(invoiceName,selectedProducts);
        System.Console.WriteLine($"Factura creada con exito: {newInvoice.InvoiceName}");
    }

    public void ModifyInvoice()
    {
        List<Invoice> invoices = invoiceService.ListInvoices();
        List<ProductQuantity> products = productQuantityService.ListProducts();
        int invoiceIndex = 0;
        int invoiceToModify;
        string newInvoiceName;
        int productModifyOption;
        

        foreach(Invoice invoice in invoices)
        {
            invoiceIndex++;
            System.Console.WriteLine($"{invoiceIndex}- {invoice.InvoiceName}");
        }

        System.Console.WriteLine("0- Cancelar");
        System.Console.WriteLine("\nSeleccione el ID de la factura a modificar!");

        while(!int.TryParse(Console.ReadLine(), out invoiceToModify) || invoiceToModify < 0 || invoiceToModify > invoices.Count)
        {
            System.Console.WriteLine("Introduzca una opcion valida!");
        }

        if(invoiceToModify-1==-1)
        {
            return;
        }

        invoiceToModify = invoiceToModify-1;
        System.Console.WriteLine($"Factura seleccionada: {invoices[invoiceToModify].InvoiceName}");
        System.Console.WriteLine(
            $"""
            ========================================================
            Nombre de la factura {invoices[invoiceToModify].InvoiceName}
            ID de la factura {invoiceToModify+1}

            Productos:

            """);

        foreach(ProductQuantity product in invoices[invoiceToModify].Products)
        {
            System.Console.WriteLine($"{product.Product.Name}: {product.Quantity}");
        }
        
        System.Console.WriteLine("\n===TOTAL A PAGAR DE LA FACTURA===");
        Console.WriteLine($"ITBIS: {invoices[invoiceToModify].CalculateTotalTax()}");
        Console.WriteLine($"SUBTOTAL: {invoices[invoiceToModify].CalculateSubtotal()}");
        Console.WriteLine($"TOTAL: {invoices[invoiceToModify].CalculateTotalInvoice()}");
        System.Console.WriteLine("========================================================");
        System.Console.WriteLine("");

        System.Console.WriteLine("Presione una tecla para ver la siguiente factura!");
        Console.ReadKey();
        
        System.Console.WriteLine("Introduzca un nombre para la factura (Presione 'Enter' para omitir)");
        newInvoiceName = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(newInvoiceName))
        {
            newInvoiceName = invoices[invoiceToModify].InvoiceName;
        }
        System.Console.WriteLine("Modificar Productos de la Factura");

        for(int i = invoices[invoiceToModify].Products.Count-1; i>=0; i--)
        {
            System.Console.WriteLine($"{invoices[invoiceToModify].Products[i].Product.Name}: {invoices[invoiceToModify].Products[i].Product.Name}");
            System.Console.WriteLine(
                """

                1- Siguiente producto.
                2- Editar producto/cantidad.
                3- Eliminar.
                """
            );

            while(!int.TryParse(Console.ReadLine(), out productModifyOption) || productModifyOption>3 || productModifyOption<1)
            {
                System.Console.WriteLine("Introduzca una opcion valida!");
            }

            switch(productModifyOption)
            {
                case 1:
                    break;
                
                case 2:
                    ModifyProductQuantity(invoices[invoiceToModify].Products[i]);
                    System.Console.WriteLine("Se ha editado el registro exitosamente.");
                    Console.ReadKey();
                    break;

                case 3:
                    productQuantityService.DeleteProduct(invoices[invoiceToModify].Products[i]);
                    invoices[invoiceToModify].Products.RemoveAt(i);
                    System.Console.WriteLine("Se ha eliminado el registro exitosamente.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public void ModifyProductQuantity(ProductQuantity productq)
    {
        List<Product> products = productService.ListProducts();
        string productSelected;
        int selectedProductIndex = -1;
        int productIndex = 0;
        string quantity;
        int newQuantity = 0;
        Console.Clear();
        System.Console.WriteLine("Seleccione un nuevo producto (Presione 'Enter' para dejar igual).");

        foreach(Product product in products)
        {
            productIndex++;
            System.Console.WriteLine($"{productIndex}- {product.Name}");
        }

        while (true)
        {
            System.Console.WriteLine("Seleccione el ID del producto (o presione Enter para dejar igual):");
            productSelected = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(productSelected)) break; 

            if (int.TryParse(productSelected, out selectedProductIndex) && selectedProductIndex > 0 && selectedProductIndex <= products.Count)
            {
                System.Console.WriteLine($"Has seleccionado: {products[selectedProductIndex - 1].Name}");
                break;
            }
            System.Console.WriteLine($"Error: Introduzca un número válido entre 1 y {products.Count}");
        }

        System.Console.WriteLine("Introduzca la cantidad (o presione Enter para omitir)");
        quantity = Console.ReadLine();
        while(true)
        {
            if(string.IsNullOrWhiteSpace(quantity))
            {
                newQuantity = productq.Quantity;
                break;
            }


            if(int.TryParse(quantity, out newQuantity) && newQuantity >= 0)
            {
                break;
            }

            System.Console.WriteLine("Error: Introduzca una cantidad válida (número positivo):");
            quantity = Console.ReadLine();
        }
        Product finalProduct = (selectedProductIndex == -1) ? productq.Product : products[selectedProductIndex - 1];
        productQuantityService.ModifyProduct(productq, finalProduct, newQuantity);
    }
    
    public void DeleteInvoice()
    {
        int selectedInvoice;
        int invoiceIndex = 0;
        bool deletedInvoice;

        List<Invoice> invoices = invoiceService.ListInvoices();

        foreach(Invoice invoiceToModify in invoices)
        {
            invoiceIndex++;
            System.Console.WriteLine($"{invoiceIndex}- {invoiceToModify.InvoiceName}");
        }

        System.Console.WriteLine("0- Cancelar");
        System.Console.WriteLine("\nSeleccione el ID de la factura a modificar!");

        while(!int.TryParse(Console.ReadLine(),out selectedInvoice) || selectedInvoice>invoices.Count || selectedInvoice <0)
        {
            System.Console.WriteLine("Introduzca una opcion valida!");
        }

        if(selectedInvoice-1==-1)
        {
            return;
        }

        else
        {
            selectedInvoice = selectedInvoice-1;
            System.Console.WriteLine($"La factura a eliminar sera: {invoices[selectedInvoice].InvoiceName}");
            deletedInvoice = invoiceService.DeleteInvoice(invoices[selectedInvoice]);
            if(deletedInvoice)
            {
                System.Console.WriteLine("Factura eliminada exitosamente");
            }
            else
            {
                System.Console.WriteLine("No se ha podido eliminar la factura.");
            }
        }
    }
}