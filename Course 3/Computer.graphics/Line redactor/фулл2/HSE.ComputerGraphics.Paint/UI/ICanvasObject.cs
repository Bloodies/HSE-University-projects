using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace HSE.ComputerGraphics.Paint.UI
{
    public interface ICanvasObject
    {
        void Move(Vector delta);
        void Select();
        void Deselect();
        List<Line> GetLines();
    }
}
