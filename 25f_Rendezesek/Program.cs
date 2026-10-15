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
		#region Maximumkiválasztásos rendezés
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
		static void Csere<T>(List<T> l, int i, int j)
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
		#endregion

		#region Beszúró rendezés

		static void Beszúró_rendezés<T>(List<T> l, Func<T, T, int> comparator)
		{
			for (int i = 1; i < l.Count; i++)
			{
				int j = Helye_a_balralévők_közt(l,i, comparator);
				Átemelés(l, i, j);
			}
		}
		static int Helye_a_balralévők_közt<T>(List<T> l, int i, Func<T, T, int> comparator)
		{
			int j = i - 1;
			while (0 <= j && comparator(l[j], l[i]) > 0)
			{
				j--;
			}
			return j + 1;
		}
		/** /
		Függvény Helye_a_balralévők_közt(l:Lista[Egész], i:Egész) : Egész
			Lokális változók: 
				j: Egész
			j:=i-1
			Ciklus amíg 1<=j és l[i] < l[j]:
				j:=j-1
			Ciklus vége
			Helye_a_balralévők_közt := j+1
		Függvény vége
		/**/
		static void Átemelés<T>(List<T> l, int i, int j){
			T temp = l[i];
			for (int k = i; j < k; k--)
			{
				l[k] = l[k - 1];
			}
			l[j] = temp;
		}

		/** /
		Eljárás Átemelés (l:Lista[Egész], i:Egész, j:Egész)
			Lokális változó:
				temp: Egész
			temp := l[i]
			Ciklus k:=i-től j<k; -1-esével
				l[k] := l[k-1]
			Ciklus vége
			l[j] := temp			
		Eljárás vége
		/**/

		#endregion

		#region Buborékos rendezés

		static void Gagyi_buborékos_rendezés<T>(List<T> l, Func<T, T, int> comparator)
		{
			for (int eddig = l.Count; 1 <= eddig; eddig--)
			{
				for (int i = 1; i < eddig; i++)
				{
					if (comparator(l[i-1], l[i]) > 0)
					{
						Csere(l, i, i - 1);
					}
				}
			}
		}

		#endregion

		#region gyorsrendezés
		static void Gyorsrendezés<T>(List<T> l, Func<T,T,int> comparator) => Gyorsrendezés(l, 0, l.Count-1, comparator);
		static void Gyorsrendezés<T>(List<T> l, int e, int v, Func<T,T,int> comparator)
		{
			if (e < v)
			{
				int i = Partíciókra_bontás(l, e, v, comparator); // ettől jobbra már csak nagyobbak, balra meg csak kisebbek vannak!								   
				Gyorsrendezés(l, e, i - 1, comparator); // oszd meg és uralkodj! / divide et impera !
				Gyorsrendezés(l, i + 1, v, comparator);
			}
		}

		static int Partíciókra_bontás<T>(List<T> l, int e, int v, Func<T,T,int> comparator)
		{
			int i = e;
			int j = v; //  virágos kalap

			while (i != j)
			{
				//if (i < j != l[i] < l[j])
				if (i < j != (comparator(l[i],l[j]) == -1) )
				{
					//Csere(l, i, j); 
					//Csere(ref i, ref j); // pl C++-ban így kell
					(l[i], l[j]) = (l[j], l[i]);
					(i, j) = (j, i);
				}
				//j += i < j ? -1 : 1;
				j += i.CompareTo(j);
			}
			return i; // vagy j
		}

		static void Csere(ref int i, ref int j)
		{
			Console.WriteLine($"i={i}, j={j}");
			int temp = i;
			i = j;
			j = temp;
			Console.WriteLine($"i={i}, j={j}");
		}

		#endregion

		#region összefésüléses rendezés

		static void MergeSort<T>(List<T> lista, Func<T,T,int> c)
		{
			T[] temp_tomb = new T[lista.Count]; // azért tömb, mert egyből ennyi eleme kellene legyen, ne üresen induljon!
			MergeSort(lista, 0, lista.Count - 1, c , temp_tomb);
		}

		static void MergeSort<T>(List<T> lista, int e, int v, Func<T,T,int> c, T[] temp_lista)
		{
			if (e>=v)
				return;

			int k = (e + v) / 2;
			MergeSort(lista, e, k, c, temp_lista);
			MergeSort(lista, k+1, v, c, temp_lista);
			Merge(lista, e, k, v, c, temp_lista);
		}
		static void Merge<T>(List<T> lista, int e, int k, int v, Func<T,T,int> c, T[] temp_tomb)
		{

			int i = e;
			int j = k + 1;
			int temp_i = e;

			while (i<=k && j<=v)
			{
				if (c(lista[i],lista[j]) < 0)
				{
					temp_tomb[temp_i] = lista[i];
					i++;
					temp_i++;
				}
				else if (c(lista[i],lista[j]) > 0)
				{
					temp_tomb[temp_i] = lista[j];
					j++;
					temp_i++;
				}
				else // if (c(lista[i],lista[j]) == 0)
				{
					temp_tomb[temp_i] = lista[i];
					i++;
					temp_i++;

					temp_tomb[temp_i] = lista[j];
					j++;
					temp_i++;

				}
			}

			while (i<=k)
			{
				temp_tomb[temp_i] = lista[i];
				i++;
				temp_i++;
			}

			while (j <= v)
			{
				temp_tomb[temp_i] = lista[j];
				j++;
				temp_i++;
			}

			// visszamásoljuk a temp-ből
			for (int index = e; index <= v; index++)
			{
				lista[index] = temp_tomb[index];
			}
		}
		#endregion

		static List<int> Unió(List<int> A, List<int> B)
		{
			List<int> result = new List<int>(A.Count+B.Count);
			int i = 0;
			int j = 0;

			while (i<A.Count && j<B.Count)
			{
				if (A[i] < B[j])
				{
					result.Add(A[i]);
					i++;
				}
				else if (A[i] > B[j])
				{
					result.Add(B[j]);
					j++;					
				}
				else
				{
					result.Add(A[i]);
					i++;
					result.Add(B[j]);
					j++;
				}
			}

			while (i < A.Count)
			{
				result.Add(A[i]);
				i++;
			}

			while (j < B.Count)
			{
				result.Add(B[j]);
				j++;
			}

			return result;
		}

		static List<int> Metszet(List<int> A, List<int> B)
		{
			throw new NotImplementedException();
		}

		static List<int> Különbség(List<int> A, List<int> B)
		{
			throw new NotImplementedException();
		}

		static void Main(string[] args)
		{
			int p = 5;
			int q = 7;
			Console.WriteLine($"a={p}, b={q}");
			Csere(ref p, ref q);
			Console.WriteLine($"a={p}, b={q}");



			List<int> l1 = new List<int> { 1, 3, 4, 56, 2, 2, 3, 45, 5, 2, 3, 4, 5, 9, 6, 6, 2, 123, 4, 1, 456, 0, 2, 2, 34, 5 };

			List<int> l2 = new List<int> { 5, 8, 9, 3, 4, 5, 8, 3, 1, 2, 3, 4, 6, 8, 123, 34, 6, 234, 56, 1 };

			Console.WriteLine(string.Join(", ", l1));
			Console.WriteLine(string.Join(", ", l2));

			MergeSort(l1, (x, y) => x.CompareTo(y));
			MergeSort(l2, (x, y) => x.CompareTo(y));


			Console.WriteLine(string.Join(", ", l1));
			Console.WriteLine(string.Join(", ", l2));
			return;
			List<int> l3 = Unió(l1, l2);
			Console.WriteLine(string.Join(", ", l3));



			//List<int> l4 = Metszet(l1, l2);
			//List<int> l5 = Különbség(l1, l2);






			Console.WriteLine(string.Join(", ", l1));
			//Maximumkiválasztásos_rendezés(lista, (x, y) => x.CompareTo(y));
			//Beszúró_rendezés(lista, (x, y) => x.CompareTo(y));
			//Gagyi_buborékos_rendezés(lista, (x, y) => x.CompareTo(y));
			Gyorsrendezés(l1, (x, y) => x.CompareTo(y));
			Console.WriteLine(string.Join(", ", l1));

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
