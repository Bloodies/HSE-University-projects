using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    public class CollectionHandlerEventArgs : System.EventArgs
    {
        public string collectionname;
        public string changetype;
        public object objectinfo;

        public CollectionHandlerEventArgs()
        {
            collectionname = "Не определено";
            changetype = "Не определено";
            objectinfo = null;
        }
        public CollectionHandlerEventArgs(string Collectionname, string Changetype, object Objectinfo)
        {
            collectionname = Collectionname;
            changetype = Changetype;
            objectinfo = Objectinfo;
        }
        public override string ToString()
        {
            if (objectinfo != null)
            {
                return $"Имя коллекции - {collectionname}, тип изменений - {changetype}, объект - {objectinfo.ToString()}";
            }
            else
            {
                return $"Имя коллекции - {collectionname}, тип изменений - {changetype}, объект - не определен";
            }
        }
    }
}