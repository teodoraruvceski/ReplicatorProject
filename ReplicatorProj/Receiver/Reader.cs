using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Receiver
{
    public struct CollectionFileItems
    {
        public FileItem[] fileItems;
    }
    public class Reader
    {
        string fileName;
		Logger l;

		public Reader(string fileName)
        {
            this.fileName = fileName;
			l = new Logger(@"LOGGS\readerLogs");
        }
		
		public void WriteInFile(ReceiverProperty receiverProperty)
		{
			string str;
			string[] niz;
			int difference;

			if (receiverProperty.Code == CODE.CODE_DIGITAL)
			{
				using(StreamWriter sw = new StreamWriter(fileName, true))
				{
					sw.WriteLine(DateTime.Now.ToString() + ";" + receiverProperty.Code.ToString() + ";" + receiverProperty.ReceiverValue.ToString());
					l.LoggStoredCodes(receiverProperty.Code, receiverProperty.ReceiverValue, DateTime.Now);
					return;
				}
			}
			using (StreamReader sr = new StreamReader(fileName))
			{
				while (true)
				{
					str = sr.ReadLine();
					if (str == "<EOF>" || str==null)
					{
						break;
					}
					else
					{
						niz = str.Split(';');
						DateTime pom1 = DateTime.Parse(niz[0]);
						CODE pom2;
						CODE.TryParse(niz[1],out pom2);
						int pom3 = int.Parse(niz[2]);
						FileItem it=new FileItem() { dateTime = pom1, rp = new ReceiverProperty() { Code = pom2, ReceiverValue =pom3 } };
						if (it.rp.Code == receiverProperty.Code)
						{
							difference = Math.Abs(it.rp.ReceiverValue - receiverProperty.ReceiverValue);
							if (difference < it.rp.ReceiverValue * 0.02)
							{
								return;
							}
						}
					}
				}
			}
			using (StreamWriter sw = new StreamWriter(fileName, true))
			{
				sw.WriteLine(DateTime.Now.ToString() + ";" + receiverProperty.Code.ToString() + ";" + receiverProperty.ReceiverValue.ToString());
				l.LoggStoredCodes(receiverProperty.Code, receiverProperty.ReceiverValue,DateTime.Now);
			}
		}

	}
}
