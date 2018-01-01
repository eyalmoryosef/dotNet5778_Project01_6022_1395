//(C) 5778 David Rakovsky and Eyal Mor-Yosef 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Child
    {

        #region Fields
        //readonly fields
        readonly int id;
        readonly int mother_id;
        readonly DateTime date_of_birth;
        #endregion

        #region Constructors:
        /// <summary>
        /// Default constructor
        /// </summary>
        public Child()
        {
            id = 0;
            mother_id = 0;
            FirstName = null;
            date_of_birth = DateTime.Today;
            SpecialNeeds = false;
            TheSpecialNeeds = null;
        }
        /// <summary>
        /// Parameters constructor
        /// </summary>
        public Child(int iD, int motherID, string firstName, DateTime dateOfBirth, bool specialNeeds, string theSpecialNeeds)
        {
            #region  id = iD (with validation)
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

            #region MotherID = motherID (with validation)
            num = Convert.ToString(motherID);
            num.Trim();//Erases all spaces entered in front or back
            if (num.Length != 9)////Check whether the ID entered is exactly 9 digits:
                throw new FormatException("The ID entered is less than 9 digits long.");

            //Validation of ID (According to the algorithm of the integrity of an Israeli ID)
            validation = 0;

            for (int i = 0; i < 9; ++i)
            {
                validation += ((int)num[i] * (int)propriety[i]);
            }

            if (validation % 10 != 0)
                throw new ArgumentException("The ID that was entered illegally in Israel");
            mother_id = motherID;
            #endregion

            FirstName = firstName;

            #region date_of_birth = dateOfBirth (with validation)
            if (dateOfBirth.CompareTo(DateTime.Now) == 1)
                throw new ArgumentException("The entered date of birth is in the future");
            date_of_birth = dateOfBirth;
            #endregion

            SpecialNeeds = specialNeeds;

            TheSpecialNeeds = theSpecialNeeds;
        }
        /// <summary>
        /// Copy constructor
        /// </summary>
        public Child(Child child)
        {
            this.id = child.id;
            this.mother_id = child.mother_id;
            this.FirstName = child.FirstName;
            this.date_of_birth = child.date_of_birth;
            this.SpecialNeeds = child.SpecialNeeds;
            this.TheSpecialNeeds = child.TheSpecialNeeds;
        }
        #endregion

        #region Properties:

        /// <summary>
        /// Get id
        /// </summary>
        public int ID { get { return id; } }

        /// <summary>
        /// Get mother_id
        /// </summary>
        public int MotherID { get { return mother_id; } }

        /// <summary>
        /// Get/Set FirstName
        /// </summary>
        public string FirstName { get { return FirstName; } set { FirstName = value; } }

        /// <summary>
        /// Get date_of_birth
        /// </summary>
        public DateTime DateOfBirth { get { return date_of_birth; } }

        /// <summary>
        /// Get/Set SpecialNeeds
        /// </summary>
        public bool SpecialNeeds { get { return SpecialNeeds; } set { SpecialNeeds = value; } }

        /// <summary>
        /// Get/Set TheSpecialNeeds
        /// </summary>
        public string TheSpecialNeeds { get { return TheSpecialNeeds; } set { TheSpecialNeeds = value; } }
        #endregion

        #region Methods:
        /// <summary>
        /// Child's ToString
        /// </summary>
        /// <returns>Child's ToString</returns>
        public override string ToString()
        {
            return "I am the child: " + FirstName + ", ID: " + ID + ", Date of birth: " + DateOfBirth.ToShortDateString() + ", My mother ID: " + MotherID;
        }
        #endregion
        //מאפיינים נוספים לפי הצורך
        //בדיקת שם תקין
    }
}
