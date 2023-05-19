using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    class ViewDriver : IViewDialogWindow
    {
        private double _allWidth;

        public ViewDriver()
        {
        }
        public GridView GetGridView()
        {
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth = Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;


            Binding bindinFio = new Binding("Fio");
            GridViewColumn Fio = new GridViewColumn();
            Fio.Header = "ФИО";
            Fio.Width = 200;
            _allWidth += Fio.Width;
            Fio.DisplayMemberBinding = bindinFio;

            Binding bindinAddress = new Binding("Address");
            GridViewColumn Address = new GridViewColumn();
            Address.Header = "Адрес";
            Address.Width = 180;
            _allWidth += Address.Width;
            Address.DisplayMemberBinding = bindinAddress;


            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(Fio);
            myGridView.Columns.Add(Address);
            return myGridView;
        }

        public double GetWidth()
        {
            return _allWidth;
        }
    }
}
