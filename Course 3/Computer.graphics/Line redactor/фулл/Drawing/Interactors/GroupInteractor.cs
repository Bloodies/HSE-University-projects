using Drawing.Composite;
using Drawing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawing.Interactors
{
    class GroupInteractor
    {
        private GroupData<MainShape> shapes = new GroupData<MainShape>();

        public bool AddGroup(MainShape shape, Brush color)
        {
            if (CheckIntersectedShapes(shape))
                return false;
            shapes.Add(shape);
            foreach(var s in shapes.Last().GetShapes())
            {
                s.Stroke = color;
            }
            shapes.ID++;
            return true;
        }

        public bool ContainsShape(MainShape shape)
        {
            return CheckIntersectedShapes(shape);
        }

        public List<List<Shape>> GetFigures()
        {
            return shapes.Select(x => x.GetShapes()).ToList();
        } 

        public void ClearColors(MainShape shape)
        {
            var shapesMainShapeTag = shape.GetShapes().Select(x => x.Tag);
            foreach(var s in shapes)
            {
                if (s.GetShapes().Select(x => x.Tag).Intersect(shapesMainShapeTag).Any())
                    continue;
                foreach(var us in s.GetShapes())
                {
                    us.Stroke = Brushes.Black;
                }
            }
        }

        public void RemoveShapes()
        {
            shapes.RemoveRange(0, shapes.Count);
        }

        private bool CheckIntersectedShapes(MainShape shape)
        {
            var shapesMainShape = shape.GetShapes().Select(x => x.Tag).ToList();
            var addShapes = shapes.Select(x => x.GetShapes().Select(y => y.Tag));
            List<object> intersected = new List<object>();
            foreach (var s in addShapes)
            {
                intersected.AddRange(s);
            }
            intersected = intersected.Intersect(shapesMainShape).ToList();
            return intersected.Any();
        }
    }
}
