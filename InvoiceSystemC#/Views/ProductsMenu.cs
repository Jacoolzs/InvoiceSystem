namespace InvoiceSystem;
public class ProductMenu
{
    public int Show()
    {
        string[] ProductsMenu = new string[]
        {
            " ",
            " 1- Agregar producto ",
            " 2- Editar Producto ",
            " 3- Eliminar Producto ",
            " 4- Ver Productos ",
            " 5- Atras ",
            " "
        };
        
        return MenuDrawer.DibujarMenu(ProductsMenu," Productos ");
    }
}