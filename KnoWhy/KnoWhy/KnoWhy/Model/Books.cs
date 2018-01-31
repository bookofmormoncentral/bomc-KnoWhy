using System;
namespace KnoWhy.Model
{
    public class Books
    {
        public int position { get; set; }
        public string name { get; set; }
        public int chapters { get; set; }

        public Books(int _pos, string locale)
        {
            position = _pos;
            if (_pos == 0)
            {
                chapters = 0;
                name = "All Books";
                if (locale == "es") {
                    name = "Todos los libros";
                }
            }
            else if (_pos == 1)
            {
                chapters = 0;
                name = "Introduction & Witnesses";
                if (locale == "es")
                {
                    name = "Introducción y Testigos";
                }
            }
            else if (_pos == 2)
            {
                chapters = 22;
                name = "1 Nephi";
                if (locale == "es")
                {
                    name = "1 Nefi";
                }
            }
            else if (_pos == 3)
            {
                chapters = 33;
                name = "2 Nephi";
                if (locale == "es")
                {
                    name = "2 Nefi";
                }
            }
            else if (_pos == 4)
            {
                chapters = 7;
                name = "Jacob";
                if (locale == "es")
                {
                    name = "Jacob";
                }
            }
            else if (_pos == 5)
            {
                chapters = 0;
                name = "Enos";
                if (locale == "es")
                {
                    name = "Enós";
                }
            }
            else if (_pos == 6)
            {
                chapters = 0;
                name = "Jarom";
                if (locale == "es")
                {
                    name = "Jarom";
                }
            }
            else if (_pos == 7)
            {
                chapters = 0;
                name = "Omni";
                if (locale == "es")
                {
                    name = "Omni";
                }
            }
            else if (_pos == 8)
            {
                chapters = 0;
                name = "Words of Mormon";
                if (locale == "es")
                {
                    name = "Palabras de Mormón";
                }
            }
            else if (_pos == 9)
            {
                chapters = 29;
                name = "Mosiah";
                if (locale == "es")
                {
                    name = "Mosíah";
                }
            }
            else if (_pos == 10)
            {
                chapters = 63;
                name = "Alma";
                if (locale == "es")
                {
                    name = "Alma";
                }
            }
            else if (_pos == 11)
            {
                chapters = 16;
                name = "Helaman";
                if (locale == "es")
                {
                    name = "Helamán";
                }
            }
            else if (_pos == 12)
            {
                chapters = 30;
                name = "3 Nephi";
                if (locale == "es")
                {
                    name = "3 Nefi";
                }
            }
            else if (_pos == 13)
            {
                chapters = 0;
                name = "4 Nephi";
                if (locale == "es")
                {
                    name = "4 Nefi";
                }
            }
            else if (_pos == 14)
            {
                chapters = 9;
                name = "Mormon";
                if (locale == "es")
                {
                    name = "Mormón";
                }
            }
            else if (_pos == 15)
            {
                chapters = 15;
                name = "Ether";
                if (locale == "es")
                {
                    name = "Éter";
                }
            }
            else if (_pos == 16)
            {
                chapters = 10;
                name = "Moroni";
                if (locale == "es")
                {
                    name = "Moroni";
                }
            }
        }

    }
}
