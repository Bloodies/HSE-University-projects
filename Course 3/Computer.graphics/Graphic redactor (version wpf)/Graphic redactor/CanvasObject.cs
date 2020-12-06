using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace Graphic_redactor
{
    public interface CanvasObject
    {
        void Move(Vector delta);
        void Select();
        void Deselect();
        List<Line> GetLines();
    }
}
