using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Data
{
    class GroupData<T> : List<T>
    {
        private int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
