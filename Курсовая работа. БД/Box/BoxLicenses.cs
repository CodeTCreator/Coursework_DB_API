using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    class BoxLicenses
    {
        Service_DriversLicense _service_DriversLicense = new Service_DriversLicense();  
        List<DriversLicense> _driversLicenses = new List<DriversLicense>();

        public BoxLicenses() { UpdateLicenses(); }

        public void UpdateLicenses()
        {
            _driversLicenses = _service_DriversLicense.getAllDriversLicense();
        }
        public List<DriversLicense> GetDriversLicense() 
        { 
            return _driversLicenses; 
        }

        public List<DriversLicense> FilterLimitsID(List<int> limits) 
        {
            if (limits != null)
            {
                List<DriversLicense> context = new List<DriversLicense>();
                foreach (DriversLicense driversLicense in _driversLicenses)
                {
                    if (!limits.Contains(driversLicense.Id))
                    {
                        context.Add(driversLicense);
                    }
                }
                _driversLicenses = context;
                return context;
            }
            return GetDriversLicense();
        }

        public bool CheckSameinDB(params string[] list)
        {
            foreach (var item in _driversLicenses)
            {
                if (item.IdDriver.ToString() == list[0] & 
                    item.IdPlaceOfIssue.ToString() == list[1] & 
                    item.DateOfIssue.ToString() == list[2] & 
                    item.ValidUntil.ToString() == list[3])
                {
                    return true;
                }
            }
            return false;
        }

        public List<DriversLicense> FilterLicenses(params string[] listT)
        {
            DateOnly dateIssue, ValidUntil;
            if (listT[3] == "")
            {
                dateIssue = new DateOnly();
            }
            else
            {
                dateIssue = DateOnly.Parse(listT[3]);
            }
            if (listT[4] == "")
            {
                ValidUntil = new DateOnly(9999, 12, 31);
            }
            else
            {
                ValidUntil = DateOnly.Parse(listT[4]);
            }
            List<DriversLicense> list = (from item in _driversLicenses
                        where
                        item.Id.ToString().Contains(listT[0]) &&
                        item.IdDriver.ToString().Contains(listT[1]) &&
                        item.IdPlaceOfIssue.ToString().Contains(listT[2]) &&
                        (item.DateOfIssue >= dateIssue) &&
                        (item.ValidUntil <= ValidUntil)
                        select item).ToList();
            return list;
        }

        public List<DriversLicense> filterIDdriverLicenses(string IdDriver)
        {
            List<DriversLicense> list = (from item in _driversLicenses
                                         where
                                 item.IdDriver.ToString().Contains(IdDriver)
                                 select item).ToList();
            return list;
        }

        public void SaveDriverLicense(DriversLicense driverLicense)
        {
            _service_DriversLicense.saveDriversLicense(driverLicense);
            UpdateLicenses();
        }

        public void Edit(DriversLicense driverLicense, int ID)
        {
            _service_DriversLicense.edit(driverLicense, ID);
            UpdateLicenses();
        }

        public void Delete(DriversLicense driversLicense)
        {
            _service_DriversLicense.delete(driversLicense);
            UpdateLicenses();
        }
    }
}
