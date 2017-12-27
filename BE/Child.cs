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
        readonly int id;
        readonly int mother_id;
        readonly DateTime date_of_birth;
        #endregion

        #region Constructors:
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
        #endregion

        #region Properties:
        public int ID { get { return id; } }
        public int MotherID { get { return MotherID; }}
        public string FirstName { get { return FirstName; } set { FirstName = value; } }
        public DateTime DateOfBirth { get { return date_of_birth; } }
        public bool SpecialNeeds { get { return SpecialNeeds; } set { SpecialNeeds = value; } }
        public string TheSpecialNeeds { get { return TheSpecialNeeds; } set { TheSpecialNeeds = value; } }
        #endregion

        #region Methods:
        public override string ToString()
        {
            return "I am the child: " + FirstName + ", ID: " + ID + ", Date of birth: " + DateOfBirth.ToShortDateString() + ", My mother ID: " + MotherID;
        }
        #endregion
        //מאפיינים נוספים לפי הצורך
    }
}
