using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LearningLinQ
{
    /// <summary>
    /// loads xml data from file
    /// </summary>
    class MainClass
    {

        static void Main()
        {
            XElement foods = XElement.Load(@"/Users/l.apunkt/Desktop/Code Learning/FileExamples/Foods.xml");
            IEnumerable<string> myFood =
                from food in foods.Descendants("breakfast_menu")
                where food.Descendants("food")
                select (string)food.Attribute("name");
            foreach (var item in myFood)
            {
                Console.WriteLine(item);
            }
            

            



        }
    }
}
