using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnoWhy.Interfaces
{
    public interface ListInterface
    {
        void setup();
        void refreshList(bool fistTime);
        void showProgress();
        void hideProgress();
        void updateFilter();
        void showSettings();
        void showArticle(int position);
        void openFilterPanel();
        void closeFilterPanel();
        void hideBooks();
        void showBooks();
        bool isConnected();
    }
}
