import java.util.Scanner;

public class miClase
{
	public static void main(String[] args)
	{

		Scanner leer = new Scanner(System.in);
		int respuesta = 10;
		int numero;
		boolean correcto = false;

		while(correcto == false)
		{
			System.out.println("Adivina el numero");
			numero = leer.nextInt();

			if(numero > respuesta)
			{
				System.out.println("Prueba con un numero mas pequeño");
			}
			else if(numero < respuesta)
			{
				System.out.println("Prueba con un numero mas grande");
			}
			else
			{
				correcto = true;
			}
		}

		System.out.println("Felicidades, acertaste");
	}
}