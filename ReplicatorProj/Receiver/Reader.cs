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


		public Reader(string fileName)
        {
            this.fileName = fileName;
        }
		/*public void WriteInFile(ReceiverProperty receiverProperty)
        {
            FileItem fileItem = new FileItem();
            fileItem.dateTime = DateTime.Now;
            fileItem.rp = receiverProperty;
			int difference;
			CollectionFileItems collectionFileItems = new CollectionFileItems() { fileItems = new FileItem[1000] };
			;
			CollectionFileItems collectionFileItems1;

			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(CollectionFileItems));
			System.Xml.Serialization.XmlSerializer deserializer =  new System.Xml.Serialization.XmlSerializer(typeof(CollectionFileItems));
            var path = fileName;

			//CITANJE IZ FAJLA
			StreamReader streamReader = new StreamReader(path);
			
			collectionFileItems = (CollectionFileItems)deserializer.Deserialize(streamReader); //citanje citavog fajla

			streamReader.Close();
			StreamWriter streamWriter = new StreamWriter(path);

			if (fileItem.rp.Code == CODE.CODE_DIGITAL)
			{
				collectionFileItems1 = new CollectionFileItems() { fileItems = new FileItem[collectionFileItems.fileItems.Length + 1] };
				collectionFileItems1.fileItems[collectionFileItems1.fileItems.Length] = fileItem;
				serializer.Serialize(streamWriter, collectionFileItems1);
				streamWriter.Close();
				return;
			}
			else
			{
				for (int i = 0; i < collectionFileItems.fileItems.Length; i++)
				{
					if (collectionFileItems.fileItems[i].rp.Code == fileItem.rp.Code)
					{
						difference = Math.Abs(collectionFileItems.fileItems[i].rp.ReceiverValue - fileItem.rp.ReceiverValue);
						if (difference < collectionFileItems.fileItems[i].rp.ReceiverValue * 0.02)
						{
							serializer.Serialize(streamWriter, collectionFileItems);
							streamWriter.Close();
							return;
						}
					}
				}
				collectionFileItems1 = new CollectionFileItems() { fileItems = new FileItem[1] };
				collectionFileItems1.fileItems[collectionFileItems1.fileItems.Length] = fileItem;
				streamWriter = new StreamWriter(path);
				serializer.Serialize(streamWriter, collectionFileItems1);
				streamWriter.Close();
				return;

			}
        }*/

		public void WriteInFile(ReceiverProperty receiverProperty)
		{
			StreamReader sr = new StreamReader(fileName);
			List<FileItem> lista = new List<FileItem>();
			FileItem fi = new FileItem();
			string str;
			string[] niz;
			int difference;

			while (true)
			{
				str = sr.ReadLine();
				if (str == "<EOF>")
				{
					break;
				}
				else
				{
					niz = str.Split(':', '/');
					lista.Add(new FileItem() { dateTime = DateTime.Parse(niz[0]), rp = new ReceiverProperty() { Code = (CODE)(int.Parse(niz[1])), ReceiverValue = int.Parse(niz[2]) } });
				}
			}

			sr.Close();
			StreamWriter sw = new StreamWriter(fileName,true);

			if (receiverProperty.Code == CODE.CODE_DIGITAL)
			{
				lista.Add(new FileItem() { dateTime = DateTime.Now, rp = new ReceiverProperty() { Code = CODE.CODE_DIGITAL, ReceiverValue = receiverProperty.ReceiverValue } });
				foreach(FileItem f in lista)
				{
					sw.WriteLine(f.dateTime.ToString() + ":" + f.rp.Code.ToString() + "/" + f.rp.ReceiverValue.ToString());
				}
				return;
			}
			foreach(FileItem f in lista)
			{
				if (f.rp.Code==receiverProperty.Code)
				{
					difference = Math.Abs(f.rp.ReceiverValue - receiverProperty.ReceiverValue);
					if(difference< f.rp.ReceiverValue * 0.02)
					{
						return;
					}
				}
			}
			lista.Add(new FileItem() { dateTime = DateTime.Now, rp = new ReceiverProperty() { Code = CODE.CODE_DIGITAL, ReceiverValue = receiverProperty.ReceiverValue } });
			foreach (FileItem f in lista)
			{
				sw.WriteLine(f.dateTime.ToString() + ":" + f.rp.Code.ToString() + "/" + f.rp.ReceiverValue.ToString());
			}

		}

	}
}
