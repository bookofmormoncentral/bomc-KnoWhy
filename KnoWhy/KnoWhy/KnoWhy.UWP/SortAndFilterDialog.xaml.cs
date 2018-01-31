using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using KnoWhy;
using KnoWhy.Interfaces;
using KnoWhy.Model;
using System.Collections.ObjectModel;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace KnoWhy.UWP
{
    public sealed partial class SortAndFilterDialog : ContentDialog, SortAndFilterInterface, INotifyPropertyChanged
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

        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _sortLabel;
        public string sortLabel
        {
            get { return _sortLabel; }
            set
            {
                _sortLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _filterLabel;
        public string filterLabel
        {
            get { return _filterLabel; }
            set
            {
                _filterLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _doneLabel;
        public string doneLabel
        {
            get { return _doneLabel; }
            set
            {
                _doneLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _sortByDateLabel;
        public string sortByDateLabel
        {
            get { return _sortByDateLabel; }
            set
            {
                _sortByDateLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _sortByChapterLabel;
        public string sortByChapterLabel
        {
            get { return _sortByChapterLabel; }
            set
            {
                _sortByChapterLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _ascLabel;
        public string ascLabel
        {
            get { return _ascLabel; }
            set
            {
                _ascLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _descLabel;
        public string descLabel
        {
            get { return _descLabel; }
            set
            {
                _descLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _favoritesLabel;
        public string favoritesLabel
        {
            get { return _favoritesLabel; }
            set
            {
                _favoritesLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _unreadedLabel;
        public string unreadedLabel
        {
            get { return _unreadedLabel; }
            set
            {
                _unreadedLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _bookAndChapterLabel;
        public string bookAndChapterLabel
        {
            get { return _bookAndChapterLabel; }
            set
            {
                _bookAndChapterLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _bookLabel;
        public string bookLabel
        {
            get { return _bookLabel; }
            set
            {
                _bookLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private string _chapterLabel;
        public string chapterLabel
        {
            get { return _chapterLabel; }
            set
            {
                _chapterLabel = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private bool _filterEnabled;
        public bool filterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private int _sortType;
        public int sortType
        {
            get { return _sortType; }
            set
            {
                _sortType = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private int _sortMode;
        public int sortMode
        {
            get { return _sortMode; }
            set
            {
                _sortMode = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private bool _filterFavorites;
        public bool filterFavorites
        {
            get { return _filterFavorites; }
            set
            {
                _filterFavorites = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private bool _filterUnreaded;
        public bool filterUnreaded
        {
            get { return _filterUnreaded; }
            set
            {
                _filterUnreaded = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private bool _filterBookAndChapter;
        public bool filterBookAndChapter
        {
            get { return _filterBookAndChapter; }
            set
            {
                _filterBookAndChapter = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private int _filterBookId;
        public int filterBookId
        {
            get { return _filterBookId; }
            set
            {
                _filterBookId = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        private int _filterChapterId;
        public int filterChapterId
        {
            get { return _filterChapterId; }
            set
            {
                _filterChapterId = value;
                //onPropertyChanged(this, "score");
                this.OnPropertyChanged();

            }
        }

        ObservableCollection<Books> booksList = new ObservableCollection<Books>();
        ObservableCollection<Chapter> chaptersList = new ObservableCollection<Chapter>();

        public SortAndFilterDialog()
        {
            this.InitializeComponent();

            title = KnoWhy.Current.CONSTANT_SORT_AND_FILTER;
            sortLabel = KnoWhy.Current.CONSTANT_SORT;
            doneLabel = KnoWhy.Current.CONSTANT_DONE;
            filterLabel = KnoWhy.Current.CONSTANT_FILTER;
            sortByDateLabel = KnoWhy.Current.CONSTANT_PUBLISH_DATE;
            sortByChapterLabel = KnoWhy.Current.CONSTANT_CHAPTER_VERSE;
            ascLabel = KnoWhy.Current.CONSTANT_ASCEND_C;
            descLabel = KnoWhy.Current.CONSTANT_DESCEND_C;
            favoritesLabel = KnoWhy.Current.CONSTANT_FAVORITES;
            unreadedLabel = KnoWhy.Current.CONSTANT_UNREAD;
            bookAndChapterLabel = KnoWhy.Current.CONSTANT_BOOK_CHAPTER;
            bookLabel = KnoWhy.Current.CONSTANT_BOOK;
            chapterLabel = KnoWhy.Current.CONSTANT_CHAPTER;

            

            
        }

        public void updateBooksList()
        {
            booksList.Clear();
            foreach (Books item in KnoWhy.Current.booksList)
            {
                booksList.Add(item);
            }
            comboBooks.SelectedIndex = KnoWhy.Current.filterBookId;
        }

        public void updateChaptersList()
        {
            chaptersList.Clear();
            List<Chapter> list = Chapter.getChaptersList(KnoWhy.Current.filterBookId);
            foreach (Chapter item in list)
            {
                chaptersList.Add(item);
            }
            comboChapters.SelectedIndex = KnoWhy.Current.filterChapterId;
        }

        public void sortByDate()
        {
            KnoWhy.Current.sortByDate();
        }

        public void sortByChapter()
        {
            KnoWhy.Current.sortByChapter();
        }

        public void sortAsc()
        {
            KnoWhy.Current.sortAsc();
        }

        public void sortDesc()
        {
            KnoWhy.Current.sortDesc();
        }

        public void toggleFavorites()
        {
            KnoWhy.Current.toggleFavorites();
        }

        public void toggleUnreaded()
        {
            KnoWhy.Current.toggleUnreaded();
        }

        public void toggleBookAndChapter()
        {
            KnoWhy.Current.toggleBookAndChapter();
        }

        public void reloadData()
        {
            filterEnabled = KnoWhy.Current.filterEnabled;
            sortType = KnoWhy.Current.sortType;
            sortMode = KnoWhy.Current.sortMode;
            filterFavorites = KnoWhy.Current.filterFavorites;
            filterUnreaded = KnoWhy.Current.filterUnreaded;
            filterBookAndChapter = KnoWhy.Current.filterBookAndChapter;
            filterBookId = KnoWhy.Current.filterBookId;
            filterChapterId = KnoWhy.Current.filterChapterId;

            updateBooksList();
            updateChaptersList();

            /*comboBooks.SelectedValue = filterBookId;
            comboChapters.SelectedValue = filterChapterId;*/
        }

        private void buttonDone_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void buttonSortDate_Click(object sender, RoutedEventArgs e)
        {
            sortByDate();
            reloadData();
        }

        private void buttonSortChapter_Click(object sender, RoutedEventArgs e)
        {
            sortByChapter();
            reloadData();
        }

        private void buttonAsc_Click(object sender, RoutedEventArgs e)
        {
            sortAsc();
            reloadData();
        }

        private void buttonDesc_Click(object sender, RoutedEventArgs e)
        {
            sortDesc();
            reloadData();
        }

        private void buttonFavorites_Click(object sender, RoutedEventArgs e)
        {
            toggleFavorites();
            reloadData();
        }

        private void buttonUnreaded_Click(object sender, RoutedEventArgs e)
        {
            toggleUnreaded();
            reloadData();
        }

        private void buttonBookAndChapter_Click(object sender, RoutedEventArgs e)
        {
            toggleBookAndChapter();
            reloadData();
        }

        private void comboBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = ((ComboBox)sender).SelectedIndex;
            if (val != filterBookId && val > -1)
            {
                KnoWhy.Current.filterBookId = val;
                KnoWhy.Current.filterChapterId = 0;
                filterBookId = KnoWhy.Current.filterBookId;
                filterChapterId = KnoWhy.Current.filterChapterId;
                updateChaptersList();
                //reloadData();

            }
        }

        private void comboChapters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = ((ComboBox)sender).SelectedIndex;
            if (val != filterChapterId && val > -1)
            {
                KnoWhy.Current.filterChapterId = val;
                filterChapterId = KnoWhy.Current.filterChapterId;
                //reloadData();
            }
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            reloadData();
        }
    }
}
