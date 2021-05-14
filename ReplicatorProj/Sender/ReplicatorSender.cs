using Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{

	public class ReplicatorSender
	{
		Mutex mutex;
		Queue<ReceiverProperty> reciverProp;
		ReplicatorReceiver replicatorRec;
		Thread thread;
		EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);


		public ReplicatorSender()
		{
			reciverProp = new Queue<ReceiverProperty>();
			mutex = new Mutex();
			replicatorRec = new ReplicatorReceiver();
			thread = new Thread(ReplicatorSenerSend);
			thread.Start();
		}

		public void ReplicatorSenderRecive(CODE code ,int value)
		{
			reciverProp.Enqueue(new ReceiverProperty(code, value));
			ewh.Set();
		}

		public void ReplicatorSenerSend()
		{
			lock (mutex)
			{
				while (true)
				{
					while (reciverProp.Count <= 0)
					{
						ewh.WaitOne();
					}
					ReceiverProperty rp = reciverProp.Dequeue();
					replicatorRec.Send(rp.Code.ToString(), rp.ReceiverValue);
					Thread.Sleep(1000);

				}
			}
			
		}


	}
}
