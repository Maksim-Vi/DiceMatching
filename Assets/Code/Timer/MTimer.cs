using System;
using UnityEngine;

namespace Matching.Timer
{
    public class MTimer
    {
        private float TimeRemaining = 0;
        private bool timerIsRunning = false;
        private DisplayTime DisplayTimeData = new DisplayTime();
        
        private Action<DisplayTime> Callback;

        public float StartTimer(long timeRemaining, Action<DisplayTime> callback = null)
        {
            if (callback != null)
                Callback = callback;
            
            long currentTime = Current();

            if (timeRemaining > currentTime)
            {
                timerIsRunning = true;
                TimeRemaining = timeRemaining - currentTime;

                return TimeRemaining;
            }
            
            return -1;
        }

        public void StopTimer()
        {
            timerIsRunning = false;
        }

        public void UpdateTimer()
        {
            if (timerIsRunning)
            {
                if (TimeRemaining > 0)
                {
                    TimeRemaining -= Time.deltaTime;
                    CalcTime(TimeRemaining);
                }
                else
                {
                    TimeRemaining = 0;
                    timerIsRunning = false;
                }
            }
        }

        public long Current()
        {
            return DateTimeOffset.Now.ToUniversalTime().ToUnixTimeSeconds();
        }

        public long CurrentInMs()
        {
            return DateTimeOffset.Now.ToUniversalTime().ToUnixTimeMilliseconds();
        }

        public long GetTimeWithAdd(int seconds)
        { 
            DateTime currentTime = DateTime.UtcNow;
            DateTime futureTime = currentTime.AddSeconds(seconds);
            return new DateTimeOffset(futureTime).ToUnixTimeSeconds();
        }

        public void CalcTime(float timeToDisplay)
        {
            float days = Mathf.FloorToInt(timeToDisplay / 86400);
            timeToDisplay -= days * 86400;
            float hours = Mathf.FloorToInt(timeToDisplay / 3600);
            timeToDisplay -= hours * 3600;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            timeToDisplay -= minutes * 60;
            float seconds = Mathf.FloorToInt(timeToDisplay);

            DisplayTimeData.days = days;
            DisplayTimeData.hours = hours;
            DisplayTimeData.minutes = minutes;
            DisplayTimeData.seconds = seconds;

            DispalyTimeData();
        }
        
        public string GetTimeText()
        {
            if (DisplayTimeData.days > 0)
            {
                string day = DisplayTimeData.days > 9 ? $"{DisplayTimeData.days}" : $"0{DisplayTimeData.days}";
                string hour = DisplayTimeData.hours > 9 ? $"{DisplayTimeData.hours}" : $"0{DisplayTimeData.hours}";

                return $"{day}d : {hour}h";
            }
            else if (DisplayTimeData.hours > 0)
            {
                string hour = DisplayTimeData.hours > 9 ? $"{DisplayTimeData.hours}" : $"0{DisplayTimeData.hours}";
                string minute = DisplayTimeData.minutes > 9
                    ? $"{DisplayTimeData.minutes}"
                    : $"0{DisplayTimeData.minutes}";

                return $"{hour}h : {minute}m";
            }
            else if (DisplayTimeData.minutes > 0)
            {
                string minute = DisplayTimeData.minutes > 9
                    ? $"{DisplayTimeData.minutes}"
                    : $"0{DisplayTimeData.minutes}";
                string second = DisplayTimeData.seconds > 9
                    ? $"{DisplayTimeData.seconds}"
                    : $"0{DisplayTimeData.seconds}";

                return $"{minute}m : {second}s";
            }
            else if (DisplayTimeData.seconds > 0)
            {
                string second = DisplayTimeData.seconds > 9
                    ? $"{DisplayTimeData.seconds}"
                    : $"0{DisplayTimeData.seconds}";

                return $"00m : {second}s";
            }

            return "00:00";
        }

        public bool IsFinishedTime()
        {
            if (DisplayTimeData.days == 0 && DisplayTimeData.hours == 0 && DisplayTimeData.minutes == 0 && DisplayTimeData.seconds == 0)
            {
                return true;
            }
            
            return false;
        }
        
        public DisplayTime DispalyTimeData()
        {
            Callback(DisplayTimeData);
            return DisplayTimeData;
        }
    }
    
    public class DisplayTime
    {
        public float days;
        public float hours;
        public float minutes;
        public float seconds;
    }
}