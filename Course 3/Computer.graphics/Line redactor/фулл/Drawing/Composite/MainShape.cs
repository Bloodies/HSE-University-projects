using Drawing.Data;
using Drawing.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawing.Composite
{
    class MainShape : Component
    {
        List<Component> children = new List<Component>();

        public MainShape(Shape shape, CoordinateSystemInteractor coordinate)
            : base(shape, coordinate)
        { }

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public Shape GetLastShape()
        {
            return children.Last().Display();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(List<LineData> lines)
        {
            var intersectedIdTags = lines.Select(x => x.Id)
                .Intersect(children.Select(x => (int)(x.Display() as Line).Tag)).ToArray();

            for(var i = 0; i < intersectedIdTags.Length; ++i)
            {
                lines.Remove(lines.Where(x => x.Id == intersectedIdTags[i]).First());
            }

            foreach (var c in children)
            {
                c.Remove();
            }
        }

        public override Shape Display()
        {
            return shape;
        }

        public override void ClearColor()
        {
            foreach(var s in children)
            {
                s.ClearColor();
            }
        }

        public override void MoveShape(Point oldPos, Point newPos)
        {
            foreach (var c in children)
            {
                c.MoveShape(oldPos, newPos);
            }
        }

        public void PickAddShape(Shape shape, List<LineData> lines)
        {
            if (Contains(shape))
                return;

            foreach (var c in children)
            {
                if (c.Contains(shape))
                    return;
            }
            shape = interactor.PickShape(shape);
            Add(new UnderLine(shape as Line, coordinate));

            var intersect = lines.Where(x => x.Id == (int)(children.Last().Display() as Line).Tag).First();
            (children.Last() as UnderLine).SetZ(new double[] { intersect.Z1, intersect.Z2 });
        }

        public override bool Contains(Shape shape)
        {
            return shape == this.shape;
        }

        public override double[] GetEquation()
        {
            return children.Last().GetEquation();
        }

        public override double[] GetCoordinates()
        {
            return children.Last().GetCoordinates();
        }

        public void AddZ(double[] z, List<LineData> lines)
        {
            var line = lines.Where(x => x.Id == (int)(children.Last().Display() as Line).Tag).First();
            line.Z1 = z[0];
            line.Z2 = z[1];
            (children.Last() as UnderLine).SetZ(z);
        }

        public void ProjectReal3D(double zc)
        {
            var childrenCount = children.Count;
            var data = MakeDataFromLines(childrenCount);

            var cm = new ComputingMatrix();
            cm.AddData(data);
            cm.AddOperation(new double[,]
            {
                    {1, 0, 0, 0 },
                    {0, 1, 0, 0 },
                    {0, 0, 0, -1 / zc },
                    {0, 0, 0, 1 }
            });
            cm.ComputeMatrix();

            var result = cm.GetResult();

            for(var i = 0; i < childrenCount; ++i)
            {
                var line = children[i].Display() as Line;
                var newPoint1 = coordinate.ToNormalCoordinates(new Point(result[i * 2, 0], result[i * 2, 1]));
                var newPoint2 = coordinate.ToNormalCoordinates(new Point(result[i * 2 + 1, 0], result[i * 2 + 1, 1]));

                line.X1 += newPoint1[0] - line.X1;
                line.Y1 += newPoint1[1] - line.Y1;
                line.X2 += newPoint2[0] - line.X2;
                line.Y2 += newPoint2[1] - line.Y2;
            }
        }

        public void ComputeReal3D(double[,] operation)
        {
            var childrenCount = children.Count;
            var data = MakeDataFromLines(childrenCount);
            
            ComputingMatrix cm = new ComputingMatrix();
            cm.AddData(data);
            cm.AddOperation(operation);
            cm.ComputeMatrix();

            var result = cm.GetResult();

            for(var i = 0; i < childrenCount; ++i)
            {
                var underLine = children[i] as UnderLine;
                var point1 = coordinate.ToNormalCoordinates(new Point(result[i * 2, 0], result[i * 2, 1]));
                var point2 = coordinate.ToNormalCoordinates(new Point(result[i * 2 + 1, 0], result[i * 2 + 1, 1]));
                var realMatrix = new double[,]
                {
                    { point1[0], point1[1], result[i * 2, 2], result[i * 2, 3] },
                    { point2[0], point2[1], result[i * 2 + 1, 2], result[i * 2 + 1, 3] }
                };
                underLine.SetRealMatrix(realMatrix);
            }
        }

        private double[,] MakeDataFromLines(int n)
        {
            var data = new double[n * 2, 4];
            for (var i = 0; i < n; ++i)
            {
                var line = children[i];
                var dataLine = (line as UnderLine).GetRealMatrix();
                var point1 = coordinate.GetPoint(new Point(dataLine[0, 0], dataLine[0, 1]));
                data[i * 2, 0] = point1[0];
                data[i * 2, 1] = point1[1];
                data[i * 2, 2] = dataLine[0, 2];
                data[i * 2, 3] = 1;

                var point2 = coordinate.GetPoint(new Point(dataLine[1, 0], dataLine[1, 1]));
                data[i * 2 + 1, 0] = point2[0];
                data[i * 2 + 1, 1] = point2[1];
                data[i * 2 + 1, 2] = dataLine[1, 2];
                data[i * 2 + 1, 3] = 1;
            }
            return data;
        }

        internal void SetDataLineCoordinates(List<LineData> lines)
        {
            var intersectedIdTags = lines.Select(x => x.Id)
                    .Intersect(children.Select(x => (int)(x.Display() as Line).Tag)).ToArray();

            foreach(var id in intersectedIdTags)
            {
                var lineTag = children.Where(x => (int)(x.Display() as Line).Tag == id).First() as UnderLine;
                var matrix = lineTag.GetRealMatrix();
                var lineId = lines.Where(x => x.Id == id).First();
                lineId.X1 = matrix[0, 0];
                lineId.Y1 = matrix[0, 1];
                lineId.Z1 = matrix[0, 2];
                lineId.X2 = matrix[1, 0];
                lineId.Y2 = matrix[1, 1];
                lineId.Z2 = matrix[1, 2];
            }
        }

        public List<Shape> GetShapes()
        {
            return children.Select(x => x.Display()).ToList();
        }
    }
}
