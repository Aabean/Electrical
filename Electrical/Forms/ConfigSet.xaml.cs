using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Electrical.Forms
{
    /// <summary>
    /// ConfigSet.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigSet : Window
    {
        AsheetConfigSetInfo asheetConfigSetInfo;
        List<double> selectHeiht;
        public ConfigSet()
        {
            InitializeComponent();
            asheetConfigSetInfo = new AsheetConfigSetInfo();

            DataContext = asheetConfigSetInfo;

            //mainGrid.DataContext = asheetConfigSetInfo;            
        }

        public AsheetConfigSetInfo AsheetConfigSetInfo { get => asheetConfigSetInfo; set => asheetConfigSetInfo = value; }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var item = dataGrid.SelectedItem as SingleSpaceConfigSetInfo;
                var count = asheetConfigSetInfo.SetInfos.IndexOf(item);
                asheetConfigSetInfo.Switch(count, count - 1);
                dataGrid.SelectedItems.Clear();
            }
            else
            {
                System.Windows.MessageBox.Show("请选择一项进行调整");
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var item = dataGrid.SelectedItem as SingleSpaceConfigSetInfo;
                var count = asheetConfigSetInfo.SetInfos.IndexOf(item);
                asheetConfigSetInfo.Switch(count, count + 1);
                dataGrid.SelectedItems.Clear();
            }
            else
            {
                System.Windows.MessageBox.Show("请选择一项进行调整");
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "文件保存";
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = saveFileDialog.FileName;
                var jason = JsonConvert.SerializeObject(asheetConfigSetInfo, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.Write(jason);
                streamWriter.Flush();
                streamWriter.Close();
                this.Close();
            }
            else
            {
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LedtDis_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!double.TryParse(e.Text, out _) || ContainsChinese(e.Text))
            {
                e.Handled = true;
            }
        }
        private bool ContainsChinese(string txt)
        {
            foreach (char c in txt)
            {
                if (c >= 0x4e00 && c <= 0x9fff)
                {
                    return true;
                }
            }
            return false;
        }

        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            SingleSpaceConfigSetInfo singleSpaceConfigSetInfo = new SingleSpaceConfigSetInfo();
            singleSpaceConfigSetInfo.Index = asheetConfigSetInfo.SetInfos.Count + 1;
            e.NewItem = singleSpaceConfigSetInfo;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem as SingleSpaceConfigSetInfo;
            var count = asheetConfigSetInfo.SetInfos.IndexOf(item);
            asheetConfigSetInfo.Delete(count);
        }
    }
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && double.TryParse(stringValue, out double result))
            {
                result = Math.Round(result, 1);
                return result;
            }

            return 0.0;
        }
    }
    public enum SelectName
    {
        开关柜型号KKS编码,
        回路抽屉编号
    }
}
