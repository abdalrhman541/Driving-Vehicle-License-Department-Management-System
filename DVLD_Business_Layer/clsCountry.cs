using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess_Layer;

namespace DVLD_Business_Layer
{
    enum enMode { AddNew = 0, Update = 1 }
    internal class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        private enMode Mode { get; set; }



        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            this.Mode = enMode.Update;
        }
        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
            this.Mode = enMode.AddNew;
        }

        // add new 

        private bool _AddNewCountry()
        {
            this.CountryID = clsCountryDataAccess.AddCountry(this.CountryName);
            return (this.CountryID != -1);
        }

        // Update 
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID, this.CountryName);
        }


        // Delete 
        
        public static bool DeleteCountry(int CountryID)
        {
            return clsCountryDataAccess.DeleteCountry(CountryID);
        }
        // Find 

        public static clsCountry FindCountry(int CountryID)
        {
            string CountryName = "";

            if (clsCountryDataAccess.FindCountryByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;

        }
        public static clsCountry FindCountry(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryDataAccess.FindCountryByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static bool IsCountryExist(int CountryID)
        {
            return clsCountryDataAccess.IsCountryExist(CountryID);
        }
        public static bool IsCountryExist(string CountryName)
        {
            return clsCountryDataAccess.IsCountryExist(CountryName);
        }

        // Save 
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if(_AddNewCountry())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();
                default:
                    return false;
            }
             

        }

    }
}
