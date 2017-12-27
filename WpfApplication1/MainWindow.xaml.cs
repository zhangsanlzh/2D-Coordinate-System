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
        public static int _WH_CANVAS;//画布的边长
        public static int X_DOT=-1;//待描点的横坐标，-1表示文本框为空
        public static int Y_DOT = -1;//待描点的纵坐标，-1表示文本框为空
        public static int NUM = 10;//单元格的个数
        double R = 0.5;//放大或缩小的倍数
        public static int SYSMODE = 0;//填充模式,0表示完全填充，1表示内切圆填充

        public MainWindow()
        {
            InitializeComponent();

            _WH_CANVAS=(int)stackPanel_XySys.Width;//设置画布的边长            

            //生成坐标系
            xySys xysys = new xySys();
            mySys = xysys.createSys(_WH_CANVAS, NUM, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
        }

        private void enlarge_Click(object sender, RoutedEventArgs e)//放大
        {
            NUM -= (int)(NUM * R);//最终单元格个数

            if (NUM < 2)
            {
                NUM = 2;//锁定单元格下限为2
                return;              
            }

            stackPanel_XySys.Children.Remove(mySys);//重绘坐标系
            xySys xysys = new xySys();
            mySys = xysys.createSys(_WH_CANVAS, NUM, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
            if (X_DOT == -1 || Y_DOT == -1)//文本框空时，不再执行描点操作
            {
                return;
            }

            stackPanel_XySys.Children.Remove(mySys);//描点
            xySysCtrl sysCtrl = new xySysCtrl();
            mySys = sysCtrl.drawDot(true);
            stackPanel_XySys.Children.Add(mySys);

        }

        private void shirink_Click(object sender, RoutedEventArgs e)//缩小
        {
            NUM += (int)(NUM * R);//最终单元格个数

            stackPanel_XySys.Children.Remove(mySys);//重绘坐标系
            xySys xysys = new xySys();
            mySys = xysys.createSys(_WH_CANVAS, NUM, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
            if (X_DOT == -1 || Y_DOT == -1)//文本框空时，不再执行描点操作
            {
                return;
            }

            stackPanel_XySys.Children.Remove(mySys);//描点
            xySysCtrl sysCtrl = new xySysCtrl();
            mySys=sysCtrl.drawDot(true);
            stackPanel_XySys.Children.Add(mySys);

        }

        private void toggleMode_Click(object sender, RoutedEventArgs e)//切换填充模式
        {
            {//将填充模式变量在0和1之间切换
                SYSMODE++;
                SYSMODE %= 2;
            }
            //重绘坐标系
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl ctrl = new xySysCtrl();
            Canvas newCanvas = ctrl.toggleMode(SYSMODE);
            mySys=newCanvas;
            stackPanel_XySys.Children.Add(mySys);
           
        }

        private void drawDot_Click(object sender, RoutedEventArgs e)//描点
        {
            if (x_TxtBox.Text == ""||y_TxtBox.Text=="")//文本框值为空则返回
            {
                stackPanel_XySys.Children.Remove(mySys);//重绘坐标系
                xySys tmpCanvas = new xySys();
                mySys = tmpCanvas.createSys(_WH_CANVAS, NUM, SYSMODE);
                stackPanel_XySys.Children.Add(mySys);

                return;                
            }

            X_DOT = int.Parse(x_TxtBox.Text);
            Y_DOT = int.Parse(y_TxtBox.Text);

            //坐标系上描点
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl sysCtrl = new xySysCtrl();
            Canvas newCanvas=sysCtrl.drawDot(true);
            mySys = newCanvas;
            stackPanel_XySys.Children.Add(mySys);
        }

        private void clearTxtBox_Click(object sender, RoutedEventArgs e)//清空文本框和网格所有内容
        {
            x_TxtBox.Text = "";//清空x文本框
            y_TxtBox.Text = "";//清空y文本框
            X_DOT = -1;
            Y_DOT = -1;
            stackPanel_XySys.Children.Remove(mySys);//重绘坐标系
            xySys newCanvas = new xySys();
            mySys=newCanvas.createSys(_WH_CANVAS, NUM, SYSMODE);
            stackPanel_XySys.Children.Add(mySys);
        }

        private void turn_top_Click(object sender, RoutedEventArgs e)//上移点
        {
            if (x_TxtBox.Text=="" || y_TxtBox.Text== "")//文本框空则移动函数无效
            {
                return;
            }
            //坐标系上描点
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl sysCtrl = new xySysCtrl();
            Canvas newCanvas = sysCtrl.turnSys(0, 1, 0, 0);
            mySys = newCanvas;
            stackPanel_XySys.Children.Add(mySys);

            y_TxtBox.Text = (int.Parse(y_TxtBox.Text) -1).ToString();//改变当前y文本框的值
        }

        private void turn_left_Click(object sender, RoutedEventArgs e)//左移点
        {
            if (x_TxtBox.Text == "" || y_TxtBox.Text == "")//文本框空则移动函数无效
            {
                return;
            }
            //坐标系上描点
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl sysCtrl = new xySysCtrl();
            Canvas newCanvas = sysCtrl.turnSys(1, 0, 0, 0);
            mySys = newCanvas;
            stackPanel_XySys.Children.Add(mySys);

            x_TxtBox.Text = (int.Parse(x_TxtBox.Text) - 1).ToString();//改变当前x文本框的值
        }

        private void turn_bottom_Click(object sender, RoutedEventArgs e)//下移点
        {
            if (x_TxtBox.Text == "" || y_TxtBox.Text == "")//文本框空则移动函数无效
            {
                return;
            }
            //坐标系上描点
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl sysCtrl = new xySysCtrl();
            Canvas newCanvas = sysCtrl.turnSys(0, 0, 0, 1);
            mySys = newCanvas;
            stackPanel_XySys.Children.Add(mySys);

            y_TxtBox.Text = (int.Parse(y_TxtBox.Text) + 1).ToString();//改变当前y文本框的值
        }

        private void turn_right_Click(object sender, RoutedEventArgs e)//右移点
        {
            if (x_TxtBox.Text == "" || y_TxtBox.Text == "")//文本框空则移动函数无效
            {
                return;
            }
            //坐标系上描点
            stackPanel_XySys.Children.Remove(mySys);
            xySysCtrl sysCtrl = new xySysCtrl();
            Canvas newCanvas = sysCtrl.turnSys(0, 0, 1, 0);
            mySys = newCanvas;
            stackPanel_XySys.Children.Add(mySys);

            x_TxtBox.Text = (int.Parse(x_TxtBox.Text) + 1).ToString();//改变当前x文本框的值
        }
                
    }
}
