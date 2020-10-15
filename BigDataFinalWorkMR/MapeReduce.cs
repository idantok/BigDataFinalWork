using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Dynamic;
using System.Diagnostics;
using BigDataFinalWork;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace BigDataFinalWorkNMR
{
   
    class MapeReduce
    {
        static void Main(string[] args)
        {
            List<Precipitate> precipitatesOne = new List<Precipitate>();
            List<Precipitate> precipitatesTwo = new List<Precipitate>();
            List<Precipitate> precipitatesThree = new List<Precipitate>();
            List<List<Tuple<int, double>>> threadOneStatesList = new List<List<Tuple<int, double>>>();
            List<List<Tuple<int, double>>> threadTwoStatesList = new List<List<Tuple<int, double>>>();
            List<List<Tuple<int, double>>> threadThreeStatesList = new List<List<Tuple<int, double>>>();
            List<List<Tuple<int, double>>> results = new List<List<Tuple<int, double>>>();
            double plAverage;
            divPrecipitateToLists();
            Task t1 = threadOne();
            Task t2 = threadTwo();
            Task t3 = threadThree();
            List<Tuple<int, double>> maxPrecipitateStates = new List<Tuple<int, double>>();
            List<Tuple<int, double>> minPrecipitateStates = new List<Tuple<int, double>>();
            List<Tuple<int, double>> maxPrecipitatPeriod = new List<Tuple<int, double>>();
            List<Tuple<int, double>> minPrecipitatPeriod = new List<Tuple<int, double>>();

            Process proc = Process.GetCurrentProcess();
            DateTime dt = DateTime.Now;

            //wait until all the threads finish and then take care in them
            Task.WhenAll(t1,t2,t3).ContinueWith(t => {

                double maxPrecipitateStatesNumber = Math.Max(threadOneStatesList[0][0].Item2, Math.Max(threadTwoStatesList[0][0].Item2, threadThreeStatesList[0][0].Item2));
                    double minPrecipitateStatesNumber = Math.Min(threadOneStatesList[1][0].Item2, Math.Min(threadTwoStatesList[1][0].Item2, threadThreeStatesList[1][0].Item2));
                    double maxWinterPrecipitatNumber = Math.Max(threadOneStatesList[2][0].Item2, Math.Max(threadTwoStatesList[2][0].Item2, threadThreeStatesList[2][0].Item2));
                    double maxAutumnPrecipitatNumber = Math.Max(threadOneStatesList[2][1].Item2, Math.Max(threadTwoStatesList[2][1].Item2, threadThreeStatesList[2][1].Item2));
                    double maxSpringPrecipitatNumber = Math.Max(threadOneStatesList[2][2].Item2, Math.Max(threadTwoStatesList[2][2].Item2, threadThreeStatesList[2][2].Item2));
                    double maxSummerPrecipitatNumber = Math.Max(threadOneStatesList[2][3].Item2, Math.Max(threadTwoStatesList[2][3].Item2, threadThreeStatesList[2][3].Item2));
                    double minWinterPrecipitatNumber = Math.Min(threadOneStatesList[3][0].Item2, Math.Min(threadTwoStatesList[3][0].Item2, threadThreeStatesList[3][0].Item2));
                    double minAutumnPrecipitatNumber = Math.Min(threadOneStatesList[3][1].Item2, Math.Min(threadTwoStatesList[3][1].Item2, threadThreeStatesList[3][1].Item2));
                    double minSpringPrecipitatNumber = Math.Min(threadOneStatesList[3][2].Item2, Math.Min(threadTwoStatesList[3][2].Item2, threadThreeStatesList[3][2].Item2));
                    double minSummerPrecipitatNumber = Math.Min(threadOneStatesList[3][3].Item2, Math.Min(threadTwoStatesList[3][3].Item2, threadThreeStatesList[3][3].Item2));
                    Console.WriteLine("The maximum amount of precipitation fell in  the amount is: {0}", maxPrecipitateStatesNumber);
                    Console.WriteLine("The minimum amount of precipitation that fell in the amount is: {0}", minPrecipitateStatesNumber);
                    Console.WriteLine("############################################################################");
                    Console.WriteLine("The maximum amount of precipitation in winter is: {0}", maxWinterPrecipitatNumber);
                    Console.WriteLine("The maximum amount of precipitation in autumn  is: {0}", maxAutumnPrecipitatNumber);
                    Console.WriteLine("The maximum amount of precipitation in spring is: {0}", maxSpringPrecipitatNumber);
                    Console.WriteLine("The maximum amount of precipitation in summer is: {0}", maxSummerPrecipitatNumber);
                    Console.WriteLine("############################################################################");
                    Console.WriteLine("The minimum amount of precipitation in winter is: {0}", minWinterPrecipitatNumber);
                    Console.WriteLine("The minimum amount of precipitation in autumn is: {0}", minAutumnPrecipitatNumber);
                    Console.WriteLine("The minimum amount of precipitation in spring is: {0}", minSpringPrecipitatNumber);
                    Console.WriteLine("The minimum amount of precipitation in  summer is: {0}", minSummerPrecipitatNumber);
                    Console.WriteLine("############################################################################");
                    Console.WriteLine("the perennial Average is :{0}", plAverage);
                    TimeSpan ts = DateTime.Now - dt;
                    Console.WriteLine("Time spend running program :{0} ms", ts.TotalMilliseconds.ToString());
                    Console.WriteLine("the memory used for the process is approximately : {0} KB", proc.PrivateMemorySize64 / 1000);
                    Console.ReadLine();
                });

            Task threadOne()
            {
                return Task.Run(() => {
                    List<Tuple<int, double>> maxPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> maxPrecipitatPeriodThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitatPeriodThread = new List<Tuple<int, double>>();

                    maxPrecipitateStatesThread = Program.maxPrecipitateYearStates(precipitatesOne);
                    minPrecipitateStatesThread = Program.minPrecipitateYearStates(precipitatesOne);
                    maxPrecipitatPeriodThread = Program.maxPrecipitatePeriodStates(precipitatesOne);
                    maxPrecipitatPeriodThread = Program.minPrecipitatePeriodStates(precipitatesOne);

                    threadOneStatesList.Add(maxPrecipitateStatesThread);
                    threadOneStatesList.Add(minPrecipitateStatesThread);
                    threadOneStatesList.Add(maxPrecipitatPeriodThread);
                    threadOneStatesList.Add(maxPrecipitatPeriodThread);
                    
                });
            }

            Task threadTwo()
            {
                return Task.Run(() => {
                    List<Tuple<int, double>> maxPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> maxPrecipitatPeriodThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitatPeriodThread = new List<Tuple<int, double>>();

                    maxPrecipitateStatesThread = Program.maxPrecipitateYearStates(precipitatesTwo);
                    minPrecipitateStatesThread = Program.minPrecipitateYearStates(precipitatesTwo);
                    maxPrecipitatPeriodThread = Program.maxPrecipitatePeriodStates(precipitatesTwo);
                    maxPrecipitatPeriodThread = Program.minPrecipitatePeriodStates(precipitatesTwo);

                    threadTwoStatesList.Add(maxPrecipitateStatesThread);
                    threadTwoStatesList.Add(minPrecipitateStatesThread);
                    threadTwoStatesList.Add(maxPrecipitatPeriodThread);
                    threadTwoStatesList.Add(maxPrecipitatPeriodThread);

                });
            }

            Task threadThree()
            {
                return Task.Run(() => {
                    List<Tuple<int, double>> maxPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitateStatesThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> maxPrecipitatPeriodThread = new List<Tuple<int, double>>();
                    List<Tuple<int, double>> minPrecipitatPeriodThread = new List<Tuple<int, double>>();

                    maxPrecipitateStatesThread = Program.maxPrecipitateYearStates(precipitatesThree);
                    minPrecipitateStatesThread = Program.minPrecipitateYearStates(precipitatesThree);
                    maxPrecipitatPeriodThread = Program.maxPrecipitatePeriodStates(precipitatesThree);
                    maxPrecipitatPeriodThread = Program.minPrecipitatePeriodStates(precipitatesThree);

                    threadThreeStatesList.Add(maxPrecipitateStatesThread);
                    threadThreeStatesList.Add(minPrecipitateStatesThread);
                    threadThreeStatesList.Add(maxPrecipitatPeriodThread);
                    threadThreeStatesList.Add(maxPrecipitatPeriodThread);

                });
            }

            void divPrecipitateToLists()
            {
                List<Precipitate> precipitates = new List<Precipitate>();
                precipitates = Program.precipitateCsvToList();
                plAverage =  Program.perennialAverage(precipitates);
                precipitatesOne.AddRange(precipitates.Take(precipitates.Count()/3));
                precipitatesTwo.AddRange(precipitates.Skip(precipitates.Count() / 3).Take(precipitates.Count() / 3));
                precipitatesThree.AddRange(precipitates.Skip((precipitates.Count() / 3)*2));
            }

            Console.ReadLine();
        }
         
    }
}