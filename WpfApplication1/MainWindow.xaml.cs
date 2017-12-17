using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas mySys;
        public MainWindow()
        {
            InitializeComponent();
            //生成坐标系
            xySys xysys = new xySys();
            mySys = xysys.createSys((int)stackPanel_XySys.Width, 10);
            stackPanel_XySys.Children.Add(mySys);
        }

        int num = 10;//最初单元格的个数
        double r = 0.5;//放大或缩小的倍数
        private void enlarge_Click(object sender, RoutedEventArgs e)
        {

            //最终单元格个数
            num -= (int)(num * r);

            if (num<2)
            {
                num = 2;//锁定单元格下限为2
                return;              
            }

            //重绘坐标系
            stackPanel_XySys.Children.Remove(mySys);
            xySys xysys = new xySys();
            mySys = xysys.createSys((int)stackPanel_XySys.Width, num);
            stackPanel_XySys.Children.Add(mySys);
        }

        private void shirink_Click(object sender, RoutedEventArgs e)
        {
            //最终单元格个数
            num += (int)(num * r);

            //重绘坐标系
            stackPanel_XySys.Children.Remove(mySys);
            xySys xysys = new xySys();
            mySys = xysys.createSys((int)stackPanel_XySys.Width, num);
            stackPanel_XySys.Children.Add(mySys);
        }
                
    }
}
