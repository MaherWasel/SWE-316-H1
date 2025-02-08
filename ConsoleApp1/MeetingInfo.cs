using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MeetingInfo
    {
        private string days;
        private string startTime;
        private string endTime;
        private Location location;

        public MeetingInfo(string days, string startTime, string endTime, Location location)
        {
            this.days = days;
            this.startTime = startTime;
            this.endTime = endTime;
            this.location = location;
        }

        public string GetDays() => days;
        public string GetStartTime() => startTime;
        public string GetEndTime() => endTime;
        public Location GetLocation() => location;
    }
}
