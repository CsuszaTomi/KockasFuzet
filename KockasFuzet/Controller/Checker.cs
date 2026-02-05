using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Controller
{
    internal class Checker
    {
        public static bool DateTimeChecker(string date)
        {
            try
            {
                DateTime.Parse(date);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IntChecker(string number)
        {
            try
            {
                int.Parse(number);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DecimalChecker(string number)
        {
            try
            {
                decimal.Parse(number);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool LenghtChecker(string text, int min, int max)
        {
            if (text.Length >= min && text.Length <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
