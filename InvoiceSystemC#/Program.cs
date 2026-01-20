namespace InvoiceSystem;

class Program
{
    public static void Main(string[] args)
    {
        Setup Instances = new Setup();
        bool salir = false;
        
        do{
            Console.Clear();
            int menuOption = Instances.mainMenu.Show();

            switch(menuOption)
            {
                case 1:
                    Instances.productController.HandleProductMenu();
                    break;
                case 2:
                    Instances.invoiceController.HandleInvoiceMenu();
                    break;
                case 3:
                    salir = true;
                    break;
                default:
                    System.Console.WriteLine("ERR: Debe elegir una opcion valida!");
                    break;
            }
        } while (!salir);
    }
}