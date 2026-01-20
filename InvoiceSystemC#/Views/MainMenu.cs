namespace InvoiceSystem;
public class MainMenu
{
    public int Show()
    {
        string[] MainMenu = new string[]
        {
            " ",
            " 1- Productos. ",
            " 2- Facturas. ",
            " 3- Salir. ",
            " "
        };
        
        return MenuDrawer.DibujarMenu(MainMenu," Menu Principal ");
    }
}