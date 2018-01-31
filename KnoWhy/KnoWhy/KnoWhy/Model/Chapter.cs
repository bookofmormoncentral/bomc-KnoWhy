using System;
using System.Collections.Generic;
using System.Text;

namespace KnoWhy.Model
{
    public class Chapter
    {
        public int position { get; set; }
        public string name { get; set; }

        public Chapter(int _pos)
        {
            position = _pos;
            if (_pos == 0)
            {
                name = KnoWhy.Current.CONSTANT_ALL_CHAPTERS;
            } else
            {
                name = _pos.ToString();
            }
        }

        public static List<Chapter> getChaptersList(int bookId)
        {
            List<Chapter> list = new List<Chapter>();
            if (bookId > 0)
            {
                Books book = KnoWhy.Current.booksList.ToArray()[bookId];
                int i = 0;
                while (i <= book.chapters)
                {
                    list.Add(new Chapter(i));
                    i++;
                }
            } else
            {
                list.Add(new Chapter(0));
            }
            return list;
        }
    }
}
