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
        //_SYS_MODE,网格填充模式，默认为单元格完全填充，值为0；为1是内切圆填充；其它值无效
        public Canvas createSys(int _WH_Canvas, int _NUM_CELL,int _SYS_MODE=0)
        {
            return createSys_PRIVATE(_WH_Canvas, _NUM_CELL, _SYS_MODE);
        }

        //隐藏xySys函数实现细节的函数
        private Canvas createSys_PRIVATE(int _WH_Canvas, int _NUM_CELL,int _SYS_MODE)
        {
            //if (_SYS_MODE != 0 || _SYS_MODE != 1)
            //{
            //    throw new ArgumentException("填充模式_SYS_MODE参数不正确");
            //}

            Point xy_start = new Point();//起点
            Point xy_end = new Point();//终点
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
            
            if (_SYS_MODE==0)//如果填充模式是完全填充，则直接返回画布对象
            {
                return _Temp_Canvas;
            }

            double RADIUS = 0.5 * _WH_Canvas / _NUM_CELL;//半径

            {
                //给每个单元格都填充一个内切圆
                for (int j = 0; j < _NUM_CELL;j++ )
                {
                    //绘制横向内切圆
                    for (int i = 0; i < _NUM_CELL; i++)
                    {
                        Point CENTER_XY = new Point();//圆心
                        CENTER_XY.X = 0.5 * (2 * i + 1) * _WH_Canvas / _NUM_CELL;
                        CENTER_XY.Y = 0.5 *(2*j+1)* _WH_Canvas / _NUM_CELL;
                        DrawCircle(CENTER_XY, RADIUS);
                    }

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

            _Temp_Canvas.Children.Add(myPath);//把图像添加到待返回的临时canvas对象上
        }  

        //画一个圆
        private void DrawCircle(Point CENTER_XY, double RADIUS)
        {
            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = CENTER_XY;//设置圆心坐标
            myEllipseGeometry.RadiusX = RADIUS;//设置X方向径长
            myEllipseGeometry.RadiusY = RADIUS;//设置Y方向径长
            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = myEllipseGeometry;

            _Temp_Canvas.Children.Add(myPath);//把图像添加到待返回的临时canvas对象上
        }
    }
}
