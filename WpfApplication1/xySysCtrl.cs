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
        //切换坐标系填充模式
        public Canvas toggleMode(int _SYS_MODE)
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

        //描点函数
        public Canvas drawDot(bool isFill)
        {
            xySys mySys = new xySys();
            Canvas tmpCanvas = mySys.createSys(MainWindow._WH_CANVAS, MainWindow.NUM, MainWindow.SYSMODE);
                        
            Point loc = new Point();//单元格的位置
            loc.X = MainWindow.X_DOT;
            loc.Y = MainWindow.Y_DOT;
            tmpCanvas = mySys.findAndFill_A_Cell(loc);//返回带点的画布

            return tmpCanvas;
        }
    }
}
