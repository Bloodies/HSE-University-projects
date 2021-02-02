using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab._8
{
    [Serializable]
    public class Person
    {
        public int id;
        public string name;
        public string surname;
        public string midname;
        public string group;
        public string address;
        public DateTime date;
        public Mark marks;
        public bool delete;

        public Person()
        {
            name = "Пример";
            surname = "Примеров";
            midname = "Примерович";
            group = "П-1-1";
            address = "Улица Примерная";
            date = new DateTime(1, 1, 1);
            marks = new Mark(10, "Примерный");
            delete = false;
        }

        public Person(int in_id, string in_name, string in_surname, string in_midname, string in_group, string in_address, DateTime in_date, Mark in_marks)
        {
            id = in_id;
            name = in_name;
            surname = in_surname;
            midname = in_midname;
            group = in_group;
            address = in_address;
            date = in_date;
            marks = in_marks;
            delete = false;
        }
    }

    [Serializable]
    public class Mark
    {
        public int mark;
        public string name;
        public Mark next;

        public Mark()
        {
            mark = -1;
            name = null;
            next = null;
        }

        public Mark(int data, string lesson)
        {
            mark = data;
            name = lesson;
            next = null;
        }

        public override string ToString()
        {
            string ans = "";
            Mark help = this;
            while (help != null)
            {
                ans += help.name + ": " + help.mark + "\n";
                help = help.next;
            }
            if (ans == ": -1\n") ans = "-";
            return ans;
        }
    }

    class Core
    {
        public static Person student;
        public static string core_file = "datebase.bin", help_file = "help.bin";
        public static int core_id;
        public static bool new_file = false;

        public static Person[] ReadAllStudents(string file)
        {
            Person[] students = new Person[0];
            FileStream f = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    Array.Resize(ref students, students.Length + 1);
                    students[students.Length - 1] = (Person)bf.Deserialize(f);
                }
                catch (System.Runtime.Serialization.SerializationException ex)
                {
                    if (ex.Message == "Конец потока обнаружен до завершения разбора." || ex.Message == "Попытка десериализации пустого потока.")
                        ok = false;
                    else
                    {
                        ok = false;
                        throw new Exception("Ошибка открытия файла");
                    }
                }
                catch { ok = false; }
            }
            f.Close();
            return students;
        }

        public static void DeleteDataBase(string file)
        {
            File.Delete(file);
        }

        public static void AddStudent(Person pers)
        {
            FileStream f = new FileStream(core_file, FileMode.Append, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(f, pers);
            f.Close();
        }

        public static void AddStudent(Person pers, int pos)
        {
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                help = new FileStream(help_file, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();

            for (int i = 1; i < pos; i++)
            {
                try { bf.Serialize(help, (Person)bf.Deserialize(f)); }
                catch { break; }
            }

            bf.Serialize(help, pers);

            bool ok = true;
            while (ok)
            {
                try { bf.Serialize(help, (Person)bf.Deserialize(f)); }
                catch { ok = false; }
            }

            f.Seek(0, SeekOrigin.Begin);
            help.Seek(0, SeekOrigin.Begin);

            ok = true;
            while (ok)
            {
                try { bf.Serialize(f, (Person)bf.Deserialize(help)); }
                catch { ok = false; }
            }

            f.Close();
            help.Close();
            File.Delete(help_file);
        }

        public static int CheckID(int in_ID)
        {
            int id;
            Person pers;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true, find = false;

            while (ok && !find)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.id == in_ID) find = true;
                }
                catch { ok = false; }
            }

            if (find == false) id = -1;
            else id = in_ID;

            f.Close();

            return id;
        }

        public static void DeleteStudent(int in_ID)// нужно дописать, чтобы удаленный по новой не удалялся
        {
            Person pers;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                help = new FileStream(help_file, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.id == in_ID) pers.delete = true;
                    bf.Serialize(help, pers);
                }
                catch { ok = false; }
            }

            f.Seek(0, SeekOrigin.Begin);
            help.Seek(0, SeekOrigin.Begin);

            ok = true;
            while (ok)
            {
                try { bf.Serialize(f, (Person)bf.Deserialize(help)); }
                catch { ok = false; }
            }

            f.Close();
            help.Close();
            File.Delete(help_file);
        }

        public static void CleanFile()
        {
            Person pers;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                help = new FileStream(help_file, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (!pers.delete) bf.Serialize(help, pers);
                }
                catch { ok = false; }
            }

            f.Close();
            f = new FileStream(core_file, FileMode.Truncate, FileAccess.ReadWrite);
            help.Seek(0, SeekOrigin.Begin);

            ok = true;
            while (ok)
            {
                try { bf.Serialize(f, (Person)bf.Deserialize(help)); }
                catch { ok = false; }
            }

            f.Close();
            help.Close();
            File.Delete(help_file);
        }

        public static bool DeletedMoreThanHalfCount()
        {
            Person pers;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;
            int count = 0, delete = 0;

            while (ok)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.delete) delete++;
                    count++;
                }
                catch { ok = false; }
            }

            f.Close();

            if (delete > (count / 2)) return true;
            else return false;
        }

        public static void EditStudent(Person edit_pers)
        {
            Person pers;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                help = new FileStream(help_file, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.id == edit_pers.id) bf.Serialize(help, edit_pers);
                    else bf.Serialize(help, pers);
                }
                catch { ok = false; }
            }

            f.Seek(0, SeekOrigin.Begin);
            help.Seek(0, SeekOrigin.Begin);

            ok = true;
            while (ok)
            {
                try { bf.Serialize(f, (Person)bf.Deserialize(help)); }
                catch { ok = false; }
            }

            f.Close();
            help.Close();
            File.Delete(help_file);
        }

        public static Person FindID(int in_ID)
        {
            Person pers, ans = null;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true, find = false;

            while (ok && !find)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.id == in_ID)
                    {
                        find = true;
                        ans = pers;
                    }
                }
                catch { ok = false; }
            }

            f.Close();

            return ans;
        }

        public static int NumInFile(int in_ID)
        {
            Person pers;
            int ans = 1;
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true, find = false;

            while (ok && !find)
            {
                try
                {
                    pers = (Person)bf.Deserialize(f);
                    if (pers.id == in_ID)
                    {
                        find = true;
                    }
                    else ans++;
                }
                catch { ok = false; }
            }

            if (!find) ans = -1;


            f.Close();

            return ans;
        }

        public static int FindFreeID()
        {
            Person[] students = ReadAllStudents(Core.core_file);
            int ans = 1;
            while (true)
            {
                int count = ans;
                for (int i = 0; i < students.Length; i++)
                    if (students[i] != null && ans == students[i].id)
                    {
                        ans++;
                        break;
                    }
                if (count == ans) return ans;
            }
        }

        public static Person[] ReadBadStudents()
        {
            Person[] students = new Person[0];
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    Person pers = (Person)bf.Deserialize(f);
                    Mark help = pers.marks;
                    bool run = true;
                    while (run)
                    {
                        if (help != null && help.mark <= 3 && help.mark >= 0)
                        {
                            Array.Resize(ref students, students.Length + 1);
                            students[students.Length - 1] = pers;
                            help = help.next;
                        }
                        else if (help == null) run = false;
                        else help = help.next;
                    }
                }
                catch { ok = false; }
            }

            f.Close();

            return students;
        }

        public static string BadLessons()
        {
            string[] lsns = new string[0];
            string ans = "";
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    Person pers = (Person)bf.Deserialize(f);
                    Mark help = pers.marks;
                    bool run = true;
                    while (run)
                    {
                        if (help != null && help.mark <= 3 && help.mark >= 0)
                        {
                            bool find = false;
                            for (int i = 0; i < lsns.Length; i++) if (help.name == lsns[i]) find = true;
                            if (!find)
                            {
                                Array.Resize(ref lsns, lsns.Length + 1);
                                lsns[lsns.Length - 1] = help.name;
                                ans += help.name + "\n";
                            }
                            help = help.next;
                        }
                        else if (help == null) run = false;
                        else help = help.next;
                    }
                }
                catch { ok = false; }
            }

            f.Close();

            return ans
;
        }

        public static string[] AllLessons()
        {
            string[] ans = new string[0];
            FileStream f = new FileStream(core_file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bool ok = true;

            while (ok)
            {
                try
                {
                    Person pers = (Person)bf.Deserialize(f);
                    Mark help = pers.marks;
                    bool run = true;
                    while (run)
                    {
                        if (help != null)
                        {
                            bool find = false;
                            for (int i = 0; i < ans.Length; i++) if (help.name == ans[i]) find = true;
                            if (!find && help.name != null && help.name != "")
                            {
                                Array.Resize(ref ans, ans.Length + 1);
                                ans[ans.Length - 1] = help.name;
                            }
                            help = help.next;
                        }
                        else run = false;
                    }
                }
                catch { ok = false; }
            }

            f.Close();

            return ans;
        }

        public static string StringName(string input)
        {
            string ans = "";
            if (input != "")
            {
                ans += input[0].ToString().ToUpper();
                for (int i = 1; i < input.Length; i++) ans += input[i].ToString().ToLower();
            }
            return ans;
        }
    }
}