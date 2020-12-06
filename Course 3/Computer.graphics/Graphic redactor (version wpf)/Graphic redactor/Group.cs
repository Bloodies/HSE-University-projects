using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Graphic_redactor
{
    [Serializable]
    public class LineGroup : CanvasObject
    {
        public List<MyLine> Lines { get; set; }
        public List<CanvasObject> GroupedObjects { get; set; }

        public LineGroup(List<CanvasObject> lines)
        {
            Lines = new List<MyLine>();

            foreach (CanvasObject canvasObject in lines)
            {
                if (canvasObject is MyLine line)
                {
                    line.Group = this;
                    Lines.Add(line);
                }
                else if (canvasObject is LineGroup group)
                {
                    Lines.AddRange(group.Lines);
                }
            }

            GroupedObjects = new List<CanvasObject>();
            GroupedObjects.AddRange(lines);
        }

        public void Move(Vector delta)
        {
            Lines.ForEach(x => x.Move(delta));
        }

        public void Select()
        {
            Lines.ForEach(x => x.Select());
        }

        public void Deselect()
        {
            Lines.ForEach(x => x.Deselect());
        }

        public override int GetHashCode()
        {
            return Lines.Sum(x => x.GetHashCode());
        }

        public List<Line> GetLines()
        {
            return Lines.Select(x => x.Line).ToList();
        }
    }
}
