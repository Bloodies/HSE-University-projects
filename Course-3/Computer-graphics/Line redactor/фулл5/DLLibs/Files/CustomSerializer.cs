using DLLibs.Enums;
using DLLibs.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DLLibs.Files
{
    [Serializable]
    internal class ForSerialize
    {
        public List<IShape> Shapes;
        public Dictionary<IShape, VectorActions> Groupped;

        public ForSerialize(List<IShape> shapes, Dictionary<IShape, VectorActions> groupped)
        {
            Shapes = shapes;
            Groupped = groupped;
        }
    }

    public class CustomSerializer
    {
        public static void Serialize(string path, List<IShape> shapes, Dictionary<IShape, VectorActions> groupped)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ForSerialize item = new ForSerialize(shapes, groupped);
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }

        public static void Deserialize(string path, ref List<IShape> shapes, ref Dictionary<IShape, VectorActions> groupped)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ForSerialize item;
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                item = (ForSerialize)formatter.Deserialize(fs);
                shapes = item.Shapes;
                groupped = item.Groupped;
            }
        }
    }
}
