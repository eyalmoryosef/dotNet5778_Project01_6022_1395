//(C) 5778 David Rakovsky and Eyal Mor-Yosef 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract
    {
        #region Fields
        //readonly fields
        readonly int nanny_id;
        readonly int child_id;
        #endregion

        #region Constructors:

        /// <summary>
        /// Default constructor
        /// </summary>
        public Contract()
        {
            Num = "0";
            nanny_id = 0;
            child_id = 0;
            IntroductoryMeeting = false;
            ContractSigned = false;
            HourlySalary = 0;
            MonthlySalary = 0;
            HourOrMonth = SalaryBy.Hour;
            DateOfStartContract = DateTime.Today;
            DateOfEndContract = DateTime.Today;
        }

        /// <summary>
        /// Parameters constructor
        /// </summary>
        public Contract(string num, int nannyID, int childID, bool introductoryMeeting, bool contractSigned, int hourlySalary, int monthlySalary, SalaryBy hourOrMonth, DateTime dateOfStartContract, DateTime dateOfEndContract)
        {
            #region Num = num (with validation)
            num.Trim();

            foreach (char c in num)
            {
                if (c < '0' || c > '9')
                    throw new FormatException("Num must consist of numbers only");
            }

            if (num.Length != 8)
                throw new FormatException("The number must be exactly 8 digits");
            Num = num;
            #endregion

            #region nanny_id = nannyID (with validation)
            //Validation of the ID:
            string id = Convert.ToString(nannyID);
            id.Trim();//Erases all spaces entered in front or back
            if (id.Length != 9)////Check whether the ID entered is exactly 9 digits:
                throw new FormatException("The ID entered is less than 9 digits long.");

            //Validation of ID (According to the algorithm of the integrity of an Israeli ID)
            char[] propriety = { '1', '2', '1', '2', '1', '2', '1', '2', '1' };
            int validation = 0;

            for (int i = 0; i < 9; ++i)
            {
                validation += ((int)id[i] * (int)propriety[i]);
            }

            if (validation % 10 != 0)
                throw new ArgumentException("The ID that was entered illegally in Israel");
            nanny_id = nannyID;
            #endregion

            #region child_id = childID (with validation)
            //Validation of the ID:
            id = Convert.ToString(childID);
            id.Trim();//Erases all spaces entered in front or back
            if (id.Length != 9)////Check whether the ID entered is exactly 9 digits:
                throw new FormatException("The ID entered is less than 9 digits long.");

            //Validation of ID (According to the algorithm of the integrity of an Israeli ID)
            validation = 0;

            for (int i = 0; i < 9; ++i)
            {
                validation += ((int)id[i] * (int)propriety[i]);
            }

            if (validation % 10 != 0)
                throw new ArgumentException("The ID that was entered illegally in Israel");

            child_id = childID;
            #endregion

            IntroductoryMeeting = introductoryMeeting;

            ContractSigned = contractSigned;

            HourlySalary = hourlySalary;

            MonthlySalary = monthlySalary;

            HourOrMonth = hourOrMonth;

            #region DateOfStartContract = dateOfStartContract (with validation)
            if (dateOfStartContract.CompareTo(DateTime.Now) == 1)
                throw new ArgumentException("The entered date of birth is in the future");
            DateOfStartContract = dateOfStartContract;
            #endregion

            #region DateOfEndContract = dateOfEndContract (with validation)
            if (dateOfEndContract.CompareTo(DateOfStartContract) == -1)
                throw new ArgumentException("The contract end date entered earlier than the contract start date");
            DateOfEndContract = dateOfEndContract;
            #endregion
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Contract(Contract contract)
        {
            this.Num = contract.Num;
            this.nanny_id = contract.nanny_id;
            this.child_id = contract.child_id;
            this.IntroductoryMeeting = contract.IntroductoryMeeting;
            this.ContractSigned = contract.ContractSigned;
            this.HourlySalary = contract.HourlySalary;
            this.MonthlySalary = contract.MonthlySalary;
            this.HourOrMonth = contract.HourOrMonth;
            this.DateOfStartContract = contract.DateOfStartContract;
            this.DateOfEndContract = contract.DateOfEndContract;
        }
        #endregion

        #region Properties:
        /// <summary>
        /// Get/Set Num
        /// </summary>
        public string Num
        {
            get { return Num; }
            set
            {
                value.Trim();

                foreach (char c in value)
                {
                    if (c < '0' || c > '9')
                        throw new FormatException("Num must consist of numbers only");
                }

                if (value.Length != 8)
                    throw new FormatException("The number must be exactly 8 digits");

                Num = value;
            }
        }

        /// <summary>
        /// Get nanny_id
        /// </summary>
        public int NannyID { get { return nanny_id; } }

        /// <summary>
        /// Get child_id
        /// </summary>
        public int ChildID { get { return child_id; } }

        /// <summary>
        /// Get/Set IntroductoryMeeting
        /// </summary>
        public bool IntroductoryMeeting { get { return IntroductoryMeeting; } set { IntroductoryMeeting = value; } }

        /// <summary>
        /// Get/Set ContractSigned
        /// </summary>
        public bool ContractSigned { get { return ContractSigned; } set { ContractSigned = value; } }

        /// <summary>
        /// Get/Set HourlySalary
        /// </summary>
        public int HourlySalary { get { return HourlySalary; } set { HourlySalary = value; } }

        /// <summary>
        /// Get/Set MonthlySalary
        /// </summary>
        public int MonthlySalary { get { return MonthlySalary; } set { MonthlySalary = value; } }

        /// <summary>
        /// Get/Set HourOrMonth
        /// </summary>
        public SalaryBy HourOrMonth { get { return HourOrMonth; } set { HourOrMonth = value; } }

        /// <summary>
        /// Get/Set DateOfStartContract
        /// </summary>
        public DateTime DateOfStartContract
        {
            get { return DateOfStartContract; }
            set
            {
                if (value.CompareTo(DateTime.Now) == 1)
                    throw new ArgumentException("The entered date of birth is in the future");
                DateOfStartContract = value;
            }
        }

        /// <summary>
        /// Get/Set DateOfEndContract
        /// </summary>
        public DateTime DateOfEndContract
        {
            get { return DateOfEndContract; }
            set
            {
                if (value.CompareTo(DateOfStartContract) == -1)
                    throw new ArgumentException("The contract end date entered earlier than the contract start date");
                DateOfEndContract = value;
            }
        }
        #endregion

        #region Methods:
        /// <summary>
        /// Contract's ToString
        /// </summary>
        /// <returns>Contract's ToString</returns>
        public override string ToString()
        {
            return "I am the contract No." + Num + ", nanny ID: " + NannyID + ", child ID: " + ChildID;
        }
        #endregion
        //מאפיינים נוספים לפי הצורך
    }

    public enum SalaryBy { Hour, Month };
}
