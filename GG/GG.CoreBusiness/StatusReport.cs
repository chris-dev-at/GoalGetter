using MudBlazor;
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

		private static Dictionary<StatusState, Severity> translateStatusState = new Dictionary<StatusState, Severity>{
			{ StatusState.Normal, Severity.Normal },
			{ StatusState.Failed, Severity.Warning },
			{ StatusState.Warning, Severity.Warning },
			{ StatusState.Error, Severity.Error },
			{ StatusState.Success, Severity.Success }
		};

		public Severity severity { get { return translateStatusState[this.State]; } private set { } }


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
