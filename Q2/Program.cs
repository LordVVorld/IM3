using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    internal class Program
    {
        private const double l = 1.2;
        private const double m = 2;
        private const int selectionSet = 100;
        private const int workHours = 8 * 60;

        static void Main(string[] args)
        {
            double loadFactor = l / m;
            double serveChance = 1 / (1 + loadFactor);
            double denyChance = loadFactor / (1 + loadFactor);
            double throughput = l * serveChance;

            var cpmRandomizer = new Random();
            var clRandomizer = new Random();
            for (int dayIndex = 0; dayIndex < selectionSet; dayIndex++)
            {
                double callsPerMin = 2.4 * cpmRandomizer.NextDouble();
                double callLenght = 4 * clRandomizer.NextDouble();
                for (double minute = 0; minute < workHours; minute += callLenght)
                {
                    if (minute % l)
                }
            }
        }
    }
}
