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
		List<ReceiverProperty> reciverProp;
		ReplicatorReceiver replicatorRec;
		Thread thread;

		public ReplicatorSender()
		{
			reciverProp = new List<ReceiverProperty>();
			mutex = new Mutex();
			replicatorRec = new ReplicatorReceiver();
			thread = new Thread(ReplicatorSenerSend);
			thread.Start();
		}

		public void ReplicatorSenderRecive(CODE code ,int value)
		{
			reciverProp.Add(new ReceiverProperty(code, value));
			Monitor.Pulse(thread);
		}

		public void ReplicatorSenerSend()
		{
			lock (mutex)
			{
				while (reciverProp.Count <= 0)
				{
					mutex.WaitOne();
				}

				replicatorRec.Send(reciverProp[reciverProp.Count - 1].Code.ToString(), reciverProp[reciverProp.Count - 1].ReceiverValue);
				Thread.Sleep(1000);
			}
			
		}


	}
}
