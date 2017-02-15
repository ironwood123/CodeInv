using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.IO;
using System.Configuration;
namespace WeatherAnalysis
{
    public class Weather2016
    {
        public List<DayInfo> Weather2016Data(string fileName)
        {
            List<DayInfo> tblWeather2016 = new List<DayInfo>();

            using (var fs = File.OpenRead(fileName))
            using (var reader = new StreamReader(fs))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');                  
                    tblWeather2016.Add(new DayInfo {theDay= Convert.ToDateTime(values[0]), MaxTemp = Convert.ToInt16(values[1]), MinTemp = Convert.ToInt16(values[2]),AvgTemp= Convert.ToDecimal(values[3]), Precipitation= Convert.ToDecimal(values[4]) });
                }
            }
            return tblWeather2016;
        }
    }
    public class DayInfo
    {
        public DateTime theDay { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public decimal AvgTemp { get; set; }
        public decimal Precipitation { get; set; }
    }

    public class WeatherCalculationUtility
    {
        public void WeatherCalculator()
        {
            List<DayInfo> top10HotDays = new List<DayInfo>();
            List<DayInfo> top10ColdDays = new List<DayInfo>();
            List<DayInfo> top10WetDays = new List<DayInfo>();
            decimal springAvg = 0m;
            decimal summerAvg = 0m;
            decimal fallAvg = 0m;
            decimal winterAvg = 0m;
            decimal springAvgRF = 0m;
            decimal summerAvgRF = 0m;
            decimal fallAvgRF = 0m;
            decimal winterAvgRF = 0m;

            try
            {
                Weather2016 weather2061 = new Weather2016();
                string filename = ConfigurationManager.AppSettings["WeatherFile"];
                List<DayInfo> tblWeather = weather2061.Weather2016Data(filename);
                top10HotDays = tblWeather.OrderByDescending(x => x.MaxTemp).Take(10).ToList();
                top10ColdDays = tblWeather.OrderBy(x => x.MinTemp).Take(10).ToList();
                top10WetDays = tblWeather.OrderByDescending(x => x.Precipitation).Take(10).ToList();

                springAvg = (from m in tblWeather
                             where m.theDay > Convert.ToDateTime("03/20/2016") && m.theDay <= Convert.ToDateTime("06/21/2016")
                             select m.AvgTemp).DefaultIfEmpty().Average();
                summerAvg = (from m in tblWeather
                             where m.theDay > Convert.ToDateTime("06/21/2016") && m.theDay <= Convert.ToDateTime("09/23/2016")
                             select m.AvgTemp).DefaultIfEmpty().Average();
                fallAvg = (from m in tblWeather
                           where m.theDay > Convert.ToDateTime("09/23/2016") && m.theDay <= Convert.ToDateTime("12/21/2016")
                           select m.AvgTemp).DefaultIfEmpty().Average();

                winterAvg = (from m in tblWeather
                             where (m.theDay > Convert.ToDateTime("12/21/2016") && m.theDay <= Convert.ToDateTime("12/31/2016"))
                                 || (m.theDay > Convert.ToDateTime("1/1/2016") && m.theDay <= Convert.ToDateTime("3/20/2016"))
                             select m.AvgTemp).DefaultIfEmpty().Average();

                springAvgRF = (from m in tblWeather
                               where m.theDay > Convert.ToDateTime("03/20/2016") && m.theDay <= Convert.ToDateTime("06/21/2016")
                               select m.Precipitation).DefaultIfEmpty().Average();

                summerAvgRF = (from m in tblWeather
                               where m.theDay > Convert.ToDateTime("06/21/2016") && m.theDay <= Convert.ToDateTime("09/23/2016")
                               select m.Precipitation).DefaultIfEmpty().Average();

                fallAvgRF = (from m in tblWeather
                             where m.theDay > Convert.ToDateTime("09/23/2016") && m.theDay <= Convert.ToDateTime("12/21/2016")
                             select m.Precipitation).DefaultIfEmpty().Average();

                winterAvgRF = (from m in tblWeather
                             where (m.theDay > Convert.ToDateTime("12/21/2016") && m.theDay <= Convert.ToDateTime("12/31/2016"))
                                 || (m.theDay > Convert.ToDateTime("1/1/2016") && m.theDay <= Convert.ToDateTime("3/20/2016"))
                             select m.Precipitation).DefaultIfEmpty().Average();

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            finally
            {
                Console.WriteLine("Average Temperature of Spring: " + Math.Round(springAvg, 2));
                Console.WriteLine("Average Temperature of Summer: " + Math.Round(summerAvg, 2));
                Console.WriteLine("Average Temperature of Fall: " + Math.Round(fallAvg, 2));
                Console.WriteLine("Average Temperature of Winter: " + Math.Round(winterAvg, 2));
                Console.WriteLine();

                Console.WriteLine("Average rain fall of Spring: " + Math.Round(springAvgRF, 2));
                Console.WriteLine("Average rain fall of Summer: " + Math.Round(summerAvgRF, 2));
                Console.WriteLine("Average rain fall of Fall: " + Math.Round(fallAvgRF, 2));
                Console.WriteLine("Average rain fall of Winter: " + Math.Round(winterAvgRF, 2));
                Console.WriteLine();

                Console.WriteLine("Top 10 Hottest Days");
                for (int i = 0; i < top10HotDays.Count; i++)
                {
                    Console.WriteLine(top10HotDays[i].theDay.ToString().Substring(0,9));
                }
                Console.WriteLine();
                Console.WriteLine("Top 10 Coldest Days");
                for (int i = 0; i < top10ColdDays.Count; i++)
                {
                    Console.WriteLine(top10ColdDays[i].theDay.ToString().Substring(0, 9));
                }
                Console.WriteLine();
                Console.WriteLine("Top 10 Wettest Days");
                for (int i = 0; i < top10WetDays.Count; i++)
                {
                    Console.WriteLine(top10WetDays[i].theDay.ToString().Substring(0, 9));
                }

                Console.ReadLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            WeatherCalculationUtility WC = new WeatherCalculationUtility();
            WC.WeatherCalculator();
        }
    }
}
