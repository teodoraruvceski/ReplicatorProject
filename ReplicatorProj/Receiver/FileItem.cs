using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    class FileItem
    {
         ReceiverProperty rp;
        DateTime dateTime;

        public FileItem(ReceiverProperty rp, DateTime dateTime)
        {
            this.Rp = rp;
            this.DateTime = dateTime;
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        internal ReceiverProperty Rp { get => rp; set => rp = value; }
    }
}
