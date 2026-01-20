namespace InvoiceSystem;
public static class MenuDrawer{
    public static int DibujarMenu(string[] Menu, string Titulo)
    {
        int opcionSeleccionada = 0;
        int longitudMax = 0;
        int longitudTitulo = Titulo.Length;
        foreach (string linea in Menu)
        {
            if (linea.Length > longitudMax)
            {

                longitudMax = linea.Length;
            }

            if (longitudMax < longitudTitulo)
            {
                longitudMax = longitudTitulo;
            }
        }

        int espaciosTitulo = longitudMax - longitudTitulo;
        int espaciosIzquierda = espaciosTitulo / 2;
        int espaciosDerecha = espaciosTitulo - espaciosIzquierda;

        System.Console.WriteLine("++" + new string('-', longitudMax) + "++");
        System.Console.WriteLine("||" + new string(' ', espaciosIzquierda) + Titulo + new string(' ', espaciosDerecha) + "||");
        System.Console.WriteLine("++" + new string('-', longitudMax) + "++");

        foreach (string linea in Menu)
        {
            int cantidadEspacios = longitudMax - linea.Length;
            System.Console.WriteLine("||" + linea + new string(' ', cantidadEspacios) + "||");
        }

        System.Console.WriteLine("++" + new string('-', longitudMax) + "++");
        System.Console.WriteLine("++" + new string('-', longitudMax) + "++");

        do
        {
            System.Console.Write("Seleccione una opcion del menu: ");
        } while(!int.TryParse(Console.ReadLine(),out opcionSeleccionada));

        return opcionSeleccionada;
    }
}
