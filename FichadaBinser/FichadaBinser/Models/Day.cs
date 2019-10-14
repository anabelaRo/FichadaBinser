using System;

namespace FichadaBinser.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? StartLunchTime { get; set; }
        public DateTime? EndLunchTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public string TotalTime
        {
            get
            {
                int totalSeconds = 0;

                if (this.EntryTime != null)
                {
                    if (this.StartLunchTime != null)
                    {
                        totalSeconds += Convert.ToInt32(this.StartLunchTime.Value.Subtract(this.EntryTime.Value).TotalSeconds);

                        if (this.EndLunchTime != null)
                        {
                            if (this.ExitTime != null)
                            {
                                totalSeconds += Convert.ToInt32(this.ExitTime.Value.Subtract(this.EndLunchTime.Value).TotalSeconds);
                            }
                            else
                            {
                                totalSeconds += Convert.ToInt32(DateTime.Now.Subtract(this.EndLunchTime.Value).TotalSeconds);
                            }
                        }
                    }
                    else
                    {
                        if (this.ExitTime != null)
                        {
                            totalSeconds += Convert.ToInt32(this.ExitTime.Value.Subtract(this.EntryTime.Value).TotalSeconds);
                        }
                        else
                        {
                            totalSeconds += Convert.ToInt32(DateTime.Now.Subtract(this.EntryTime.Value).TotalSeconds);
                        }
                    }
                }

                TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

                return time.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
