namespace InvoiceSystem;
public class InvoiceMenu
{
    public int Show()
    {
        string[] InvoiceMenu = new string[]
        {
            " ",
            " 1- Crear Factura ",
            " 2- Editar Factura ",
            " 3- Eliminar Factura ",
            " 4- Ver Facturas ",
            " 4- Atras ",
            " "
        };
        
        return MenuDrawer.DibujarMenu(InvoiceMenu," Facturas ");
    }
}