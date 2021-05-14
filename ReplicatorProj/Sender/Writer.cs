using Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
	public class Writer
	{
		//Mutex mutex;
		ReplicatorSender replicatoSender;
		int code, value;
		Random rand1;
		Random rand2;

		public Writer()
		{
			replicatoSender = new ReplicatorSender();
			rand1 = new Random();
			rand2 = new Random();
		}
		public void WriterSend()
		{
		
			while (true)
			{
				code = rand1.Next(0, 7);
				value = rand2.Next();
				replicatoSender.ReplicatorSenderRecive((CODE)code, value);
				//Console.WriteLine($"Posao : {(CODE)code} , val: {value}");
				Thread.Sleep(2000);
			}

		}

	}
}
