using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Enums
{
    [Flags]
    public enum VectorActions
    {
        None = 0,
        StartPoint = 1,
        EndPoint = 2,
        Body = 4,
    }

    [Flags]
    public enum OffsetTypes
    {
        Usual = 0,
        MatrixOffset = 1,
        HouseOffset = 2,
    }
}
