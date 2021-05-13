using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    struct CollectionFileItems
    {
        public FileItem[] fileItems;
    }
    class Reader
    {
        string fileName;

        public Reader(string fileName)
        {
            this.fileName = fileName;
        }
        public void WriteInFile(ReceiverProperty receiverProperty)
        {
            FileItem fileItem = new FileItem(receiverProperty, DateTime.Now);
           
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(FileItem));
            var path = @"\garbage.xml";
            if (fileName=="dataSet1")
            {
                 path = @"\dataset1.xml";
            }
            else if(fileName == "dataSet2")
            {
                path = @"\dataset2.xml";
            }
            else if (fileName == "dataSet3")
            {
                path = @"\dataset3.xml";
            }
            else if (fileName == "dataSet4")
            {
                path = @"\dataset4.xml";
            }

            System.Xml.Serialization.XmlSerializer serializer =  new System.Xml.Serialization.XmlSerializer(typeof(CollectionFileItems));
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
            CollectionFileItems hc = (CollectionFileItems)serializer.Deserialize(reader);
            reader.Close();
            double difference;
            foreach (FileItem fi in hc.fileItems)
            {
                 difference = Math.Abs(fi.Rp.ReceiverValue - receiverProperty.ReceiverValue);
                if (difference > 0.02)
                    return;
            }

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, fileItem);
            file.Close();

        }
    }
}
