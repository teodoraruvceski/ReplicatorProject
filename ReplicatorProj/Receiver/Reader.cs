using System;
using System.Collections.Generic;
using System.IO;
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
            FileItem fileItem = new FileItem();
            fileItem.dateTime = DateTime.Now;
            fileItem.rp = receiverProperty;
           
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(FileItem));
            var path = fileName;
           

            //System.Xml.Serialization.XmlSerializer serializer =  new System.Xml.Serialization.XmlSerializer(typeof(CollectionFileItems));
            //System.IO.StreamReader reader = new System.IO.StreamReader(path);
            //CollectionFileItems hc = (CollectionFileItems)serializer.Deserialize(reader);
            //reader.Close();
            //double difference;
            //foreach (FileItem fi in hc.fileItems)
            //{
            //     difference = Math.Abs(fi.rp.ReceiverValue - receiverProperty.ReceiverValue);
            //     if (difference > fi.rp.ReceiverValue*0.02)
            //        return;
            //}

            TextWriter file = new StreamWriter(path);

            writer.Serialize(file, fileItem);
            file.Close();

        }
    }
}
