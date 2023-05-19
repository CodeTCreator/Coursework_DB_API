using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System.Collections.Generic;
using System.Linq;

namespace Курсовая_работа._БД.Box
{
    internal class BoxInspectors
    {
        Service_Inspector _service_Inspector = new Service_Inspector(); 
        List<Inspector> _inspectors = new List<Inspector>();
        
        
        public BoxInspectors() { UpdateInspectors(); }

        public void UpdateInspectors()
        {
            _inspectors = _service_Inspector.getAllInspector();
        }

        public List<Inspector> GetInspectors() { return _inspectors; }

        public List<Inspector> FilterFIOInspectors(string Fio)
        {
            List<Inspector> list = (from item in _inspectors
                                 where
                                 item.FullName.Contains(Fio)
                                 select item).ToList();
            return list;
        }

        public void SaveInspector(Inspector inspector)
        {
            _service_Inspector.saveInspector(inspector);
            UpdateInspectors();
        }

        public void Edit(Inspector inspector,int ID)
        {
            _service_Inspector.edit(inspector,ID);
            UpdateInspectors();
        }

        public void Delete(Inspector inspector)
        {
            _service_Inspector.delete(inspector); 
            UpdateInspectors();
        }
    }
}
