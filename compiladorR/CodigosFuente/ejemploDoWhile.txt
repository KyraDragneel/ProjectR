import java.util.Scanner;

public class miClase
{
	public static void main(String[] args)
	{
		Scanner leer = new Scanner(System.in);
		int numero, limite, opcion;
		System.out.println("Tablas de multiplicar");
		System.out.println("");
		
		do
		{
			System.out.println("Ingrese el numero a evaluar");
			numero = leer.nextInt();
			System.out.println("Ingrese el limite");
			limite = leer.nextInt();
			System.out.println("");

			System.out.println("Tabla del "+numero);
			for(int i = 1; i <= limite; i++)
			{
				System.out.println(numero+"x"+i+"= "+(numero*i));
			}
			System.out.println("");

			System.out.println("¿Quieres calcular otras tablas?");
			System.out.println("0-No");
			System.out.println("1-Si");
			opcion = leer.nextInt();
			System.out.println("");
		}
		while(opcion != 0);
	}
}