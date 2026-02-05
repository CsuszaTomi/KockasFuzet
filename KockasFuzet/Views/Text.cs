using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using KockasFuzet.Controller;
using KockasFuzet.Models;

namespace KockasFuzet.Views
{
    internal class Text
    {
        public static int ArrowMenu(string cim, string[] menupoints)
        {
            bool valasztas = false;
            int mostanipont = 0;
            while (!valasztas)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{cim}");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("=========");

                for (int i = 0; i < menupoints.Length; i++)
                {
                    if (i == mostanipont)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {menupoints[i]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{menupoints[i]}");
                    }
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.E:
                        valasztas = true;
                        break;
                    case ConsoleKey.W:
                        if (mostanipont > 0) mostanipont--;
                        break;
                    case ConsoleKey.S:
                        if (mostanipont < menupoints.Length - 1) mostanipont++;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
            }
            return mostanipont;
        }

        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteWithInfo(string torzs,string info,string utotag, ConsoleColor torzscolor = ConsoleColor.White,ConsoleColor infocolor = ConsoleColor.White)
        {
            Console.ForegroundColor = torzscolor;
            Console.Write($"{torzs}(");
            Console.ForegroundColor = infocolor;
            Console.Write($"{info}");
            Console.ResetColor();
            Console.Write($"){utotag} ");
        }
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
