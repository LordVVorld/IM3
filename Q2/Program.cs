using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2
{
    internal class Program
    {
        public static Random randomizer = new Random();
        private const double l = 1.2;
        private const double m = 1.0 / 2.0;
        private const int selectionSet = 100;
        private const int T = 8 * 60;

        static void Main(string[] args)
        {
            double P0 = m / (l + m);
            double P1 = l / (l + m);
            double A = l * P0;
            double fi = l / m;

            Console.WriteLine($"Вероятность обслуживания: {Math.Round(P0, 2) }");
            Console.WriteLine($"Вероятность отказа: {Math.Round(P1, 2)}");
            Console.WriteLine($"Пропускная способность: {Math.Round(A, 2)}");
            Console.WriteLine($"Коэффициент загрузки: {Math.Round(fi, 2)}");

            Modeling();
        }

        static void Modeling()
        {
            double[] callCount = new double[selectionSet];
            double[] answeredCalls = new double[selectionSet];
            for (int dayIndex = 0; dayIndex < selectionSet; dayIndex++)
            {
                PhoneLine line = new PhoneLine();
                for (int t = 0; t < T; t++)
                {
                    CallStream stream = new CallStream();
                    callCount[dayIndex] += stream.calls.Count;
                    foreach (var call in stream.calls)
                    {
                        if (line.IsFree && call.ReceptionTime >= (1 + line.TimeTillFree))
                        {
                            line.GetCall(call);
                        }
                        line.ProcessCall();
                        if (line.TimeTillFree >= 0)
                        {
                            break;
                        }
                    }
                }
                answeredCalls[dayIndex] = line.CallCount;
            }
            Console.WriteLine($"\nЗвонков в минуту в среднем: {Math.Round(callCount.Average() / T, 2)}");
            Console.WriteLine($"Вероятность обслуживания: {Math.Round(answeredCalls.Average() / callCount.Average(), 2)}");
        }

        public class Call
        {
            public double Duration;
            public double ReceptionTime;

            public Call()
            {
                Duration = randomizer.NextDouble() * 4;
                ReceptionTime = randomizer.NextDouble();
            }
        }

        public class CallStream
        {
            public List<Call> calls;

            public CallStream()
            {
                var callCount = Math.Round(randomizer.NextDouble() * 2 * l);
                calls = new List<Call>();
                for (int index = 0; index < callCount; index++)
                {
                    calls.Add(new Call());
                }
                calls.OrderBy(call => call.ReceptionTime);
            }
        }

        public class PhoneLine
        {
            public bool IsFree;
            public int CallCount;
            public double TimeTillFree;
            public Call ActualCall;

            public PhoneLine()
            {
                IsFree = true;
                CallCount = 0;
                TimeTillFree = 0;
            }

            public void GetCall(Call call)
            {
                IsFree = false;
                ActualCall = call;
                TimeTillFree = call.Duration;
                ++CallCount;
            }

            public void ProcessCall()
            {
                --TimeTillFree;
                if (TimeTillFree <= 0)
                {
                    IsFree = true;
                }
            }
        }
    }
}
