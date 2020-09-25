using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Dynamic;

namespace BigDataFinalWork
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Precipitate> precipitates = new List<Precipitate>();
            List<Tuple<int, double>> maxPrecipitateStates = new List<Tuple<int, double>>();
            List<Tuple<int, double>> minPrecipitateStates = new List<Tuple<int, double>>();
            List<Tuple<int, double>> maxPrecipitatPeriod = new List<Tuple<int, double>>();
            List<Tuple<int, double>> minPrecipitatPeriod = new List<Tuple<int, double>>();
            precipitates = precipitateCsvToList();
            maxPrecipitateStates = maxPrecipitateYearStates(precipitates);
            Console.WriteLine("The maximum amount of precipitation fell in {0} the amount is: {1}", maxPrecipitateStates[0].Item1, maxPrecipitateStates[0].Item2);
            minPrecipitateStates = minPrecipitateYearStates(precipitates);
            Console.WriteLine("The minimum amount of precipitation that fell in {0} the amount is: {1}", minPrecipitateStates[0].Item1, minPrecipitateStates[0].Item2);
            Console.WriteLine("############################################################################");
            maxPrecipitatPeriod = maxPrecipitatePeriodStates(precipitates);
            Console.WriteLine("The maximum amount of precipitation in winter is in {0} and the amount is: {1}", maxPrecipitatPeriod[0].Item1, maxPrecipitatPeriod[0].Item2);
            Console.WriteLine("The maximum amount of precipitation in autumn is in {0} and the amount is: {1}", maxPrecipitatPeriod[1].Item1, maxPrecipitatPeriod[1].Item2);
            Console.WriteLine("The maximum amount of precipitation in spring is in {0} and the amount is: {1}", maxPrecipitatPeriod[2].Item1, maxPrecipitatPeriod[2].Item2);
            Console.WriteLine("The maximum amount of precipitation in summer is in {0} and the amount is: {1}", maxPrecipitatPeriod[3].Item1, maxPrecipitatPeriod[3].Item2);
            Console.WriteLine("############################################################################");
            minPrecipitatPeriod = minPrecipitatePeriodStates(precipitates);
            Console.WriteLine("The minimum amount of precipitation in winter is in {0} and the amount is: {1}", minPrecipitatPeriod[0].Item1, minPrecipitatPeriod[0].Item2);
            Console.WriteLine("The minimum amount of precipitation in autumn is in {0} and the amount is: {1}", minPrecipitatPeriod[1].Item1, minPrecipitatPeriod[1].Item2);
            Console.WriteLine("The minimum amount of precipitation in spring is in {0} and the amount is: {1}", minPrecipitatPeriod[2].Item1, minPrecipitatPeriod[2].Item2);
            Console.WriteLine("The minimum amount of precipitation in summer is in {0} and the amount is: {1}", minPrecipitatPeriod[3].Item1, minPrecipitatPeriod[3].Item2);
            Console.WriteLine("############################################################################");
            //Console.WriteLine(perennialAverage(precipitates));

            Console.ReadLine();
        }


        static List<Tuple<int, double>> maxPrecipitatePeriodStates(List<Precipitate> precipitates)
        {
            //create tuple {year, sum of the month's Winter,sum of the month's springs,....}
            List<Tuple<int, double, double, double, double>> periodPrecipitateSums = new List<Tuple<int, double, double, double, double>>();
            List<Tuple<int, double>> maxPrecipitatePeriodStates = new List<Tuple<int, double>>();
            double maxWinterPrecipitateAmount;
            double maxSummerPrecipitateAmount;
            double maxAutumnPrecipitateAmount;
            double maxSpringPrecipitateAmount;
            int maxWinterPrecipitateYear;
            int maxSummerPrecipitateYear;
            int maxAutumnPrecipitateYear;
            int maxSpringPrecipitateYear;

            //Going over the tuple and sum of the precipitate months period of each year
            for (int i = 0; i < precipitates.Count; i++)
            {
                periodPrecipitateSums.Add(new Tuple<int, double, double, double, double>(precipitates[i].year, precipitates[i].december +precipitates[i].january + precipitates[i].february , precipitates[i].march + precipitates[i].april + precipitates[i].may , precipitates[i].june + precipitates[i].july + precipitates[i].august , precipitates[i].september + precipitates[i].october + precipitates[i].november));
            }

            maxWinterPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item2).Max();
            maxAutumnPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item3).Max();
            maxSpringPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item4).Max();
            maxSummerPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item5).Max();
            int winterIndex = periodPrecipitateSums.FindIndex(t => t.Item2 == maxWinterPrecipitateAmount);
            int autumnIndex = periodPrecipitateSums.FindIndex(t => t.Item3 == maxAutumnPrecipitateAmount);
            int springIndex = periodPrecipitateSums.FindIndex(t => t.Item4 == maxSpringPrecipitateAmount);
            int summerIndex = periodPrecipitateSums.FindIndex(t => t.Item5 == maxSummerPrecipitateAmount);
            maxWinterPrecipitateYear = periodPrecipitateSums[winterIndex].Item1;
            maxAutumnPrecipitateYear = periodPrecipitateSums[autumnIndex].Item1;
            maxSpringPrecipitateYear = periodPrecipitateSums[springIndex].Item1;
            maxSummerPrecipitateYear = periodPrecipitateSums[summerIndex].Item1;

            //add the values of each period to tuple
            maxPrecipitatePeriodStates.Add(new Tuple<int, double>(maxWinterPrecipitateYear, maxWinterPrecipitateAmount));
            maxPrecipitatePeriodStates.Add(new Tuple<int, double>(maxAutumnPrecipitateYear, maxAutumnPrecipitateAmount));
            maxPrecipitatePeriodStates.Add(new Tuple<int, double>(maxSpringPrecipitateYear, maxSpringPrecipitateAmount));
            maxPrecipitatePeriodStates.Add(new Tuple<int, double>(maxSummerPrecipitateYear, maxSummerPrecipitateAmount));



            return maxPrecipitatePeriodStates;
        }

        static List<Tuple<int, double>> minPrecipitatePeriodStates(List<Precipitate> precipitates)
        {
            //create tuple {year, sum of the month's Winter,sum of the month's springs,....}
            List<Tuple<int, double, double, double, double>> periodPrecipitateSums = new List<Tuple<int, double, double, double, double>>();
            List<Tuple<int, double>> minPrecipitatePeriodStates = new List<Tuple<int, double>>();
            double minWinterPrecipitateAmount;
            double minSummerPrecipitateAmount;
            double minAutumnPrecipitateAmount;
            double minSpringPrecipitateAmount;
            int minWinterPrecipitateYear;
            int minSummerPrecipitateYear;
            int minAutumnPrecipitateYear;
            int minSpringPrecipitateYear;

            //Going over the tuple and sum of the precipitate months period of each year
            for (int i = 0; i < precipitates.Count; i++)
            {
                periodPrecipitateSums.Add(new Tuple<int, double, double, double, double>(precipitates[i].year, precipitates[i].december + precipitates[i].january + precipitates[i].february, precipitates[i].march + precipitates[i].april + precipitates[i].may, precipitates[i].june + precipitates[i].july + precipitates[i].august, precipitates[i].september + precipitates[i].october + precipitates[i].november));
            }
            minWinterPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item2).Min();
            minAutumnPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item3).Min();
            minSpringPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item4).Min();
            minSummerPrecipitateAmount = periodPrecipitateSums.Select(p => p.Item5).Min();
            int winterIndex = periodPrecipitateSums.FindIndex(t => t.Item2 == minWinterPrecipitateAmount);
            int autumnIndex = periodPrecipitateSums.FindIndex(t => t.Item3 == minAutumnPrecipitateAmount);
            int springIndex = periodPrecipitateSums.FindIndex(t => t.Item4 == minSpringPrecipitateAmount);
            int summerIndex = periodPrecipitateSums.FindIndex(t => t.Item5 == minSummerPrecipitateAmount);
            minWinterPrecipitateYear = periodPrecipitateSums[winterIndex].Item1;
            minAutumnPrecipitateYear = periodPrecipitateSums[autumnIndex].Item1;
            minSpringPrecipitateYear = periodPrecipitateSums[springIndex].Item1;
            minSummerPrecipitateYear = periodPrecipitateSums[summerIndex].Item1;

            //add the values of each period to tuple
            minPrecipitatePeriodStates.Add(new Tuple<int, double>(minWinterPrecipitateYear, minWinterPrecipitateAmount));
            minPrecipitatePeriodStates.Add(new Tuple<int, double>(minAutumnPrecipitateYear, minAutumnPrecipitateAmount));
            minPrecipitatePeriodStates.Add(new Tuple<int, double>(minSpringPrecipitateYear, minSpringPrecipitateAmount));
            minPrecipitatePeriodStates.Add(new Tuple<int, double>(minSummerPrecipitateYear, minSummerPrecipitateAmount));



            return minPrecipitatePeriodStates;
        }

        static List<Tuple<int, double>> maxPrecipitateYearStates(List<Precipitate> precipitates)
        {
            //create tuple {year, sum of the precipitate months of each year}
            List<Tuple<int, double>> precipitateSums = new List<Tuple<int,double>>();
            List<Tuple<int, double>> maxPrecipitateStates = new List<Tuple<int, double>>();
            double maxPrecipitateAmount;
            int maxPrecipitateYear;

            //Going over the tuple and sum of the precipitate months of each year
            for (int i = 0; i < precipitates.Count; i++)
            {
                precipitateSums.Add(new Tuple<int, double>(precipitates[i].year,precipitates[i].january + precipitates[i].february + precipitates[i].march + precipitates[i].april + precipitates[i].may + precipitates[i].june + precipitates[i].july + precipitates[i].august + precipitates[i].september + precipitates[i].october + precipitates[i].november + precipitates[i].december));
            }
            //find the max amount from the tuple
            maxPrecipitateAmount = precipitateSums.Select(p => p.Item2).Max();
            //find the max year from the tuple by amount
            int index = precipitateSums.FindIndex(t => t.Item2 == maxPrecipitateAmount);
            maxPrecipitateYear = precipitateSums[index].Item1;

            //Going over the tuple and find the max precipitate year and min precipitate year
            maxPrecipitateStates.Add(new Tuple<int, double>(maxPrecipitateYear,maxPrecipitateAmount));
            return maxPrecipitateStates;
        }

        static List<Tuple<int, double>> minPrecipitateYearStates(List<Precipitate> precipitates)
        {
            //create tuple {year, sum of the precipitate months of each year}
            List<Tuple<int, double>> precipitateSums = new List<Tuple<int, double>>();
            List<Tuple<int, double>> minPrecipitateStates = new List<Tuple<int, double>>();
            double minPrecipitateAmount;
            int minPrecipitateYear;


            //Going over the tuple and sum of the precipitate months of each year
            for (int i = 0; i < precipitates.Count; i++)
            {
                precipitateSums.Add(new Tuple<int, double>(precipitates[i].year, precipitates[i].january + precipitates[i].february + precipitates[i].march + precipitates[i].april + precipitates[i].may + precipitates[i].june + precipitates[i].july + precipitates[i].august + precipitates[i].september + precipitates[i].october + precipitates[i].november + precipitates[i].december));
            }
            //find the min amount from the tuple
            minPrecipitateAmount = precipitateSums.Select(p => p.Item2).Min();
            //find the min year from the tuple by amount
            int index = precipitateSums.FindIndex(t => t.Item2 == minPrecipitateAmount);
            minPrecipitateYear = precipitateSums[index].Item1;

            //Going over the tuple and find the min precipitate year and min precipitate year
            minPrecipitateStates.Add(new Tuple<int, double>(minPrecipitateYear, minPrecipitateAmount));

            return minPrecipitateStates;
        }

        static List<Precipitate> precipitateCsvToList(){

            List<Precipitate> result = File.ReadAllLines("C:\\DataFile_FinalExercise.csv")
             .Select(y => y.Split(','))
                    .Select(x => new {
                        year = int.Parse(x[0]),
                        januare = double.Parse(x[1]),
                        february = double.Parse(x[2]),
                        march = double.Parse(x[3]),
                        april = double.Parse(x[4]),
                        may = double.Parse(x[5]),
                        june = double.Parse(x[6]),
                        july = double.Parse(x[7]),
                        august = double.Parse(x[8]),
                        september = double.Parse(x[9]),
                        october = double.Parse(x[10]),
                        november = double.Parse(x[11]),
                        december = double.Parse(x[12])
                    }).Select(x => new Precipitate(x.year, x.januare, x.february, x.march, x.april, x.may, x.june, x.july, x.august, x.september, x.october, x.november, x.december))
               .ToList();
            return result;

        }

        //static double perennialAverage(List<Precipitate> precipitates, double perennialAverage)
        ///{
        //    List<Tuple<int, double>> precipitateSums = new List<Tuple<int, double>>();
        //    List<Tuple<int, int, int>> yearsOfDroughtTuple = new List<Tuple<int, int, int>>();
        //    int counter = 0;
        //    List<int> yearsOfDrought = new List<int>();
        //    for (int i = 0; i < precipitates.Count; i++)
        //    {
        //        precipitateSums.Add(new Tuple<int, double>(precipitates[i].year, precipitates[i].january + precipitates[i].february + precipitates[i].march + precipitates[i].april + precipitates[i].may + precipitates[i].june + precipitates[i].july + precipitates[i].august + precipitates[i].september + precipitates[i].october + precipitates[i].november + precipitates[i].december));
        //    }
        //    for (int i = 0; i < precipitateSums.Count(); i++)
        //    {
        //        while (counter < 3)
        //        {
        //            if (precipitateSums[i].Item2 < perennialAverage)
        //            {
        //                counter++;
        //                yearsOfDrought.Add(precipitateSums[i].Item1);
        //            }
        //            else
        //            {
        //                counter = 0;
        //                continue;
        //            }
        //        }


        //        counter = 0;


        //    }

        //}
    }
}
