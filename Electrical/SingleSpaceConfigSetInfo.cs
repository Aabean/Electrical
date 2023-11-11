using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrical
{
    public class SingleSpaceConfigSetInfo : INotifyPropertyChanged
    {
        //编号
        double index;
        //名称
        string name;
        //宽
        double width;
        //高
        double height;
        //数据来源
        string dataSource;
        //是否合并
        bool isMerge = false;
        //电气原件
        bool isEletrical = true;
        List<double> selectHeiht;
        public SingleSpaceConfigSetInfo()
        {
            index = 1;
            name = string.Empty;
            selectHeiht = new List<double> { 10, 20, 30, 40, 50 };
            width = 10;
            height = 10;
            dataSource = string.Empty;
        }

        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set { height = value; OnPropertyChanged("Height"); } }
        public string DataSource { get => dataSource; set { dataSource = value; OnPropertyChanged("DataSource"); } }
        public bool IsMerge { get => isMerge; set { isMerge = value; OnPropertyChanged("IsMerge"); } }
        public bool IsEletrical { get => isEletrical; set { isEletrical = value; OnPropertyChanged("IsRangeExist"); } }
        public List<double> SelectHeiht { get => selectHeiht; set => selectHeiht = value; }
        public double Index { get => index; set { index = value; OnPropertyChanged("Index"); } }

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
