using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Messages
{
	public class MessageModelPartCreatedEvent<T> where T : class
	{
		public MessageModelPartCreatedEvent(T source, dynamic part)
		{
			Source = source;
			Part = part;
		}

		/// <summary>
		/// The source object for which the model part has been created, e.g. a Product entity.
		/// </summary>
		public T Source { get; private set; }

		/// <summary>
		/// The resulting model part.
		/// </summary>
		public dynamic Part { get; private set; }
	}
}
