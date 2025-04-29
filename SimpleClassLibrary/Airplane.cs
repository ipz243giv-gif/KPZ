using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassLibrary
{
    public class Airplane
    {
        protected string StartCity { get; set; }
        protected string FinishCity { get; set; }
        protected Date StartDate { get; set; }
        protected Date FinishDate { get; set; }
        protected double FlightDistanceKM {get; set; }
        protected double FlightDistanceM { get; set; }
        protected double FlightDistanceMiles { get; set; }
        public Airplane() { }
        public Airplane(string startCity, string finishCity, Date startDate, Date finishDate, double flightDistanceKM, double flightDistanceM, double flightDistanceMiles)
        {
            StartCity = startCity;
            FinishCity = finishCity;
            StartDate = startDate;
            FinishDate = finishDate;
            FlightDistanceKM = flightDistanceKM;
            FlightDistanceM = flightDistanceM;
            FlightDistanceMiles = flightDistanceMiles;
        }
        public Airplane(Airplane copy)
        {
            StartCity = copy.StartCity;
            FinishCity = copy.FinishCity;
            StartDate = new Date(copy.StartDate);
            FinishDate = new Date(copy.FinishDate);
            FlightDistanceKM = copy.FlightDistanceKM;
            FlightDistanceM = copy.FlightDistanceM;
            FlightDistanceMiles = copy.FlightDistanceMiles;
        }
        public int GetTotalTime()
        {
            int startTotalMinutes = StartDate.getDay() * 24 * 60 + StartDate.getHours() * 60 + StartDate.getMinutes();
            int finishTotalMinutes = FinishDate.getDay() * 24 * 60 + FinishDate.getHours() * 60 + FinishDate.getMinutes();

            return finishTotalMinutes - startTotalMinutes;
        }
        public bool IsArrivingToday()
        {
            return StartDate.getYear() == FinishDate.getYear() && StartDate.getMonth() == FinishDate.getMonth() && StartDate.getDay() == FinishDate.getDay();
        }
        public string getStartCity() { return StartCity; }
        public string getFinishCity() { return FinishCity; }
        public Date getStartDate() { return StartDate; }
        public Date getFinishDate() { return FinishDate; }
        public double getFlightDistanceKM() { return FlightDistanceKM; }
        public double getFlightDistanceMiles() { return FlightDistanceMiles; }
        public double getFlightDistanceM() { return FlightDistanceM; }
    }
}