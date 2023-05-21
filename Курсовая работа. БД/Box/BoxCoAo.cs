using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Курсовая_работа._БД.Box
{
    internal class BoxCoAo
    {
        Service_Coao _service_Coao = new Service_Coao();
        List<Coao> _coaos = new List<Coao>();
    

    public BoxCoAo() { UpdateCoAo(); }

    public void UpdateCoAo()
    {
            _coaos = _service_Coao.getAllCoao();
    }
    public List<Coao> GetCoaos() { return _coaos; }


    public List<Coao> FilterArticles(string article)
    {
        List<Coao> list = (from item in _coaos
                                        where
                                        item.Article.ToString().Contains(article)
                                        select item).ToList();
        return list;
    }

    public List<Coao> FilterLimitsID(List<int> limits)
    {
        if (limits != null)
        {
            List<Coao> context = new List<Coao>();
            foreach (Coao coao in _coaos)
            {
                if (!limits.Contains(coao.Id))
                {
                    context.Add(coao);
                }
            }
              _coaos = context;
            return _coaos;
        }
        return GetCoaos();
    }


    }
}
