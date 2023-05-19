using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    internal class BoxMinutes
    {
        Service_Minute _service_Minute = new Service_Minute();
        List<Minute> _minutes= new List<Minute>();

        public BoxMinutes() { UpdateMinutes(); }

        public void UpdateMinutes()
        {
            _minutes = _service_Minute.getAllMinute();
        }
        public List<Minute> GetMinutes() { return _minutes; }

        public bool CheckSameinDB(Minute minute)
        {
            foreach (var item in _minutes)
            {
                if (item.IdViolator == minute.IdViolator &&
                    item.IdInspector == minute.IdInspector &&
                    item.IdVrc == minute.IdVrc &&
                    item.CrimeScene == minute.CrimeScene &&
                    item.DateMinutes == minute.DateMinutes &&
                    item.ExplanationViolator == minute.ExplanationViolator
                    )
                {
                    return true;
                }
            }
            return false;
        }
        public List<Minute> FilterMinutes(params string[] listT)
        {
            DateOnly date;
            if (listT[5] == "")
            {
                date = new DateOnly();
            }
            else
            {
                date = DateOnly.Parse(listT[5]);
            }
            List<Minute> list = (from item in _minutes
                                         where
                                         item.Id.ToString().Contains(listT[0]) &&
                                         item.IdViolator.ToString().Contains(listT[1]) &&
                                         item.IdInspector.ToString().Contains(listT[2]) &&
                                         item.IdVrc.ToString().Contains(listT[3]) &&
                                         item.CrimeScene.Contains(listT[4]) &&
                                         (item.DateMinutes == date)
                                         select item).ToList();
            return list;
        }

        public void SaveMinute(Minute minute)
        {
            _service_Minute.saveMinute(minute);
            UpdateMinutes();
        }
        public void Edit(Minute minute,int id) 
        { 
            _service_Minute.edit(minute,id);  
            UpdateMinutes();
        }
        public void Delete(Minute minute)
        {
            _service_Minute.delete(minute);
            UpdateMinutes();
        }
    }
}
