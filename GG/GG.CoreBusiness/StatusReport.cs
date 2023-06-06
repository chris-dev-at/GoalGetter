using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.CoreBusiness
{
	public class StatusReport<T>
	{
		public StatusState State { get; set; }
		public T Value { get; set; }
		public string Reason { get; set; }

		public StatusReport() { }
		public StatusReport(StatusState state, T val, string reason = "")
		{
			State = state;
			Value = val;
			Reason = reason;
		}

		public override string ToString() => State + this.Reason;

	}
	public enum StatusState { Success, Normal, Warning, Failed, Error }
	public enum EmptyVal { Empty }
}
