//(C) 5778 David Rakovsky and Eyal Mor-Yosef 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Mother
    {
        #region Fields
        //readonly fields
        readonly int id;
        #endregion

        #region Constructors:
        /// <summary>
        /// Default constructor
        /// </summary>
        public Mother()
        {
            id = 0;
            LastName = null;
            FirstName = null;
            Phone = 0;
            Adress = null;
            DesiredAddressOfNanny = null;
            DaysOfNeedingNanny = null;
            HoursOfNeedingNanny = null;
            Recommendations = null;
        }

        /// <summary>
        /// Parameters constructor
        /// </summary>
        public Mother(int iD, string lastName, string firstName, int phone, string adress, string desiredAddressOfNanny, bool[] daysOfNeedingNanny, DateTime[,] hoursOfNeedingNanny, string recommendations)
        {
            #region id = iD (with validation)
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

            #region DesiredAddressOfNanny = desiredAddressOfNanny (with validation)
            counter = 0;
            helpChar = desiredAddressOfNanny.IndexOf(',', 2);

            if (helpChar == -1)
                throw new FormatException("The string is not in the format: Street, City, State");

            for (; helpChar != -1; counter++)
            {
                helpChar = desiredAddressOfNanny.IndexOf(',', helpChar + 1);
            }

            if (counter != 3)
                throw new FormatException("The string is not in the format: Street, City, State");

            DesiredAddressOfNanny = desiredAddressOfNanny;
            #endregion

            #region DaysOfNeedingNanny = daysOfNeedingNanny (with validation)
            if (daysOfNeedingNanny.Length != 7)
                throw new ArgumentException("The array is not the right size (7)");
            DaysOfNeedingNanny = daysOfNeedingNanny;
            #endregion

            #region HoursOfNeedingNanny = hoursOfNeedingNanny (with validation)
            if (hoursOfNeedingNanny.GetLength(0) != 6 || hoursOfNeedingNanny.GetLength(2) != 2)
                throw new ArgumentException("The array is not of the appropriate size (6,2)");
            HoursOfNeedingNanny = hoursOfNeedingNanny;
            #endregion

            Recommendations = recommendations;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Mother(Mother mother)
        {
            this.id = mother.id;
            this.LastName = mother.LastName;
            this.FirstName = mother.FirstName;
            this.Phone = mother.Phone;
            this.Adress = mother.Adress;
            this.DesiredAddressOfNanny = mother.DesiredAddressOfNanny;
            this.DaysOfNeedingNanny = mother.DaysOfNeedingNanny;
            this.HoursOfNeedingNanny = mother.HoursOfNeedingNanny;
            this.Recommendations = mother.Recommendations;
        }
        #endregion

        #region Properties:
        /// <summary>
        /// Get id
        /// </summary>
        public int ID { get { return id; } }

        /// <summary>
        /// Get/set LastName
        /// </summary>
        public string LastName { get { return LastName; } set { LastName = value; } }

        /// <summary>
        /// Get/set FirstName
        /// </summary>
        public string FirstName { get { return FirstName; } set { FirstName = value; } }

        /// <summary>
        /// Get/set Phone
        /// </summary>
        public int Phone { get { return Phone; } set { Phone = value; } }

        /// <summary>
        /// Get/set Adress
        /// </summary>
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

        /// <summary>
        /// Get/set DesiredAddressOfNanny
        /// </summary>
        public string DesiredAddressOfNanny
        {
            get { return DesiredAddressOfNanny; }
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

                DesiredAddressOfNanny = value;
            }
        }

        /// <summary>
        /// Get/set DaysOfNeedingNanny
        /// </summary>
        public bool[] DaysOfNeedingNanny
        {
            get { return DaysOfNeedingNanny; }
            set
            {
                if (value.Length != 7)
                    throw new ArgumentException("The array is not the right size (7)");
                DaysOfNeedingNanny = value;
            }
        }

        /// <summary>
        /// Get/set HoursOfNeedingNanny
        /// </summary>
        public DateTime[,] HoursOfNeedingNanny
        {
            get { return HoursOfNeedingNanny; }
            set
            {
                if (value.GetLength(0) != 6 || value.GetLength(1) != 2)
                    throw new ArgumentException("The array is not of the appropriate size (6,2)");
                HoursOfNeedingNanny = value;
            }
        }

        /// <summary>
        /// Get/set Recommendations
        /// </summary>
        public string Recommendations { get { return Recommendations; } set { Recommendations = value; } }
        #endregion

        #region Methods:
        /// <summary>
        /// Mother's ToString
        /// </summary>
        /// <returns>Mother's ToString</returns>
        public override string ToString()
        {
            return "I am the mother: " + FirstName + ' ' + LastName + ", ID: " + ID;
        }
        #endregion
        //מאפיינים נוספים לפי הצורך
        //בדיקת שם תקין
    }
}
