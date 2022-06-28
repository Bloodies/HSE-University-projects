using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Shapes
{
    public interface IDot
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
        float LocalX { get; set; }
        float LocalY { get; set; }
        float LocalZ { get; set; }
        float UnifCoordinate { get; set; }

        string ToString();

        IDot Offset(params float[] offsets);

        IDot Normalise();
    }
}
