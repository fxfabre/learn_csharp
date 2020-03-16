using System;
using System.Threading;

namespace BergerMT
{
    public static class Constants
    {
        public static void DisplayMsg(string message)
        {
            Console.WriteLine(Thread.CurrentThread.Name + " " + message);
        }
    }

    public enum Position
    {
        Left,
        Right,
        Boat,
        Default = Right
    }

    public enum Farming
    {
        Unknown,
        Farmer,
        Goat,
        Wolf,
        Cabbage
    }

    public enum Direction
    {
        Unknwon,
        Left,
        Right,
        Boat
    }

    public class FarmingAction
    {
        public Farming To { get; set; }
        public Farming With { get; set; }

        public Direction Direction { get; set; }
    }

    public class EatAction : FarmingAction
    {
        public Position FarmerPosition { get; set; }
    }
    

}
