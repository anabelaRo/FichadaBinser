using FichadaBinser.Helpers;
using SQLite.Net.Attributes;
using System;

namespace FichadaBinser.Models
{
    public class Day
    {
        public Day()
        {

        }

        public Day(DateTime date)
        {
            Date = date;
        }

        [PrimaryKey]
        public int? DayId { get; set; }

        public DateTime Date { get; private set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? StartLunchTime { get; set; }
        public DateTime? EndLunchTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public int TotalTime
        {
            get
            {
                int totalSeconds = 0;

                if (EntryTime != null)
                {
                    if (StartLunchTime != null)
                    {
                        totalSeconds += Convert.ToInt32(StartLunchTime.Value.ToLocalTime().Subtract(EntryTime.Value.ToLocalTime()).TotalSeconds);

                        if (EndLunchTime != null)
                        {
                            if (ExitTime != null)
                            {
                                totalSeconds += Convert.ToInt32(ExitTime.Value.ToLocalTime().Subtract(EndLunchTime.Value.ToLocalTime()).TotalSeconds);
                            }
                            else
                            {
                                totalSeconds += Convert.ToInt32(DateTime.Now.ToLocalTime().Subtract(EndLunchTime.Value.ToLocalTime()).TotalSeconds);
                            }
                        }
                    }
                    else
                    {
                        if (ExitTime != null)
                        {
                            totalSeconds += Convert.ToInt32(ExitTime.Value.ToLocalTime().Subtract(EntryTime.Value.ToLocalTime()).TotalSeconds);
                        }
                        else
                        {
                            totalSeconds += Convert.ToInt32(DateTime.Now.ToLocalTime().Subtract(EntryTime.Value.ToLocalTime()).TotalSeconds);
                        }
                    }
                }

                return totalSeconds;
            }
        }

        [Ignore]
        public string TotalTimeString
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(TotalTime);

                return time.ToString(@"hh\:mm\:ss");
            }
        }

        [Ignore]
        public string EntryTimeString
        {
            get
            {
                if (EntryTime != null)
                    return EntryTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string StartLunchTimeString
        {
            get
            {
                if (StartLunchTime != null)
                    return StartLunchTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string EndLunchTimeString
        {
            get
            {
                if (EndLunchTime != null)
                    return EndLunchTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string ExitTimeString
        {
            get
            {
                if (ExitTime != null)
                    return ExitTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        public override int GetHashCode()
        {
            return DayId != null 
                ? (int)DayId 
                : 0;
        }
    }
}
