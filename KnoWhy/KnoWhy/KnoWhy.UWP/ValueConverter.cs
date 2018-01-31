using KnoWhy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace KnoWhy.UWP
{
    public class ValueConverter : IValueConverter
    {
        public string mode { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (mode == "1")
            {
                bool visible = (bool)value;
                if (visible != true)
                {
                    return Visibility.Visible;
                } else
                {
                    return Visibility.Collapsed;
                }
            } else if (mode == "2")
            {
                bool filterEnabled = (bool)value;
                if (filterEnabled == true)
                {
                    return "\uf101";
                } else
                {
                    return "\uf100";
                }
            } else if (mode == "3")
            {
                bool visible = (bool)value;
                if (visible == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "4")
            {
                int item = (int)value;
                if (item == KnoWhy.SORT_BY_DATE)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "5")
            {
                int item = (int)value;
                if (item == KnoWhy.SORT_BY_CHAPTER)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "6")
            {
                int item = (int)value;
                if (item == KnoWhy.SORT_ASC)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "7")
            {
                int item = (int)value;
                if (item == KnoWhy.SORT_DESC)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "8")
            {
                bool visible = (bool)value;
                if (visible == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "9")
            {
                bool visible = (bool)value;
                if (visible == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "10")
            {
                bool visible = (bool)value;
                if (visible == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "11")
            {
                string visible = (string)value;
                if (visible != "")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "12")
            {
                bool isChecked = (bool)value;
                if (isChecked == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "13")
            {
                bool isChecked = (bool)value;
                if (isChecked == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "14")
            {
                int filterBookId = (int)value;
                if (filterBookId > 0)
                {
                    Books book = KnoWhy.Current.booksList.ToArray()[filterBookId];
                    if (book.chapters > 0 && KnoWhy.Current.filterBookAndChapter == true)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else if (mode == "15")
            {
                if (KnoWhy.Current.filterBookId == 0)
                {
                    return Visibility.Collapsed;
                } else
                {
                    return Visibility.Visible;
                }
            }
            else if (mode == "16")
            {
                if (KnoWhy.Current.filterBookId == 16 && KnoWhy.Current.filterChapterId == 10) //The last book id is 16 and the last chapter of that book is 10
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            else if (mode == "17")
            {
                int count = (int)value;
                if (count > 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
