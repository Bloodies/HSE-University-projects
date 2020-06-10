using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Lab._4__XML_technologies_
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, "../../XMLSchema1.xsd");

            XDocument custOrdDoc = XDocument.Load("../../XMLFile1.xml");
            bool errors = false;
            custOrdDoc.Validate(schemas, (o, e) =>
            {
                Console.WriteLine(e.Message);
                errors = true;
            });
            if (errors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Документ невалидный!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Документ валидный!");
                Console.ForegroundColor = ConsoleColor.White;
                XPath(custOrdDoc);
                XTransform();
            }
            Console.ReadKey();
        }
        static void XPath(XDocument xDoc)
        {
            Console.WriteLine("Занаятия на неделе:");
            XElement xRoot = xDoc.Root;
            var elList = xDoc.XPathSelectElements("//subject");
            foreach (var node in elList)
            {
                Console.WriteLine(node.Attribute("type").Value + " " + node.Element("name").Value + " Препод.: " + node.Element("teacher").Value + " Ауд.: " + node.Element("class").Value);
            }

            Console.WriteLine("===============================");
            //=======================================================================
            Console.WriteLine("Аудитории:");
            elList = xDoc.XPathSelectElements("//class").GroupBy(n => n.Value).Select(n => n.First()).OrderBy(n => n.Value[4]).ThenBy(n => n.Value.Substring(0, 3));
            foreach (var node in elList)
            {
                Console.Write(node.Value + ", ");
            }

            Console.WriteLine();
            Console.WriteLine("===============================");
            //=======================================================================
            Console.WriteLine("Семинары:");
            elList = xDoc.XPathSelectElements("//subject[@type='семинар']");
            foreach (var node in elList)
            {
                Console.WriteLine(node.Attribute("type").Value + " " + node.Element("name").Value + " Препод.: " + node.Element("teacher").Value + " Ауд.: " + node.Element("class").Value);
            }
            Console.WriteLine();
            Console.WriteLine("===============================");
            //=======================================================================
            Console.Write("Введите аудиторию(аудитория[корпус]): ");
            string aud = Console.ReadLine();
            elList = xDoc.XPathSelectElements($"//subject[class='{aud.Trim()}']");
            if (elList.Count() != 0)
            {
                foreach (var node in elList)
                {
                    Console.WriteLine(node.Attribute("type").Value + " " + node.Element("name").Value + " Препод.: " + node.Element("teacher").Value + " Ауд.: " + node.Element("class").Value);
                }
            }
            else
            {
                Console.WriteLine("В этой аудитории нет пар.");
            }

            Console.WriteLine();
            Console.WriteLine("===============================");
            //=======================================================================
            Console.Write("Введите аудиторию(аудитория[корпус]): ");
            aud = Console.ReadLine();
            elList = xDoc.XPathSelectElements($"//subject[class='{aud.Trim()}'][@type='семинар']/teacher").GroupBy(n => n.Value).Select(n => n.First()).OrderBy(n => n.Value);
            if (elList.Count() != 0)
            {
                foreach (var node in elList)
                {
                    Console.Write(node.Value + ", ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("В этой аудитории нет пар.");
            }

            Console.WriteLine();
            Console.WriteLine("===============================");
            //=======================================================================
            Console.WriteLine("Последние занятия:");
            elList = xDoc.XPathSelectElements($"//subject[last()]");
            foreach (var node in elList)
            {
                Console.WriteLine(node.Parent.Attribute("weekday").Value + ": " + node.Attribute("type").Value + " " + node.Element("name").Value + " Препод.: " + node.Element("teacher").Value + " Ауд.: " + node.Element("class").Value);
            }

            Console.WriteLine("===============================");
            //=======================================================================
            Console.Write("Кол-во занятий на неделе: ");
            Console.WriteLine(xDoc.XPathEvaluate("count(//subject)"));
        }
        static void XTransform()
        {
            XslCompiledTransform xslt = new XslCompiledTransform(true);
            xslt.Load("../../XSLTFile1.xslt");
            xslt.Transform("../../XMLFile1.xml", "C:\\Users\\SRuza\\Desktop\\XML\\timetable.txt");
            xslt.Load("../../XSLTFile2.xslt");
            xslt.Transform("../../XMLFile1.xml", "C:\\Users\\SRuza\\Desktop\\XML\\timetable.html");
        }
    }
}
