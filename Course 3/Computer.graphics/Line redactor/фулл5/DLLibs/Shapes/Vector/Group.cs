using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLLibs.Enums;

namespace DLLibs.Shapes.Vector
{
    [Serializable]
    public class Group: IShape
    {
        public List<IShape> Data { get; } = new List<IShape>();
        
        public void Draw(System.Drawing.Graphics graphics, Pen pen)
        {
            foreach(var i in Data)
            {
                i.Draw(graphics, pen);
            }
        }

        public float GetDistance(IDot point)
        {
            var minDistance = float.MaxValue;

            foreach(var i in Data)
            {
                var distance = i.GetDistance(point);

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        public VectorActions GetCrossingType(Point point, float sensitivity)
        {
            foreach(var i in Data)
            {
                if (i.GetCrossingType(point, sensitivity) != VectorActions.None)
                {
                    return VectorActions.Body;
                }
            }

            return VectorActions.None;
        }

        public void Offset(OffsetTypes offsetType, params float[] offsets)
        {
            foreach(var i in Data)
            {
                i.Offset(offsetType, offsets);
            }
        }
    }
}
