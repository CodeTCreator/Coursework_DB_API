using LibraryFor_CAR_DB;
using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    internal class BoxSearch
    {
        Service_Search _service_Search = new Service_Search();
        List<Search> _searches = new List<Search>();


        public BoxSearch() { UpdateSearches(); }

        public void UpdateSearches()
        {
            _searches = _service_Search.getAllSearch();
        }

        public List<Search> GetSearches()
        {
            return _searches;
        }

        public bool checkSameinDB(Search search)
        {
            foreach (var item in _searches)
            {
                if (item.CarPlate == search.CarPlate
                    & item.FullName == search.FullName &
                    item.Level == search.Level & item.Color == search.Color &
                    item.Signs == search.Signs)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Search> filterSearches(params string[] list)
        {
            List<Search> resultList = (from item in _searches
                                        where
                                        item.Id.ToString().Contains(list[0]) &&
                                        item.CarPlate.Contains(list[1]) &&
                                        item.Level.ToString().Contains(list[2]) &&
                                        item.FullName.ToString().Contains(list[3]) &&
                                        item.Color.ToString().Contains(list[4]) &&
                                        item.Signs.Contains(list[5])
                                        select item).ToList();
            return resultList;
        }

        public void SaveSearch(Search search)
        {
            _service_Search.saveSearch(search);
            UpdateSearches();
        }
        public void edit(Search search, int ID)
        {
            _service_Search.edit(search, ID);
            UpdateSearches();
        }
        public void delete(Search search)
        {
            _service_Search.delete(search);
            UpdateSearches();
        }
    }
}
