using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
	public class Logger
	{
		private string fileName;
		StreamWriter sw;
		Mutex mutex;
		public Logger(string fileName)
		{
			this.fileName = fileName;
			mutex = new Mutex();
		}

		public void LoggSentCodes(CODE code, int value, DateTime dateTime, int writerId)
		{
			lock (mutex)
			{
				using (sw = new StreamWriter(fileName, true))
				{

					sw.WriteLine($"\n{dateTime} Writer id: { writerId } sent:   {code} Value:  { value}");
					sw.Close();

				}

			}
		}

		public void LoggStoredCodes(CODE code, int value, DateTime dateTime)
		{
			using (sw = new StreamWriter(fileName, true))
			{

				sw.WriteLine($"\n{dateTime} DATA STORED: {code}, Value:  { value}");
				sw.Close();

			}
		}
	}
}
