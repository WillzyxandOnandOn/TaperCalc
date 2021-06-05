using System;

namespace TaperCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            ///space to decide substance loop/pathway
            ////// Would like this program to create a doc that creates a printable calender/taper schedule that shows the date, total grams per day. dose times,
            /// individual dose amount, and maybe encouraging statemenets put at specific points along the taper schedule. 
            /// 
            Console.WriteLine("Welcome to the Kratom taper calculator.\nThis program will create a simple taper schedule.\nYou" +
                " will need to know your average dose amount (how much in grams do you take at a time?)\nand your dosing frequency (how many times in one day do you take Kratom?)\n" +
                "If you don't know your dose amount and frequency,\nI suggest keeping track in a notebook or in a note application\nfor the next 3 days then using that information" +
                " to decide what your average dose amount and frequency has been.\n");
            Console.WriteLine("Type 1 to start from your average Daily Dose;\nType 2 to input per dose amount and frequency of dose.");
            var userInput = Console.ReadLine();
            var input = ValidateInput(userInput);
            var dailyDose = DailyDose(input);
            Console.WriteLine("Your daily dose is: " + dailyDose);

            Console.WriteLine("Type 1 to use the recommened dose of 5 grams per day as the ending dose or 'jump off dose'\n" +
                "or Type 2 to set your own ending dose?");
            userInput = Console.ReadLine();
            input = ValidateInput(userInput);
            var jumpOffDose = JumpOffDose(input);
            Console.WriteLine("Ending Dose in grams per day: " + jumpOffDose);
            Console.WriteLine("\n\nNow we are going to create the taper schedule\n");
            Console.WriteLine("\nType 1 to create a long and gentle taper, this method creates a ending date for you.\nBest option if the length of the taper doesn't matter\n\n" +
                "Type 2 to input your own end date that will be used to calcuate your taper.");
            userInput = Console.ReadLine();
            input = ValidateInput(userInput);

            if (input == "1")
            {
                var reductionPercent = .10;
                int week = 0;
                double dailyReduction;
                for (double i = dailyDose; i > jumpOffDose; i-=dailyReduction)
                {
                    dailyReduction = dailyDose * reductionPercent;
                    double newDailyDose = dailyDose - dailyReduction;
                    dailyDose = newDailyDose;
                    week += 1;
                    Console.WriteLine("Week: " + week);
                    Console.WriteLine("New Daily Dose:{0:N1}", newDailyDose);
                    var doseAmount = newDailyDose / 4;
                    Console.WriteLine("Take {0:N1} grams, 4 times a day for 1 week", doseAmount);
                    ///Add new "WriteToFile" method would be nice to write this to a file
                }
            }
            if (input == "2")
            {
                var startDate = DateTime.Today;
                Console.WriteLine("The slower your taper or the more time you have the easier the expereince will be. " +
               "30 days will be much harder than 180 days(6 months)");
                Console.WriteLine("Enter the date for end of taper. Example of readable date: " + startDate.ToString("MM/dd/yyyy"));
                var userDate = Console.ReadLine();
                DateTime lastDate = ValidateDate(userDate);
                var daysToTaperInt = TaperLengthCalc(lastDate);  
                Console.WriteLine("Length of Taper: " + daysToTaperInt);
                for (double i = daysToTaperInt; i > 1; i--)
                {
                    double dailyReduction = (dailyDose - jumpOffDose) / daysToTaperInt; 
                    double newDailyDose = dailyDose - dailyReduction;
                    dailyDose = newDailyDose;
                    daysToTaperInt -= 1;
                    var nextDay = startDate.AddDays(1);
                    startDate = nextDay;
                    Console.WriteLine(value: nextDay.ToShortDateString());
                    Console.WriteLine("New Daily Dose:{0:N1} grams a day", newDailyDose);
                    var doseAmount = newDailyDose / 4;
                    Console.WriteLine("Take {0:N1} grams, 4 times a day", doseAmount);
                    /// Add new "WriteToFile" method would be nice to write this to a file
                }
            }
        } 
        public static string ValidateInput(string userInput)
        {
            string input ;
            while (true)
            {
                if (userInput == "1" || userInput == "2")
                {
                    input = userInput;
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a 1 or 2.");
                    userInput = Console.ReadLine();
                }
            }
        }
        public static double DailyDose(string input)
        {
            if (input == "1")
            {
                Console.WriteLine("What is your average Daily dose in grams per day?");
                input = Console.ReadLine();
                double inputDD = ValidateDouble(input);
                return inputDD;
            }
            else
            {
                Console.WriteLine("What is your average dose amount in grams?");
                input = Console.ReadLine();
                var doseAmount = ValidateDouble(input);
                Console.WriteLine("What is the frequency of your dosing, i.e. how many times do you dose per day?");
                input = Console.ReadLine();
                var doseFrequency = ValidateDouble(input);
                return doseAmount * doseFrequency;
            }
        }
        public static double JumpOffDose(string input)
        {
            if (input == "1")
            {
                double jumpOffDose = 5;
                return jumpOffDose;  ///will return 5; work instead? 
            }
            else
            {
                Console.WriteLine("What dose in grams per day would you like to set as your jump off dose?");
                input = Console.ReadLine();
                var inputDouble = ValidateDouble(input);
                return inputDouble;
            }
        }
        public static DateTime ValidateDate(string userDate)
        {
            while (true)
            {
                DateTime lastDate;
                if (DateTime.TryParse(userDate, out lastDate))
                {
                    Console.WriteLine("The end date is: " + lastDate.ToShortDateString());
                    return lastDate;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                    userDate = Console.ReadLine();
                }
            }
        }
        public static double TaperLengthCalc(DateTime lastDate)
        {
            var startDate = DateTime.Today;
            var daysToTaper = lastDate - startDate;
            double daysToTaperInt = Convert.ToDouble(daysToTaper.TotalDays);
            return daysToTaperInt; 
        }
        public static double ValidateDouble(string input)
        {
            while (true)
            {
                if (double.TryParse(input, out var inputDouble))
                {
                    return inputDouble;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter dose as a number. Ex: 5");
                    input = Console.ReadLine();
                }
            }
        }
    }
}
