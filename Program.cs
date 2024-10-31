using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

class ShowTime {
    static void Main(string[] args) 
    {
        Console.WriteLine("startTime | duration");
        List<string> times = new List<string>();
        while (true) 
        {
            string time = Console.ReadLine();
            if (time == "q") {
                break;
            }
            times.Add(time);
        }
        Console.WriteLine("Working Times");
        string worktime = Console.ReadLine();

        Console.WriteLine("Consultation Time");
        int consTime = int.Parse(Console.ReadLine());

        int leng = times.Count;
        TimeSpan[] tim = new TimeSpan[leng];
        int[] duration = new int[leng];
        for (int i = 0; i < leng; i++)
        {
            tim[i] = TimeSpan.Parse(times[i].Split(' ')[0]);
            duration[i] = int.Parse(times[i].Split(' ')[1]);
        }
        
        string startTime = worktime.Split('-')[0];
        string endTime = worktime.Split('-')[1];
        TimeSpan beginDay = new TimeSpan(int.Parse(startTime.Split(':')[0]), int.Parse(startTime.Split(':')[1]), 00);
        TimeSpan endDay = new TimeSpan(int.Parse(endTime.Split(':')[0]), int.Parse(endTime.Split(':')[1]), 00);
        Console.WriteLine(String.Join("\n", AvailablePeriods(tim, duration, beginDay, endDay, consTime)));
    }

        static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            var result = new List<string>();
 
            var currentTime = beginWorkingTime;
            var indexStartInterval = 0;
 
            while (currentTime < endWorkingTime && indexStartInterval < startTimes.Length)
            {
                var duration = durations[indexStartInterval];
 
                if ((currentTime <= startTimes[indexStartInterval])
                 && ((startTimes[indexStartInterval]-currentTime) >= new TimeSpan(0, consultationTime, 0)))
                {
                    var interval = GetIntervalString(currentTime, currentTime+TimeSpan.FromMinutes(consultationTime));
                    result.Add(interval);
                    currentTime = currentTime.Add(new TimeSpan(0, consultationTime, 0));
                }
                else 
                {
                    currentTime = startTimes[indexStartInterval] + new TimeSpan(0, duration, 0);
                    indexStartInterval++;
                }
 
            }
            
            if (currentTime < endWorkingTime && (endWorkingTime - currentTime) >= new TimeSpan(0, consultationTime, 0))
            {
                var interval = GetIntervalString(currentTime, currentTime+TimeSpan.FromMinutes(consultationTime));
                result.Add(interval);
            }
 
            return result.ToArray();
        }
 
        static private string GetIntervalString(TimeSpan currentTime, TimeSpan timeSpan)
        {
            //HH:mm-HH:mm
            return $"{currentTime.Hours:00}:{currentTime.Minutes:00}-{timeSpan.Hours:00}:{timeSpan.Minutes:00}";
        }
}