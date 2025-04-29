using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassLibrary
{
    public class Date
    {
        protected int Year { get; set; }
        protected int Month { get; set; }
        protected int Day { get; set; }
        protected int Hours { get; set; }
        protected int Minutes { get; set; }
        public Date() { }

        public Date(int year, int month, int day, int hours, int minutes)
        {
            Year = year;
            Month = month;
            Day = day;
            Hours = hours;
            Minutes = minutes;
        }
        public Date(Date copy)
        {
            Year = copy.Year;
            Month = copy.Month;
            Day = copy.Day;
            Hours = copy.Hours;
            Minutes = copy.Minutes;
        }
        public int getYear() { return Year; }
        public int getMonth() { return Month; }
        public int getDay() { return Day; }
        public int getHours() { return Hours; }
        public int getMinutes() { return Minutes; }
    }
}
