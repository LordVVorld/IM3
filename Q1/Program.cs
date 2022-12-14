using System;

namespace Q1
{
    class Program
    {
        private static readonly Random randomizer = new Random();
        private const double selectionSet = 100000;
        private const double sellingPrice = 35;
        private const double turningBaseCharge = 5;
        public const double turningExtraCharge = 3;
        private const double grindingBaseCharge = 4;
        public const double grindingExtraCharge = 2;
        private const double materialPrice = 20;


        static void Main(string[] args)
        {
            double totalCharge = 0;
            double totalIncome = 0;
            double failedDetails = 0;
            for (int index = 0; index < selectionSet; index++)
            {
                Detail detail = new Detail();
                detail.Turning();
                if (detail.IsTurningFailed)
                {
                    ++failedDetails;
                    totalCharge += detail.Price;
                    continue;
                }
                detail.Grinding();
                if (detail.IsGrindingFailed)
                {
                    ++failedDetails;
                    totalCharge += detail.Price;
                    continue;
                }
                totalCharge += detail.Price;
                totalIncome += sellingPrice;
            }

            Console.WriteLine($"На {selectionSet} деталей:\n" +
                $"Затраты на производство: {totalCharge} дн.ед.\n" +
                $"Доход от производства: {totalIncome} дн.ед.\n" +
                $"Прибыль от производства: {totalIncome - totalCharge} дн.ед.\n" +
                $"Процент успешных изделий: {Math.Round((selectionSet - failedDetails) / selectionSet * 100.0, 2)}%\n" +
                $"Средняя прибыль с детали: {Math.Round((totalIncome - totalCharge) / (selectionSet - failedDetails), 2)} дн.ед.\n");
        }

        class Detail
        {
            public bool IsTurningFailed;
            public bool IsGrindingFailed;
            public double Price;

            public Detail()
            {
                Price = materialPrice;
            }

            public void Turning()
            {
                Price += turningBaseCharge;
                double turningOutcome = randomizer.NextDouble();
                if (turningOutcome < 0.08)
                {
                    IsTurningFailed = true;
                    return;
                }
                if (turningOutcome < 0.2)
                {
                    Price += turningExtraCharge;
                }
            }

            public void Grinding()
            {
                double upperGrindingOutcome = randomizer.NextDouble();
                double lowerGrindingOutcome = randomizer.NextDouble();
                Price += grindingBaseCharge;

                if (upperGrindingOutcome < 0.03 && lowerGrindingOutcome < 0.06)
                {
                    IsGrindingFailed = true;
                    return;
                }
                if (upperGrindingOutcome < 0.03 || lowerGrindingOutcome < 0.06)
                {
                    Price += grindingExtraCharge;
                }
            }
        }
    }
}