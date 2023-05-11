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
    class ViewVRC : IViewDialogWindow
    {
        private double _allWidth;
        public GridView getGridView()
        {
            Vrc vrc = new Vrc();
            
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth += Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;

            Binding bindinNumberVRC = new Binding("NumberVrc");
            GridViewColumn numberVRC = new GridViewColumn();
            numberVRC.Header = "Номер свидетельства";
            numberVRC.Width = 160;
            _allWidth += numberVRC.Width;
            numberVRC.DisplayMemberBinding = bindinNumberVRC;

            Binding bindinCarPlate = new Binding("CarPlate");
            GridViewColumn CarPlateC = new GridViewColumn();
            CarPlateC.Header = "Госномер";
            CarPlateC.Width = 90;
            _allWidth += CarPlateC.Width;
            CarPlateC.DisplayMemberBinding = bindinCarPlate;

            Binding bindinIdDriver = new Binding("IdDriver");
            GridViewColumn IdDriverC = new GridViewColumn();
            IdDriverC.Header = "id водителя";
            IdDriverC.Width = 80;
            _allWidth += IdDriverC.Width;
            IdDriverC.DisplayMemberBinding = bindinIdDriver;

            Binding bindinIssued = new Binding("Issued");
            GridViewColumn IssuedC = new GridViewColumn();
            IssuedC.Header = "Место выдачи";
            IssuedC.Width = 90;
            _allWidth += IssuedC.Width;
            IssuedC.DisplayMemberBinding = bindinIssued;

            Binding bindinIdVehicle = new Binding("IdVehicle");
            GridViewColumn IdVehicleC = new GridViewColumn();
            IdVehicleC.Header = "id ТС";
            IdVehicleC.Width = 80;
            _allWidth += IdVehicleC.Width;
            IdVehicleC.DisplayMemberBinding = bindinIdVehicle;

            Binding bindinDateIssued = new Binding("date_issued");
            GridViewColumn DateIssuedC = new GridViewColumn();
            DateIssuedC.Header = "Дата выдачи";
            DateIssuedC.Width = 80;
            _allWidth += DateIssuedC.Width;
            DateIssuedC.DisplayMemberBinding = bindinDateIssued;

            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(numberVRC);
            myGridView.Columns.Add(CarPlateC);
            myGridView.Columns.Add(IdDriverC);
            myGridView.Columns.Add(IssuedC);
            myGridView.Columns.Add(IdVehicleC);
            myGridView.Columns.Add(DateIssuedC);
            return myGridView;
        }

        public double getWidth()
        {
            return _allWidth;
        }
    }
}
