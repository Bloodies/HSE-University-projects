using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);    //делегат

    class MyNewCollection : MyCollection
    {
        public MyNewCollection(string Name) : base(Name)
        {

        }

        //происходит при добавлении нового элемента или при удалении элемента из //коллекции
        public event CollectionHandler CollectionCountChanged;
        //объекту коллекции присваивается новое значение       
        public event CollectionHandler CollectionReferenceChanged;

        //обработчик события CollectionCountChanged
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }
        //обработчик события OnCollectionReferenceChanged
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }

        public Time this[int index]
        {
            get
            {
                return arr[index];
            }
            set
            {
                arr[index] = value;
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(name, "изменение ссылки на элемент", arr[index]));
            }
        }

        public override bool Remove(Time item)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(name, "удаление элемента", item));
            return base.Remove(item);
        }

        public override void Add(Time info)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(name, "добавление элемента", info));
            base.Add(info);
        }

        public override void Fill(int num)
        {
            Random rnd = new Random();
            arr.Clear();
            for (int i = 0; i < num; i++)
            {
                Time item = new Time(rnd.Next(23), rnd.Next(59));
                arr.Add(item);
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(name, "добавление элемента", item));
            }


        }

        public override void SortMinutes()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = i; j < arr.Count(); j++)
                {
                    if (this[i].Minutes > this[j].Minutes)
                    {
                        Time temp = this[j];
                        this[j] = this[i];
                        this[i] = temp;
                    }
                }
            }
        }

        public override void SortHours()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = i; j < arr.Count(); j++)
                {
                    if (this[i].Hours > this[j].Hours)
                    {
                        Time temp = this[j];
                        this[j] = this[i];
                        this[i] = temp;
                    }
                }
            }
        }

        public override void Clear()
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(name, "очистка коллекции", null));
            base.Clear();
        }

        public override void Display()
        {
            base.Display();
        }

    }
}