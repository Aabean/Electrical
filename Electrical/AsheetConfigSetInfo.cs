using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrical
{
    public class AsheetConfigSetInfo : INotifyPropertyChanged
    {
        //名称
        string sheetName;
        List<SingleSpaceConfigSetInfo> setInfos;
        //单列宽
        double autoWidth;
        //距左距离
        double leftDis;
        //距上
        double topDis;
        //表头宽
        double headerWidth;

        List<string> widths;
        public AsheetConfigSetInfo()
        {
            sheetName = "配电柜";

            autoWidth = 10;
            leftDis = 10;
            topDis = 10;
            headerWidth = 10;
            setInfos = new List<SingleSpaceConfigSetInfo>();
            widths = new List<string>() { "10", "20", "30", "40", "50" };
        }

        public List<SingleSpaceConfigSetInfo> SetInfos
        {
            get => setInfos; set
            {
                setInfos = value;
                OnPropertyChanged("SetInfos");
            }
        }
        public void Switch(int f, int l)
        {
            var list = new List<SingleSpaceConfigSetInfo>(setInfos);
            if (f < setInfos.Count && l < setInfos.Count && f >= 0 && l >= 0)
            {
                var tempInfof = list[f];
                var tempInfol = list[l];
                var indexf = tempInfof.Index;
                var indexl = tempInfol.Index;
                tempInfof.Index = indexl;
                tempInfol.Index = indexf;
                list[l] = tempInfof;
                list[f] = tempInfol;
                SetInfos = list;
            }
        }
        public void Delete(int f)
        {
            var list = new List<SingleSpaceConfigSetInfo>(setInfos);
            list.RemoveAt(f);
            list.ForEach(a => a.Index = list.IndexOf(a) + 1);
            SetInfos = list;
        }
        public double AutoWidth { get => autoWidth; set { autoWidth = value; OnPropertyChanged("AutoWidth"); } }

        public string SheetName { get => sheetName; set { sheetName = value; OnPropertyChanged("SheetName"); } }

        public List<string> Widths { get => widths; set => widths = value; }
        public double LeftDis { get => leftDis; set => leftDis = value; }
        public double TopDis { get => topDis; set => topDis = value; }
        public double HeaderWidth { get => headerWidth; set => headerWidth = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string eventArgs)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(eventArgs));
            }
        }
    }
}
