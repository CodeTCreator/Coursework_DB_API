using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа._БД.Box
{
    internal class BoxArticles
    {
        Service_ViolatedArticle _service_ViolatedArticle = new Service_ViolatedArticle();
        List<ViolatedArticle> _articles = new List<ViolatedArticle>();


        public BoxArticles() { UpdateArticles(); }

        public void UpdateArticles()
        {
            _articles = _service_ViolatedArticle.getAllViolatedArticle();
        }

        public List<ViolatedArticle> GetArticles() { return _articles; }

        public List<string> GetArticles(int idMinutes)
        {
            List<string> list = new List<string>();

            list = (from article in _articles
                   where article.IdMinutes == idMinutes
                   select article.IdArticles.ToString()).ToList();
            return list;
        }

        public List<ViolatedArticle> Filter;
    }
}
