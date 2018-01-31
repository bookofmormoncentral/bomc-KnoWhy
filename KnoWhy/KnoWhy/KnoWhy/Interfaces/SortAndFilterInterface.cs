using System;
namespace KnoWhy.Interfaces
{
    public interface SortAndFilterInterface
    {
		void sortByDate();
		void sortByChapter();
		void sortAsc();
		void sortDesc();
		void toggleFavorites();
		void toggleUnreaded();
		void reloadData();
    }
}
