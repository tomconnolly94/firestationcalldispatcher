using System;

namespace FireStationCallDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            DispatchSimulator dispatchSimulator = new DispatchSimulator();
            dispatchSimulator.TriggerSimulation();
        }
    }
}
