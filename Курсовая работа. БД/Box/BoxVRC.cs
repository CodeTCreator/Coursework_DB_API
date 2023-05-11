using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System.Collections.Generic;
using System.Linq;

namespace Курсовая_работа._БД.Box
{
    class BoxVRC
    {
        Service_Vrc _service_VRC = new Service_Vrc();
        List<Vrc> _vrc = new List<Vrc>();

        public BoxVRC() { UpdateVRC(); }

        public void UpdateVRC()
        {
            _vrc = _service_VRC.getAllVrc();
        }
        public List<Vrc> GetVRC()
        {
            return _vrc;
        }

        public List<Vrc> FilterLimitsID(List<int> limits)
        {
            if(limits != null)
            {
                List<Vrc> context = new List<Vrc>();
                foreach(Vrc vrc in _vrc)
                {
                    if (!limits.Contains(vrc.Id))
                    {
                        context.Add(vrc);
                    }
                }
                return context;
            }
            return GetVRC();
        }

        public bool checkSameinDB(Vrc vrc)
        {
            foreach (var item in _vrc)
            {
                if (item.CarPlate == vrc.CarPlate
                    & item.IdDriver == vrc.IdDriver&
                    item.NumberVrc == vrc.NumberVrc & item.IdPlace == vrc.IdPlace &
                    item.IdVehicle == vrc.IdVehicle &
                    item.DateIssued == vrc.DateIssued)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Vrc> filterVRC(params string[] list)
        {
            List<Vrc> resultList = (from item in _vrc
                                        where
                                        item.Id.ToString().Contains(list[0]) &&
                                        item.NumberVrc.Contains(list[1]) &&
                                        item.CarPlate.Contains(list[2]) &&
                                        item.IdDriver.ToString().Contains(list[3]) &&
                                        item.DateIssued.ToString().Contains(list[4]) &&
                                        item.IdVehicle.ToString().Contains(list[5]) &&
                                        item.IdPlace.ToString().Contains(list[6])
                                        select item).ToList();
            return resultList;
        }
        public void SaveVRC(Vrc vrc)
        {
            _service_VRC.saveVrc(vrc);
            UpdateVRC();
        }
        public void edit(Vrc vrc, int ID)
        {
            _service_VRC.edit(vrc, ID);
            UpdateVRC();
        }
        public void delete(Vrc vrc)
        {
            _service_VRC.delete(vrc);
            UpdateVRC();
        }
    }
}
