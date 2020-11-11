using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    class JournalEntry
    {
        string collectionname;
        string changetype;
        object objectinfo;

        public JournalEntry()
        {
            collectionname = "Не определено";
            changetype = "Не определено";
        }
        public JournalEntry(string Collectionname, string Changetype, object Objectinfo)
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