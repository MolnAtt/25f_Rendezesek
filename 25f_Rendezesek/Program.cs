using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25f_Rendezesek
{
	internal class Program
	{
		static void Maximumkiválasztásos_rendezés<T>(List<T> l, Func<T,T, int> comparator)
		{
			//int j = l.Count-1;
			//while (0 < j)
			//{
			//	int i = Maximum_helye(l, j);
			//	Csere(i, j);
			//	j--;
			//}

			for (int j = l.Count-1; 0<j; j--)
			{
				int i = Maximum_helye(l, j, comparator);
				Csere(l, i, j);
			}

		}

		static void Csere<T>(List<T> l , int i , int j)
		{
			T temp = l[i];
			l[i] = l[j];
			l[j] = temp;
		}

		static int Maximum_helye<T>(List<T> l, int j, Func<T, T, int> comparator)
		{
			int legjobb_helye = 0;
			for (int i = 1; i <= j; i++)
			{
				if (comparator(l[legjobb_helye], l[i]) == -1 )// (l[legjobb_helye] < l[i])
				{
					legjobb_helye = i;
				}
			}
			return legjobb_helye;
		}

		static void Main(string[] args)
		{
			List<int> lista = new List<int> { 1, 3, 4, 56, 2, 2, 3, 45, 5, 2, 3, 4, 5, 9, 6, 6, 2, 123, 4, 1, 456, 0, 2, 2, 34, 5 };

			Console.WriteLine(string.Join(", ", lista));
			Maximumkiválasztásos_rendezés(lista, (x,y) => x.CompareTo(y));
			Console.WriteLine(string.Join(", ", lista));

			// rendezési szempont = comparator


			Func<string, string, int> hossz_alapjan = (a,b) => a.Length < b.Length ? -1 : (a.Length > b.Length ? 1 : 0);
			// két stringhez számot rendel
			// - -1-et rendel hozzá, ha a második nagyobb mint az első.
			// -  0-át rendel hozzá, ha a kettő egyenlő.
			// - +1-et rendel hozzá, ha a második kisebb, mint az első.

		}

		static int Hossz_alapján(string a, string b)
		{
			if (a.Length < b.Length)
			{
				return -1;
			}
			else if (a.Length > b.Length)
			{
				return +1;
			}
			else
			{
				return 0;
			}

			return a.Length < b.Length ? -1 : (a.Length > b.Length ? 1 : 0);
		}
	}
}
