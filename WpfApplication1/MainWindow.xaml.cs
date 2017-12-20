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
        Canvas mySys;//待返回画布对象
        int num = 10;//最初单元格的个数
        double r = 0.5;//放大或缩小的倍数
        int SYSMODE = 0;//填充模式,0表示完全填充，1表示内切圆填充

        public MainWindow()
        {
            InitializeComponent();
            //生成坐标系
            xySys xysys = new xySys();
            mySys = xysys.createSys((int)stackPanel_XySys.Width, 10, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
        }

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
            mySys = xysys.createSys((int)stackPanel_XySys.Width, num, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
        }

        private void shirink_Click(object sender, RoutedEventArgs e)
        {
            //最终单元格个数
            num += (int)(num * r);

            //重绘坐标系
            stackPanel_XySys.Children.Remove(mySys);
            xySys xysys = new xySys();
            mySys = xysys.createSys((int)stackPanel_XySys.Width, num, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
        }

        private void toggleMode_Click(object sender, RoutedEventArgs e)
        {
            {//将填充模式变量在0和1之间切换
                SYSMODE++;
                SYSMODE %= 2;
            }
            //重绘坐标系
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl ctrl = new xySysCtrl();
            Canvas newCanvas=ctrl.toggleMode((int)stackPanel_XySys.Width, num, SYSMODE);
            mySys=newCanvas;
            stackPanel_XySys.Children.Add(mySys);
           
        }
                
    }
}
