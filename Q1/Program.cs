using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int index = 0; index < 100000; index++)
            {
                Detail detail = new Detail();
                detail.Turning();
                if (detail.TurningOutcome.Item2)
                {
                    detail.Grinding();
                }
                detail.Totalize();
            }           
        }


    }

    class Detail
    {
        private const double turningBaseCharge = 5;
        private const double turningExtraCharge = 3;
        private const double grindingBaseCharge = 4;
        private const double grindingExtraCharge = 2;
        private const double materialPrice = 20;

        public (bool, bool) TurningOutcome;
        public double TurningPrice = turningBaseCharge;
        public (bool, bool) GrindingOutcome = (false, false);
        public double GrindingPrice = 0;
        public bool TotalOutcome;
        public double TotalPrice;

        public Detail()
        {

        }

        public void Turning()
        {
            double turningOutcome = new Random().NextDouble();
            switch (turningOutcome)
            {
                case double value when (value < 0.08):
                    {
                        TurningOutcome = (false, false);
                    }
                    break;
                case double value when (value < 0.2):
                    {
                        TurningOutcome = (false, true);
                        TurningPrice += turningExtraCharge;
                    }
                    break;
                default:
                    {
                        TurningOutcome = (true, true);
                        TurningPrice += turningExtraCharge;
                    }
                    break;
            }
        }

        public void Grinding()
        {
            double upperGrindingOutcome = new Random().NextDouble();
            double lowerGrindingOutcome = new Random().NextDouble();
            GrindingPrice = grindingBaseCharge;
            switch (upperGrindingOutcome)
            {
                case double value when (value < 0.03):
                    {
                        GrindingOutcome = (false, GrindingOutcome.Item2);
                    }
                    break;
                default:
                    {
                        GrindingOutcome = (true, GrindingOutcome.Item2);
                    }
                    break;
            }
            switch (lowerGrindingOutcome)
            {
                case double value when (value < 0.06):
                    {
                        GrindingOutcome = (GrindingOutcome.Item1, false);
                    }
                    break;
                default:
                    {
                        GrindingOutcome = (GrindingOutcome.Item1, true);
                    }
                    break;
            }
            if (GrindingOutcome != (true, true) || GrindingOutcome != (false, false))
            {
                GrindingPrice += grindingExtraCharge;
            }
        }

        public void Totalize()
        {
            TotalOutcome = (TurningOutcome.Item2 && GrindingOutcome.Item1) && (TurningOutcome.Item2 && GrindingOutcome.Item2);
            TotalPrice = materialPrice + TurningPrice + GrindingPrice;
        }
    }
}
