using System;
using System.Linq;
using Realms;
using System.Threading.Tasks;

namespace KnoWhy.Model
{
    public class Settings : RealmObject
    {
        public int sortType { get; set; }
        public int sortMode { get; set; }
        public bool filterEnabled { get; set; }
        public bool filterFavorites { get; set; }
        public bool filterUnreaded { get; set; }
        public bool filterBookAndChapter { get; set; }
        public int filterBookId { get; set; }
        public int filterChapterId { get; set; }
        public long lastUpdate { get; set; }
        public bool onlyWiFi { get; set; }
        public string languagePrevious { get; set; }
        public int lastNode { get; set; }

        public Settings()
        {
        }

        public static Settings getSettings(bool first)
        {

            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            var items = realm.All<Settings>();
            if (items.Count() > 0)
            {
                Settings settings = items.First<Settings>();
                if (first == true)
                {
                    KnoWhy.Current.sortType = settings.sortType;
                    KnoWhy.Current.sortMode = settings.sortMode;
                    KnoWhy.Current.filterEnabled = settings.filterEnabled;
                    KnoWhy.Current.filterFavorites = settings.filterFavorites;
                    KnoWhy.Current.filterUnreaded = settings.filterUnreaded;
                    KnoWhy.Current.filterBookAndChapter = settings.filterBookAndChapter;
                    KnoWhy.Current.filterBookId = settings.filterBookId;
                    KnoWhy.Current.filterChapterId = settings.filterChapterId;
                    KnoWhy.Current.lastUpdate = settings.lastUpdate;
                    KnoWhy.Current.onlyWiFi = settings.onlyWiFi;
                    KnoWhy.Current.languagePrevious = settings.languagePrevious;
                    if (KnoWhy.Current.languagePrevious == null) {
                        KnoWhy.Current.languagePrevious = KnoWhy.Current.locale;
                    }
                    KnoWhy.Current.lastNode = settings.lastNode;
                }
                return settings;
            }
            else
            {
                var settings = new Settings
                {
                    sortType = KnoWhy.Current.sortType,
                    sortMode = KnoWhy.Current.sortMode,
                    filterEnabled = KnoWhy.Current.filterEnabled,
                    filterFavorites = KnoWhy.Current.filterFavorites,
                    filterUnreaded = KnoWhy.Current.filterUnreaded,
                    filterBookAndChapter = KnoWhy.Current.filterBookAndChapter,
                    filterBookId = KnoWhy.Current.filterBookId,
                    filterChapterId = KnoWhy.Current.filterChapterId,
                    lastUpdate = KnoWhy.Current.lastUpdate,
                    onlyWiFi = KnoWhy.Current.onlyWiFi,
                    languagePrevious = KnoWhy.Current.languagePrevious,
                    lastNode = KnoWhy.Current.lastNode
                };
                realm.Write(() => realm.Add(settings));

                return settings;
            }
        }

        public static void updateSortType(int value) {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().sortType = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateSortMode(int value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().sortMode = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterEnabled(bool value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterEnabled = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterFavorites(bool value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterFavorites = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterUnreaded(bool value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterUnreaded = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterBookAndChapter(bool value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterBookAndChapter = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterBookId(int value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterBookId = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void updateFilterChapterId(int value)
        {
            /*new Thread(() =>
            {
                var realm = KnoWhy.Current.getDatabase();
                using (var trans = realm.BeginWrite())
                {
                    Settings.getSettings().filterChapterId = value;
                    trans.Commit();
                }
            }).Start();*/
        }

        public static void saveChanges()
        {
            new Task(() =>
            {
                var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
                using (var trans = realm.BeginWrite())
                {
                    Settings settings = Settings.getSettings(false);
                    settings.sortType = KnoWhy.Current.sortType;
                    settings.sortMode = KnoWhy.Current.sortMode;
                    settings.filterEnabled = KnoWhy.Current.filterEnabled;
                    settings.filterFavorites = KnoWhy.Current.filterFavorites;
                    settings.filterUnreaded = KnoWhy.Current.filterUnreaded;
                    settings.filterBookAndChapter = KnoWhy.Current.filterBookAndChapter;
                    settings.filterBookId = KnoWhy.Current.filterBookId;
                    settings.filterChapterId = KnoWhy.Current.filterChapterId;
                    settings.lastUpdate = KnoWhy.Current.lastUpdate;
                    settings.onlyWiFi = KnoWhy.Current.onlyWiFi;
                    settings.languagePrevious = KnoWhy.Current.languagePrevious;
                    settings.lastNode = KnoWhy.Current.lastNode;
                    trans.Commit();
                }
            }).Start();
        }

        public void save()
        {
            this.sortType = KnoWhy.Current.sortType;
            this.sortMode = KnoWhy.Current.sortMode;
            this.filterEnabled = KnoWhy.Current.filterEnabled;
            this.filterFavorites = KnoWhy.Current.filterFavorites;
            this.filterUnreaded = KnoWhy.Current.filterUnreaded;
            this.filterBookAndChapter = KnoWhy.Current.filterBookAndChapter;
            this.filterBookId = KnoWhy.Current.filterBookId;
            this.filterChapterId = KnoWhy.Current.filterChapterId;
            this.lastUpdate = KnoWhy.Current.lastUpdate;
            this.onlyWiFi = KnoWhy.Current.onlyWiFi;
            this.languagePrevious = KnoWhy.Current.languagePrevious;
            this.lastNode = KnoWhy.Current.lastNode;
        }
    }
}
