using Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
	class Program
	{
		

		static void Main(string[] args)
		{
			Writer writer = new Writer();
			List<Thread> threads = new List<Thread>();

			int ret;
			int Meni()
			{
				int br;
					while (true)
					{
						Console.WriteLine("1. Upali witera");
						Console.WriteLine("2. Ugasi writera");
						Console.WriteLine("3. Odustani");
						Console.WriteLine("4. Ugasi klijenta");
						try
						{
							br = int.Parse(Console.ReadLine());
							return br;
						}
						catch
						{
							Console.WriteLine("Pogresan unos!");
						}
					}
			}


			while (true)
			{
				while (true)
				{
					Console.WriteLine("Pritisni dugme za MENU");
					if (threads.Count == 0)
					{
						Console.WriteLine("Program nema upaljenih writera.");
					}
					Console.ReadLine();
					break;

					/*if (Console.KeyAvailable)
					{
						if (Console.ReadKey().Key == ConsoleKey.Escape)
							break;
					}*/
					//Task task = Task.Factory.StartNew(() => w.Send());

				}
				ret = Meni();
				if (ret == 1)
				{

					threads.Add(new Thread(writer.WriterSend));
					threads[threads.Count - 1].Start();
					//Task task = Task.Factory.StartNew(() => writers[writers.Count() - 1].Send());
				}
				else if (ret == 2)
				{
					threads[threads.Count - 1].Abort();
					threads.RemoveAt(threads.Count - 1);
				}
				else if (ret == 3)
				{
				}
				else if (ret == 4)
				{
					break;
				}
				else
				{
					Console.WriteLine("Nepostojeca komanda.");
				}
			}


		}
	}
}
