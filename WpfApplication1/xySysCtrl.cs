using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WpfApplication1
{
    class xySysCtrl
    {
        public Canvas toggleMode(int _SYS_MODE)//切换坐标系填充模式
        {
            xySys mySys = new xySys();
            Canvas tmpCanvas = mySys.createSys(MainWindow._WH_CANVAS, MainWindow.NUM, _SYS_MODE);//绘制坐标系

            if (MainWindow.X_DOT==-1||MainWindow.Y_DOT==-1)//如果文本框为空，则不描点
            {
                return tmpCanvas;
            }
            tmpCanvas=drawDot(true);//描点

            return tmpCanvas;
        }

        public Canvas drawDot(bool isFill)//描点函数
        {
            xySys mySys = new xySys();
            Canvas tmpCanvas = mySys.createSys(MainWindow._WH_CANVAS, MainWindow.NUM, MainWindow.SYSMODE);
                        
            Point loc = new Point();//单元格的位置
            loc.X = MainWindow.X_DOT;
            loc.Y = MainWindow.Y_DOT;
            tmpCanvas = mySys.findAndFill_A_Cell(loc);//返回带点的画布

            return tmpCanvas;
        }

        //offset_Left,左偏移量  //offset_Top,上偏移量
        //offset_Right,右偏移量  //offset_Bottom,下偏移量
        public Canvas turnSys(int offset_Left,int offset_Top,int offset_Right,int offset_Bottom)//变换坐标网格
        {
            xySys mySys = new xySys();
            Canvas tmpCanvas = mySys.createSys(MainWindow._WH_CANVAS, MainWindow.NUM, MainWindow.SYSMODE);

            Point loc = new Point();//单元格的位置
            loc.X = MainWindow.X_DOT+(offset_Right-offset_Left);
            loc.Y = MainWindow.Y_DOT+(offset_Bottom-offset_Top);
            MainWindow.X_DOT = MainWindow.X_DOT + (offset_Right - offset_Left);//改变用于记录当前点X坐标的变量
            MainWindow.Y_DOT = MainWindow.Y_DOT + (offset_Bottom - offset_Top);//改变用于记录当前点Y坐标的变量
            
            tmpCanvas = mySys.findAndFill_A_Cell(loc);//返回带点的画布

            return tmpCanvas;
        }
    }
}
