using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    internal class ViewDepartment : IViewDialogWindow
    {
        private double _allWidth;
        public GridView GetGridView()
        {
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth += Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;


            Binding bindinFio = new Binding("Location");
            GridViewColumn Fio = new GridViewColumn();
            Fio.Header = "Местоположение";
            Fio.Width = 300;
            _allWidth += Fio.Width;
            Fio.DisplayMemberBinding = bindinFio;

            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(Fio);
            return myGridView;
        }

        public double GetWidth()
        {
           return _allWidth;
        }
    }
}
