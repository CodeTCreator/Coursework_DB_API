using LibraryFor_CAR_DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    internal class ViewInspector : IViewDialogWindow
    {
        private double _allWidth;
        public GridView GetGridView()
        {
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth = Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;

            Binding bindingFio = new Binding("FullName");
            GridViewColumn Fio = new GridViewColumn();
            Fio.Header = "ФИО";
            Fio.Width = 200;
            _allWidth += Fio.Width;
            Fio.DisplayMemberBinding = bindingFio;

            Binding bindingRank = new Binding("Rank");
            GridViewColumn Rank = new GridViewColumn();
            Rank.Header = "Звание";
            Rank.Width = 180;
            _allWidth += Rank.Width;
            Rank.DisplayMemberBinding = bindingRank;

            Binding bindingBirthday = new Binding("Birthday");
            GridViewColumn Birthday = new GridViewColumn();
            Birthday.Header = "Дата рождения";
            Birthday.Width = 150;
            _allWidth += Birthday.Width;
            Birthday.DisplayMemberBinding = bindingBirthday;


            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(Fio);
            myGridView.Columns.Add(Rank);
            myGridView.Columns.Add(Birthday);
            return myGridView;
        }

        public double GetWidth()
        {
            return _allWidth;
        }
    }
}
