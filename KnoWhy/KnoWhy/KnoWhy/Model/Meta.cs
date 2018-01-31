using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KnoWhy.Model
{
    public class Meta : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        public Meta()
        {
        }

        public int nodeId { get; set; }
        private bool _isRead;
        public bool isRead
        {
            get { return _isRead; }
            set
            {
                _isRead = value;
                this.OnPropertyChanged();

            }
        }
        private bool _isFavorite;
        public bool isFavorite
        {
            get { return _isFavorite; }
            set
            {
                _isFavorite = value;
                this.OnPropertyChanged();

            }
        }
        public string title { get; set; }
        public int knowhyNumber { get; set; }
        public string chapterString { get; set; }
        public int book { get; set; }
        public int chapter { get; set; }
        public int verse { get; set; }
        public long timestampPosted { get; set; }
        public string scriptureReference { get; set; }
        public string formattedDate { get; set; }
        public string longDate { get; set; }
        public string mainImageURL { get; set; }
        public string mainImageWidth { get; set; }
        public string mainImageHeight { get; set; }
        //public Article article { get; set; }

        public List<Image> images = new List<Image>();

        public void clearImages()
        {
            images.Clear();
        }

        public void addImage(Image _image)
        {
            images.Add(_image);
        }

        public Image getImage(int width, int height)
        {
            foreach (Image item in images)
            {
                if (item.width >= width) {
                    if (item.height >= height || height == 0) {
                        return item;
                    }
                }
            }
            return null;
        }

        public static int[] getBookAndChapter(int chapter) {
            int book = 0;
            int chapterReal = 0;
            if (chapter == 0) {
                book = 1;
            } else if (chapter >= 1 && chapter <= 22)
            {
                book = 2;
                chapterReal = chapter;
            }
            else if (chapter >= 23 && chapter <= 55)
            {
                book = 3;
                chapterReal = chapter - 22;
            }
            else if (chapter >= 56 && chapter <= 62)
            {
                book = 4;
                chapterReal = chapter - 55;
            }
            else if (chapter >= 63 && chapter <= 63)
            {
                book = 5;
                chapterReal = chapter - 62;
            }
            else if (chapter >= 64 && chapter <= 64)
            {
                book = 6;
                chapterReal = chapter - 63;
            }
            else if (chapter >= 65 && chapter <= 65)
            {
                book = 7;
                chapterReal = chapter - 64;
            }
            else if (chapter >= 66 && chapter <= 66)
            {
                book = 8;
                chapterReal = chapter - 65;
            }
            else if (chapter >= 67 && chapter <= 95)
            {
                book = 9;
                chapterReal = chapter - 66;
            }
            else if (chapter >= 96 && chapter <= 158)
            {
                book = 10;
                chapterReal = chapter - 95;
            }
            else if (chapter >= 159 && chapter <= 174)
            {
                book = 11;
                chapterReal = chapter - 158;
            }
            else if (chapter >= 175 && chapter <= 204)
            {
                book = 12;
                chapterReal = chapter - 174;
            }
            else if (chapter >= 205 && chapter <= 205)
            {
                book = 13;
                chapterReal = chapter - 204;
            }
            else if (chapter >= 206 && chapter <= 214)
            {
                book = 14;
                chapterReal = chapter - 205;
            }
            else if (chapter >= 215 && chapter <= 229)
            {
                book = 15;
                chapterReal = chapter - 214;
            }
            else if (chapter >= 230 && chapter <= 239)
            {
                book = 16;
                chapterReal = chapter - 229;
            }
            int[] result = new int[2];
            result[0] = book;
            result[1] = chapterReal;
            return result;
        }
    }
}
