using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
    public enum CODE { CODE_ANALOG, CODE_DIGITAL, CODE_CUSTOM, CODE_LIMITSET, CODE_SINGLENODE, CODE_MULTIPLENODE, CODE_CONSUMER, CODE_SOURCE }
    public struct CollectionDescription
    {
        public int Id;
        public int DataSet;
        public HistoricalCollection Collection;
        public CollectionDescription(int id, int dataSet)
        {
            Id = id;
            DataSet = dataSet;
            Collection = new HistoricalCollection();
            Collection.properties = new ReceiverProperty[100];
        }
    }
    public struct HistoricalCollection
    {
        public ReceiverProperty[] properties;
    }
    public struct ReceiverProperty
    {
        public CODE Code;
        public int ReceiverValue;

        public ReceiverProperty(CODE code, int receiverValue)
        {
            Code = code;
            ReceiverValue = receiverValue;
        }
    }
    public class ReplicatorReceiver
    {
        CollectionDescription collectionDescription1;
        CollectionDescription collectionDescription2;
        CollectionDescription collectionDescription3;
        CollectionDescription collectionDescription4;
        int collection1Count;
        int collection2Count;
        int collection3Count;
        int collection4Count;
        Reader reader1;
        Reader reader2;
        Reader reader3;
        Reader reader4;

        Thread thread;
        public ReplicatorReceiver()
        {
            collectionDescription1 = new CollectionDescription(1,1);
            collectionDescription2 = new CollectionDescription(2,2);
            collectionDescription3 = new CollectionDescription(3,3);
            collectionDescription4 = new CollectionDescription(4,4);

            collection1Count = 0;
            collection2Count = 0;
            collection3Count = 0;
            collection4Count = 0;

            reader1 = new Reader(@"E:\Tea\fax\PrivatniCasoviFtn\Emilija res\ReplicatorProject\ReplicatorProj\Receiver\database1.txt");
            reader2= new Reader(@"E:\Tea\fax\PrivatniCasoviFtn\Emilija res\ReplicatorProject\ReplicatorProj\Receiver\database2.txt");
            reader3= new Reader(@"E:\Tea\fax\PrivatniCasoviFtn\Emilija res\ReplicatorProject\ReplicatorProj\Receiver\database3.txt");
            reader4= new Reader(@"E:\Tea\fax\PrivatniCasoviFtn\Emilija res\ReplicatorProject\ReplicatorProj\Receiver\database4.txt");

            thread = new Thread(ReadersRead);
            thread.Start();

        }

        public void Send(string code, int value)
        {

            CODE c;
            Enum.TryParse(code, out c);
            ReceiverProperty rp = new ReceiverProperty(c, value);
            if (c == CODE.CODE_ANALOG || c == CODE.CODE_DIGITAL)
            {
                collectionDescription1.Collection.properties[collection1Count] = rp;
                collection1Count++;
            }
            else if (c == CODE.CODE_CUSTOM || c == CODE.CODE_LIMITSET)
            {
                collectionDescription2.Collection.properties[collection2Count] = rp;
                collection2Count++;
            }
            else if (c == CODE.CODE_SINGLENODE || c == CODE.CODE_MULTIPLENODE)
            {
                collectionDescription3.Collection.properties[collection3Count] = rp;
                collection3Count++;
            }
            else if (c == CODE.CODE_CONSUMER || c == CODE.CODE_SOURCE)
            {
                collectionDescription4.Collection.properties[collection4Count] = rp;
                collection4Count++;
            }


        
        }
        private ReceiverProperty[] RemoveFirst(ReceiverProperty[] param)
        {
            List<ReceiverProperty> l = param.ToList<ReceiverProperty>();
            l.RemoveAt(0);
            return l.ToArray();

        }
        public void ReadersRead()
        {
            while(true)
            {
                if(collection1Count>0)
                {
                    reader1.WriteInFile(collectionDescription1.Collection.properties[0]);
                    collectionDescription1.Collection.properties = RemoveFirst(collectionDescription1.Collection.properties);
                    collection1Count--;
                }
                
                if(collection2Count>0)
                {
                    reader2.WriteInFile(collectionDescription2.Collection.properties[0]);
                    collectionDescription2.Collection.properties = RemoveFirst(collectionDescription2.Collection.properties);
                    collection2Count--;
                }

                if (collection3Count > 0)
                {
                    reader3.WriteInFile(collectionDescription3.Collection.properties[0]);
                    collectionDescription3.Collection.properties = RemoveFirst(collectionDescription3.Collection.properties);
                    collection3Count--;
                }
                   

                if (collection4Count > 0)
                {
                    reader4.WriteInFile(collectionDescription4.Collection.properties[0]);
                    collectionDescription4.Collection.properties = RemoveFirst(collectionDescription4.Collection.properties);
                    collection4Count--;
                }

                Thread.Sleep(1000);
            }
        }
    }
}
