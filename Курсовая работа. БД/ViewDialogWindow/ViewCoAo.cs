using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    internal class ViewCoAo : IViewDialogWindow
    {
        private double _allWidth;

        public ViewCoAo() { }
        public GridView GetGridView()
        {
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth = Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;


            Binding bindinArticle = new Binding("Article");
            GridViewColumn Article = new GridViewColumn();
            Article.Header = "Статья";
            Article.Width = 50;
            _allWidth += Article.Width;
            Article.DisplayMemberBinding = bindinArticle;

            Binding bindinPart = new Binding("Part");
            GridViewColumn Part = new GridViewColumn();
            Part.Header = "Часть";
            Part.Width = 50;
            _allWidth += Part.Width;
            Part.DisplayMemberBinding = bindinPart;

            Binding bindinPoint = new Binding("Point");
            GridViewColumn Point = new GridViewColumn();
            Part.Header = "Пункт";
            Part.Width = 50;
            _allWidth += Part.Width;
            Part.DisplayMemberBinding = bindinPoint;

            Binding bindinDes = new Binding("Description");
            GridViewColumn Description = new GridViewColumn();
            Description.Header = "Описание";
            Description.Width = 250;
            _allWidth += Description.Width;
            Description.DisplayMemberBinding = bindinDes;

            Binding bindinRM = new Binding("RecoveryMeasuares");
            GridViewColumn RM = new GridViewColumn();
            RM.Header = "Мера наказания";
            RM.Width = 250;
            _allWidth += RM.Width;
            RM.DisplayMemberBinding = bindinRM;

            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(Article);
            myGridView.Columns.Add(Part);
            myGridView.Columns.Add(Point);
            myGridView.Columns.Add(RM);
            return myGridView;
        }

        public double GetWidth()
        {
            return _allWidth;
        }
    }
}
