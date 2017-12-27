using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Nanny
    {
        #region Fields
        readonly int id;
        readonly DateTime date_of_birth;

        public Nanny()
        {

        }
        #endregion

        #region Constructors:
        public Nanny(int iD, string lastName, string firstName, DateTime dateOfBirth, int phone, string adress, bool elevator, int floor, int yearsOfExperience, int maxChilds, int minAgeOfChild, int maxAgeOfChild, bool hourlyRate, int pricePerHour, int pricePerMonth, bool[] workDays, DateTime[,] workHours, bool stateDaysOff, string recommendations)
        {
            #region id = iD (with validation)
            //Validation of the ID:
            string num = Convert.ToString(iD);
            num.Trim();//Erases all spaces entered in front or back
            if (num.Length != 9)////Check whether the ID entered is exactly 9 digits:
                throw new FormatException("The ID entered is less than 9 digits long.");

            //Validation of ID (According to the algorithm of the integrity of an Israeli ID)
            char[] propriety = { '1', '2', '1', '2', '1', '2', '1', '2', '1' };
            int validation = 0;

            for (int i = 0; i < 9; ++i)
            {
                validation += ((int)num[i] * (int)propriety[i]);
            }

            if (validation % 10 != 0)
                throw new ArgumentException("The ID that was entered illegally in Israel");

            id = iD;
            #endregion

            LastName = lastName;

            FirstName = firstName;

            #region date_of_birth = dateOfBirth (with validation)
            if (dateOfBirth.CompareTo(DateTime.Now) == 1)
                throw new ArgumentException("The entered date of birth is in the future");
            date_of_birth = dateOfBirth;
            #endregion

            Phone = phone;

            #region Adress = adress (with validation)
            int counter = 0, helpChar = adress.IndexOf(',', 2);

            if (helpChar == -1)
                throw new FormatException("The string is not in the format: Street, City, State");

            for (; helpChar != -1; counter++)
            {
                helpChar = adress.IndexOf(',', helpChar + 1);
            }

            if (counter != 3)
                throw new FormatException("The string is not in the format: Street, City, State");

            Adress = adress;
            #endregion

            Elevator = elevator;

            Floor = floor;

            #region YearsOfExperience = yearsOfExperience (with validation)
            int age = DateTime.Now.Year - DateOfBirth.Year;
            if (yearsOfExperience > (age - 18))
                throw new ArgumentException("The value entered is more than the number of years the nanny could work");
            YearsOfExperience = yearsOfExperience;
            #endregion

            #region MaxChilds = maxChilds (with validation)
            if (maxChilds < 1)
                throw new ArgumentException("The maximum number of children inserted is less than 1");
            MaxChilds = maxChilds;
            #endregion

            #region MinAgeOfChild = minAgeOfChild (with validation)
            if (minAgeOfChild < 3)
                throw new ArgumentException("Minimum child age introduced is less than 3 months");
            MinAgeOfChild = minAgeOfChild;
            #endregion

            #region MaxAgeOfChild = maxAgeOfChild (with validation)
            if (maxAgeOfChild < MinAgeOfChild)
                throw new ArgumentException("The maximum child age entered is less than the minimum child age entered");
            MaxAgeOfChild = maxAgeOfChild;
            #endregion

            HourlyRate = hourlyRate;

            #region PricePerHour = pricePerHour (with validation)
            if (pricePerHour < 0)
                throw new ArgumentException("The price per hour is less than 0");
            PricePerHour = pricePerHour;
            #endregion

            #region PricePerMonth = pricePerMonth (with validation)
            if (pricePerMonth < 0)
                throw new ArgumentException("The price per month is less than 0");
            PricePerMonth = pricePerMonth;
            #endregion

            #region WorkDays = workDays (with validation)
            if (workDays.Length != 7)
                throw new ArgumentException("The array is not the right size (7)");
            WorkDays = workDays;
            #endregion

            #region WorkHours = workHours (with validation)
            if (workHours.GetLength(0) != 6 || workHours.GetLength(2) != 2)
                throw new ArgumentException("The array is not of the appropriate size (6,2)");
            WorkHours = workHours;
            #endregion

            StateDaysOff = stateDaysOff;

            Recommendations = recommendations;
        }
        #endregion

        #region Properties:
        public int ID{get { return id;}}
        public string LastName { get { return LastName; } set { LastName = value; } }
        public string FirstName { get { return FirstName; } set { FirstName = value; } }
        public DateTime DateOfBirth { get { return date_of_birth; } }
        public int Phone { get { return Phone; } set { Phone = value; } }
        public string Adress
        {
            get { return Adress; }
            set
            {
                int counter = 0, helpChar = value.IndexOf(',', 2);

                if (helpChar == -1)
                    throw new FormatException("The string is not in the format: Street, City, State");

                for (; helpChar != -1; counter++)
                {
                    helpChar = value.IndexOf(',', helpChar + 1);
                }

                if (counter != 3)
                    throw new FormatException("The string is not in the format: Street, City, State");

                Adress = value;
            }
        }
        public bool Elevator { get { return Elevator; } set { Elevator = value; } }
        public int Floor { get { return Floor; } set { Floor = value; } }
        public int YearsOfExperience
        {
            get { return YearsOfExperience; }
            set
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                if (value > (age - 18))
                    throw new ArgumentException("The value entered is more than the number of years the nanny could work");
                YearsOfExperience = value;
            }
        }
        public int MaxChilds
        {
            get { return MaxChilds; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("The maximum number of children inserted is less than 1");
                MaxChilds = value;
            }
        }
        public int MinAgeOfChild
        {
            get { return MinAgeOfChild; }
            set
            {
                if (value < 3)
                    throw new ArgumentException("Minimum child age introduced is less than 3 months");
                MinAgeOfChild = value;
            }
        }
        public int MaxAgeOfChild
        {
            get { return MaxAgeOfChild; }
            set
            {
                if (value < MinAgeOfChild)
                    throw new ArgumentException("The maximum child age entered is less than the minimum child age entered");
                MaxAgeOfChild = value;
            }
        }
        public bool HourlyRate { get { return HourlyRate; } set { HourlyRate = value; } }
        public int PricePerHour
        {
            get { return PricePerHour; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("The price per hour is less than 0");
                PricePerHour = value;
            }
        }
        public int PricePerMonth
        {
            get { return PricePerMonth; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("The price per month is less than 0");
                PricePerMonth = value;
            }
        }
        public bool[] WorkDays
        {
            get { return WorkDays; }
            set
            {
                if (value.Length != 7)
                    throw new ArgumentException("The array is not the right size (7)");
                WorkDays = value;
            }
        }
        public DateTime[,] WorkHours
        {
            get { return WorkHours; }
            set
            {
                if (value.GetLength(0) != 6 || value.GetLength(2) != 2)
                    throw new ArgumentException("The array is not of the appropriate size (6,2)");
                WorkHours = value;
            }
        }
        public bool StateDaysOff { get { return StateDaysOff; } set { StateDaysOff = value; } }
        public string Recommendations { get { return Recommendations; } set { Recommendations = value; } }
        #endregion

        #region Methods:
        public override string ToString()
        {
            return "I am the nanny: " + FirstName + ' ' + LastName + ", ID: " + ID;
        }
        #endregion
        //מאפיינים נוספים לפי הצורך
        //בדיקת שם תקין
    }
}
