
using System.Collections.Generic;
using System.ComponentModel;
using Курсовая_работа._БД.Box;

namespace Курсовая_работа._БД.Classes
{
    internal class Broker
    {

        ChoosingDBWindow choosingDBWindow;
        int resultId = -1;

        public int ResultId
        {
            get { return resultId; }
        }

        public Broker()
        {
        }
        private void WindowClose(object sender, CancelEventArgs e)
        {
            resultId = choosingDBWindow.SelectedID;
        }

        public int StartWork(string nameDB,List<int> limitValues,int mode)
        {
            choosingDBWindow = new ChoosingDBWindow();
            choosingDBWindow.Closing += WindowClose;
            choosingDBWindow.Mode = mode;
            choosingDBWindow.NameDB = nameDB;
            choosingDBWindow.LimitValues= limitValues;
            choosingDBWindow.buildSkeleton();
            choosingDBWindow.ShowDialog();
            return ResultId;
        }
    }
}
