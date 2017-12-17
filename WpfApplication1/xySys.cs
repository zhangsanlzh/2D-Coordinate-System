using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// name:lzh
    /// description:坐标系类
    /// date:2017.12.16
    /// </summary>   
    class xySys
    {
        //待返回的临时canvas对象
        Canvas _Temp_Canvas = new Canvas(); 

        //_WH_Canvas，显示各种图像的画布的边长
        //_NUM_CELL,单元格的个数（坐标系的阶数）
        public Canvas createSys(int _WH_Canvas, int _NUM_CELL)
        {
            return createSys_PRIVATE(_WH_Canvas, _NUM_CELL);
        }

        //隐藏xySys函数实现细节的函数
        private Canvas createSys_PRIVATE(int _WH_Canvas, int _NUM_CELL)
        {
            //声明起点和终点对象并初始化XY坐标值
            Point xy_start = new Point();
            Point xy_end = new Point();
            xy_start.X = 0;
            xy_start.Y = 0;
            xy_end.X = 0;
            xy_end.Y = 0;

            {
                //绘制横线
                xy_start.X = 0;
                for (int j = 0; j <= _NUM_CELL; j++)
                {
                    xy_start.Y = j * _WH_Canvas / _NUM_CELL;
                    xy_end.X = _WH_Canvas;
                    xy_end.Y = xy_start.Y;
                    DrawLine(xy_start, xy_end);
                }
            }

            {
                //绘制纵线
                xy_start.Y = 0;
                for (int j = 0; j <= _NUM_CELL; j++)
                {
                    xy_start.X = j * _WH_Canvas / _NUM_CELL;
                    xy_end.X = xy_start.X;
                    xy_end.Y = _WH_Canvas;
                    DrawLine(xy_start, xy_end);
                }
            }

            return _Temp_Canvas;

        }

        //画一条线
        private void DrawLine(Point startPt, Point endPt)
        {
            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = startPt;
            myLineGeometry.EndPoint = endPt;
            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = myLineGeometry;

            //把图像添加到待返回的临时canvas对象上
            _Temp_Canvas.Children.Add(myPath);
        }  
    }
}
