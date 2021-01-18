import java.util.Scanner;

public class miClase
{
	public static void main(String[] args)
	{
	    
		Scanner leer = new Scanner(System.in);

		System.out.println("Ingrese el largo del arreglo");
		int largo = leer.nextInt();
		System.out.println("");

		System.out.println("Ingrese la cantidad de filas de la matriz");
		int filas = leer.nextInt();
		System.out.println("Ingrese la cantidad de columnas de la matriz");
		int columnas = leer.nextInt();
		System.out.println("");

		int[] arreglo = new int[largo];
		int[][] matriz = new int[filas][columnas];

		for(int i = 0; i < largo; i++)
		{
			arreglo[i] = i+1;
		}

		System.out.println("Arreglo");
		for(int j = 0; j < largo; j++)
		{
			System.out.print("["+arreglo[j]+"]");
		}
		System.out.println("");
		System.out.println("");

		for(int a = 0; a < filas; a++)
		{
			for(int b = 0; b < columnas; b++)
			{
				matriz[a][b] = b+1;
			}
		}

		System.out.println("Matriz");
		for(int c = 0; c < filas; c++)
		{
			for(int d = 0; d < columnas; d++)
			{
				System.out.print("["+matriz[c][d]+"]");
			}
			System.out.println("");
		}
		System.out.println("");
	}
}

