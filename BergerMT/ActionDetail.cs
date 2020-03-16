using System;
using System.Threading;

namespace BergerMT
{
    public class ActionDetail
    {
        private static int number = 0;
        public int N { get; set; }

        // The action to execute
        public Action<AMovingItem, FarmingAction> FunctionToCall { get; set; }
        public FarmingAction Parameters { get; set; }
        
        // Events
        public EventWaitHandle EndEvent { get; set; }

        // Constructor
        public ActionDetail(Action<AMovingItem, FarmingAction> function, FarmingAction parameters)
        {
            this.N = ++number;

            // The action to execute
            this.FunctionToCall = function;
            this.Parameters = parameters;

            // Events
            this.EndEvent = new ManualResetEvent(false);
        }

        public string Display()
        {
            string name = FunctionToCall.Method.ToString();
            name = name.Substring(0, 14);

            return " " + N + " : " + name + "\n"
                + string.Format("    To : {0}, With = {1}, Dir = {2}",
                Parameters.To, Parameters.With, Parameters.Direction);
        }

    }
}
