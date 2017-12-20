using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication1
{
    class xySysCtrl
    {
        public Canvas toggleMode(int _WH_Canvas, int _NUM_CELL,int _SYS_MODE)
        {
            xySys mySys = new xySys();
            Canvas tmpCanvas=mySys.createSys(_WH_Canvas, _NUM_CELL, _SYS_MODE);

            return tmpCanvas;
        }
    }
}
