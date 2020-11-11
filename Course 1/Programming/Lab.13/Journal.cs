using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    class Journal
    {
        public List<JournalEntry> arr = new List<JournalEntry>();

        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.collectionname, e.changetype, e.objectinfo);
            arr.Add(je);
        }
        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.collectionname, e.changetype, e.objectinfo);
            arr.Add(je);
        }
        public void Display()
        {
            if (arr.Count == 0)
            {
                Console.WriteLine("Журнал пуст");
            }
            for (int i = 0; i < arr.Count(); i++)
            {
                Console.WriteLine(arr[i].ToString());
            }
        }
    }
}