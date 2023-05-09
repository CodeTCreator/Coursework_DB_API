using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    internal class ViewVehicle : IViewDialogWindow
    {
        private double _allWidth;
        public GridView getGridView()
        {
            GridViewColumn Id = new GridViewColumn();
            Id.Header = "id";
            Id.Width = 50;
            _allWidth += Id.Width;
            Binding bindingId = new Binding("Id");
            Id.DisplayMemberBinding = bindingId;

            Binding bindinVIN = new Binding("Vin");
            GridViewColumn VINC = new GridViewColumn();
            VINC.Header = "VIN";
            VINC.Width = 160;
            _allWidth += VINC.Width;
            VINC.DisplayMemberBinding = bindinVIN;

            Binding bindinBrand = new Binding("BrandModel");
            GridViewColumn BrandC = new GridViewColumn();
            BrandC.Header = "Производитель";
            BrandC.Width = 90;
            _allWidth += BrandC.Width;
            BrandC.DisplayMemberBinding = bindinBrand;

            Binding bindinBody = new Binding("Body");
            GridViewColumn BodyC = new GridViewColumn();
            BodyC.Header = "Кузов";
            BodyC.Width = 80;
            _allWidth += BodyC.Width;
            BodyC.DisplayMemberBinding = bindinBody;

            Binding bindinEngineCapacity = new Binding("EngineCapacity");
            GridViewColumn EngineCapacityC = new GridViewColumn();
            EngineCapacityC.Header = "Объем двигателя";
            EngineCapacityC.Width = 90;
            _allWidth += EngineCapacityC.Width;
            EngineCapacityC.DisplayMemberBinding = bindinEngineCapacity;

            Binding bindinCarPower = new Binding("CarPower");
            GridViewColumn CarPowerC = new GridViewColumn();
            CarPowerC.Header = "Мощность";
            CarPowerC.Width = 80;
            _allWidth += CarPowerC.Width;
            CarPowerC.DisplayMemberBinding = bindinCarPower;

            Binding bindinColor = new Binding("Color");
            GridViewColumn ColorC = new GridViewColumn();
            ColorC.Header = "Цвет";
            ColorC.Width = 80;
            _allWidth += ColorC.Width;
            ColorC.DisplayMemberBinding = bindinColor;

            GridView myGridView = new GridView();
            myGridView.Columns.Add(Id);
            myGridView.Columns.Add(VINC);
            myGridView.Columns.Add(BrandC);
            myGridView.Columns.Add(BodyC);
            myGridView.Columns.Add(EngineCapacityC);
            myGridView.Columns.Add(CarPowerC);
            myGridView.Columns.Add(ColorC);
            return myGridView;
        }

        public double getWidth()
        {
            return _allWidth;
        }
    }
}
