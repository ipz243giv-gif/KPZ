using System;

namespace SimpleClassLibrary
{
    public class Airplane
    {
        protected string StartCity { get; set; }
        protected string FinishCity { get; set; }
        protected Date StartDate { get; set; }
        protected Date FinishDate { get; set; }

        protected double FlightDistanceM { get; set; }

        public Airplane() { }

        public Airplane(string startCity, string finishCity, Date startDate, Date finishDate, double distanceMeters)
        {
            StartCity = startCity;
            FinishCity = finishCity;
            StartDate = startDate;
            FinishDate = finishDate;
            FlightDistanceM = distanceMeters;
        }

        public Airplane(Airplane copy)
        {
            StartCity = copy.StartCity;
            FinishCity = copy.FinishCity;
            StartDate = new Date(copy.StartDate);
            FinishDate = new Date(copy.FinishDate);
            FlightDistanceM = copy.FlightDistanceM;
        }

        public int GetTotalTime()
        {
            int startTotalMinutes = StartDate.getDay() * 24 * 60 + StartDate.getHours() * 60 + StartDate.getMinutes();
            int finishTotalMinutes = FinishDate.getDay() * 24 * 60 + FinishDate.getHours() * 60 + FinishDate.getMinutes();
            return finishTotalMinutes - startTotalMinutes;
        }

        public bool IsArrivingToday()
        {
            return StartDate.getYear() == FinishDate.getYear() &&
                   StartDate.getMonth() == FinishDate.getMonth() &&
                   StartDate.getDay() == FinishDate.getDay();
        }

        public string getStartCity() { return StartCity; }
        public string getFinishCity() { return FinishCity; }
        public Date getStartDate() { return StartDate; }
        public Date getFinishDate() { return FinishDate; }

        public double getFlightDistanceM() { return FlightDistanceM; }

        public double getFlightDistanceKM()
        {
            return FlightDistanceM / 1000.0;
        }

        public double getFlightDistanceMiles()
        {
            return FlightDistanceM / 1609.34;
        }
    }
}