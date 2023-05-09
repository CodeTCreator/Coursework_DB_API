
using System.Windows.Controls;
using System.Windows.Data;

namespace Курсовая_работа._БД.ViewDialogWindow
{
    internal class ViewLicenses : IViewDialogWindow
    {
        private double _allWidth;
        public GridView getGridView()
        {
            GridViewColumn IdC = new GridViewColumn();
            IdC.Header = "Id";
            IdC.Width = 50;
            _allWidth += IdC.Width;
            Binding bindingId = new Binding("Id");
            IdC.DisplayMemberBinding = bindingId;

            Binding bindinIDdriver = new Binding("IdDriver");
            GridViewColumn IDdriverC = new GridViewColumn();
            IDdriverC.Header = "ID водителя";
            IDdriverC.Width = 160;
            _allWidth += IDdriverC.Width;
            IDdriverC.DisplayMemberBinding = bindinIDdriver;

            Binding bindinIdPlaceOfIssue = new Binding("IdPlaceOfIssue");
            GridViewColumn IdPlaceOfIssueC = new GridViewColumn();
            IdPlaceOfIssueC.Header = "Место выдачи";
            IdPlaceOfIssueC.Width = 90;
            _allWidth += IdPlaceOfIssueC.Width;
            IdPlaceOfIssueC.DisplayMemberBinding = bindinIdPlaceOfIssue;

            Binding bindinDateOfIssue = new Binding("DateOfIssue");
            GridViewColumn DateOfIssueC = new GridViewColumn();
            DateOfIssueC.Header = "Дата выдачи";
            DateOfIssueC.Width = 140;
            _allWidth += DateOfIssueC.Width;
            DateOfIssueC.DisplayMemberBinding = bindinDateOfIssue;

            Binding bindinValidUntilC = new Binding("ValidUntil");
            GridViewColumn ValidUntilC = new GridViewColumn();
            ValidUntilC.Header = "Действительно до";
            ValidUntilC.Width = 140;
            _allWidth += ValidUntilC.Width;
            DateOfIssueC.DisplayMemberBinding = bindinValidUntilC;

            GridView myGridView = new GridView();
            myGridView.Columns.Add(IdC);
            myGridView.Columns.Add(IDdriverC);
            myGridView.Columns.Add(IdPlaceOfIssueC);
            myGridView.Columns.Add(DateOfIssueC);
            myGridView.Columns.Add(ValidUntilC);
            return myGridView;
        }

        public double getWidth()
        {
            return _allWidth;
        }
    }
}
