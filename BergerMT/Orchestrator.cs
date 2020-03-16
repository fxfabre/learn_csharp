using System;
using System.Collections.Generic;

namespace BergerMT
{
    internal class Orchestrator
    {
        private static Direction[] directions = { Direction.Left, Direction.Right};

        private static int i = 0;
        private static Direction GetNewDirection()
        {
            return directions[(++i) % directions.Length];
        }

        public static IEnumerable<FarmingAction> GetAction()
        {
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection() };
            Console.WriteLine("Left to right with goat");
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection(), With = Farming.Goat };
            Console.WriteLine("right to left");
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection() };
            Console.WriteLine("Left to right with wolf");
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection(), With = Farming.Wolf };
            Console.WriteLine("right to left with goat");
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection(), With = Farming.Goat };
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection(), With = Farming.Cabbage };
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection() };
            yield return new FarmingAction { To = Farming.Farmer, Direction = GetNewDirection(), With = Farming.Goat };
        }

    }

}