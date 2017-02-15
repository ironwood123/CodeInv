using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Web;
namespace InterestPayment
{
    public struct BalanceData
    {
        public string month;
        public decimal startBalance;
        public decimal endBalance;
    }

    public class BalanceCalculator
    {
        public decimal EndBalance(decimal startBalance, decimal interestRate)
        {
            decimal endBal = 0;
            endBal = startBalance * (1 + interestRate);
            return endBal;
        }
    }

    public class CalculateInterestUtility
    {
        public List<BalanceData> BuildInterestCalculator(string fileName)
        {
            List<BalanceData> listA = new List<BalanceData>();
            using (var fs = File.OpenRead(fileName))
            using (var reader = new StreamReader(fs))
            {
                decimal startBal = 0;
                decimal endBal = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    startBal = values[0] == "January" ? 150 : endBal;
                    BalanceCalculator bc = new BalanceCalculator();
                    endBal = bc.EndBalance(startBal, Convert.ToDecimal(values[1]));
                    listA.Add(new BalanceData() { month = values[0], startBalance = startBal, endBalance = endBal });

                }
                return listA;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<BalanceData> lstInterest = new List<BalanceData>();
            try
            {
                CalculateInterestUtility interest = new CalculateInterestUtility();
                string filename = ConfigurationManager.AppSettings["InterestFile"];
                lstInterest = interest.BuildInterestCalculator(filename);
            }
            catch(Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            finally
            {
                Console.WriteLine("Month , Start Balance , End Balance");
                for (int i = 0; i < lstInterest.Count; i++)
                {    
                    Console.WriteLine(String.Format("{0} , {1} , {2}", lstInterest[i].month, Math.Round(lstInterest[i].startBalance, 2), Math.Round(lstInterest[i].endBalance, 2)));
                }
                Console.ReadLine();
            }
        }
    }
}
