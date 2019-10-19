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
            this.Date = date;
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

                if (this.EntryTime != null)
                {
                    if (this.StartLunchTime != null)
                    {
                        totalSeconds += Convert.ToInt32(this.StartLunchTime.Value.ToLocalTime().Subtract(this.EntryTime.Value.ToLocalTime()).TotalSeconds);

                        if (this.EndLunchTime != null)
                        {
                            if (this.ExitTime != null)
                            {
                                totalSeconds += Convert.ToInt32(this.ExitTime.Value.ToLocalTime().Subtract(this.EndLunchTime.Value.ToLocalTime()).TotalSeconds);
                            }
                            else
                            {
                                totalSeconds += Convert.ToInt32(DateTime.Now.ToLocalTime().Subtract(this.EndLunchTime.Value.ToLocalTime()).TotalSeconds);
                            }
                        }
                    }
                    else
                    {
                        if (this.ExitTime != null)
                        {
                            totalSeconds += Convert.ToInt32(this.ExitTime.Value.ToLocalTime().Subtract(this.EntryTime.Value.ToLocalTime()).TotalSeconds);
                        }
                        else
                        {
                            totalSeconds += Convert.ToInt32(DateTime.Now.ToLocalTime().Subtract(this.EntryTime.Value.ToLocalTime()).TotalSeconds);
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
                TimeSpan time = TimeSpan.FromSeconds(this.TotalTime);

                return time.ToString(@"hh\:mm\:ss");
            }
        }

        [Ignore]
        public string EntryTimeString
        {
            get
            {
                if (this.EntryTime != null)
                    return this.EntryTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string StartLunchTimeString
        {
            get
            {
                if (this.StartLunchTime != null)
                    return this.StartLunchTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string EndLunchTimeString
        {
            get
            {
                if (this.EndLunchTime != null)
                    return this.EndLunchTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        [Ignore]
        public string ExitTimeString
        {
            get
            {
                if (this.ExitTime != null)
                    return this.ExitTime.Value.ToLocalTime().ToString("HH:mm:ss");

                return null;
            }
        }

        public override int GetHashCode()
        {
            return this.DayId != null 
                ? (int)this.DayId 
                : 0;
        }
    }
}
