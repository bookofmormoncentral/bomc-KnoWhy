using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;
using System.Net;
using Realms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace KnoWhy
{
    public class KnoWhy
    {
        public static int SORT_BY_DATE = 1;
        public static int SORT_BY_CHAPTER = 2;
        public static int SORT_ASC = 3;
        public static int SORT_DESC = 4;
        public static int FILTER_BY_FAVORITES = 5;
        public static int FILTER_BY_UNREADED = 6;
        public static int FILTER_BY_BOOK_AND_CHAPTER = 7;
        public static int FILTER_NONE = 8;
        public static int SHOW_SETTINGS = 9;
        public static int SHOW_SORT_FILTER = 10;

        public static int BOOK_CHAPTER_PANEL_CLOSED = 11;
        public static int BOOK_CHAPTER_PANEL_BOOK = 12;
        public static int BOOK_CHAPTER_PANEL_CHAPTER = 13;

        static KnoWhy _current = null;
        public static KnoWhy Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new KnoWhy();
                }
                return _current;
            }
        }
        CultureInfo culture = null;
        string _locale = "en";
        public string locale {
            get
            {
                return _locale;
            }
            set
            {
                _locale = value;
                translateConstants();
                KNOWHY_WEB_URL = "https://knowhy.bookofmormoncentral.org";
                dateLongFormat = "MMMM dd, yyyy";
                culture = new CultureInfo("en-US");
                if (_locale.StartsWith("es", StringComparison.OrdinalIgnoreCase) == true)
                {
                    KNOWHY_WEB_URL = "https://bookofmormoncentral." + _locale.Substring(0,2);
                    dateLongFormat = "dd 'de' MMMM 'de' yyyy";
                    culture = new CultureInfo("es-ES");
                }
                loadBooks();
            }
        }
        public string type { get; set; }
        public string dateLongFormat { get; set; }
        public string KNOWHY_WEB_URL { get; set; }

        public static string rootNode = "articles";
        public static string contentNode = "content";
        public static string metaNode = "meta";
        public static string indicesNode = "indices";
        public static string slugsNode = "slugs";

        public static string firebaseURL = "https://newbookofmormoncentral.firebaseio.com";
        public static string firebaseApiKey = "AIzaSyD4GMSCERi83HZJ0OLIIOp2BUabgDEeQ68";
        public static string firebaseApplicationId = "434454143117";

        public static string VERSION = "2.2.4";
        public static string BUILD_IOS = "2017120701";
        public static string BUILD_WINDOWS = "2017120701";
        public static string BUILD_ANDROID = "2017120702";

        public string CONSTANT_PUBLISH_DATE = "Publish Date";
        public string CONSTANT_CHAPTER_VERSE = "Chapter and Verse";
        public string CONSTANT_ASCEND_C = "Ascending";
        public string CONSTANT_DESCEND_C = "Descending";
        public string CONSTANT_ASCEND = "ascending";
        public string CONSTANT_DESCEND = "descending";
        public string CONSTANT_FAVORITES = "Favorites";
        public string CONSTANT_UNREAD = "Unread";
        public string CONSTANT_BOOK_CHAPTER = "Book & Chapter";
		public string CONSTANT_SORTED_BY = "Sorted by:";
		public string CONSTANT_FILTER_BY = "Filter by:";
		public string CONSTANT_SORT = "SORT:";
		public string CONSTANT_FILTER = "FILTER:";
        public string CONSTANT_SORT_AND_FILTER = "Sort and Filter";
        public string CONSTANT_DONE = "Done";

        public string CONSTANT_UPDATE_CONTENT = "Refresh Content";
        public string CONSTANT_UPDATE_DATE = "Last updated";
        public string CONSTANT_UPDATING = "Updating...";
        public string CONSTANT_SETTINGS = "Settings";
        public string CONSTANT_WEB_LINKS = "Web Links";
        public string CONSTANT_LINK1 = "https://bookofmormoncentral.org";
        public string CONSTANT_LINK2 = "https://bookofmormoncentral.org/lesson-personal-study";
        public string CONSTANT_LINK3 = "https://bookofmormoncentral.org/about";
        public string CONSTANT_LINK1_DESC = "Book of Mormon Central";
        public string CONSTANT_LINK2_DESC = "Gospel Doctrine Resources";
        public string CONSTANT_LINK3_DESC = "About Book of Mormon Central";
        public string CONSTANT_SETTINGS_TITLE = "Settings";
        public string CONSTANT_AUTOMATIC_UPDATE = "Auto-update content on WiFi only";
        public string CONSTANT_VERSION = "App Version";
        public string CONSTANT_BUILD = "Build";
        public string CONSTANT_RESET_CONTENT = "Reset Content";
        public string CONSTANT_RESET_CONTENT_LABEL = "Download the latest version of articles but keep user data.";
        public string CONSTANT_RESET_DATABASE = "Reset Database";
        public string CONSTANT_RESET_DATABASE_LABEL = "Reset all user data and download latest version of articles.";
        public string CONSTANT_ALERT_RESET1_1 = "Reset content?";
        public string CONSTANT_ALERT_RESET1_2 = "This will delete downloaded content. User data will be saved.";
        public string CONSTANT_ALERT_RESET1_3 = "Cancel";
        public string CONSTANT_ALERT_RESET1_4 = "Reset Content";
        public string CONSTANT_ALERT_RESET2_1 = "Reset database?";
        public string CONSTANT_ALERT_RESET2_2 = "This will delete all user data  and downloaded content.";
        public string CONSTANT_ALERT_RESET2_3 = "Cancel";
        public string CONSTANT_ALERT_RESET2_4 = "Reset Database";

        public string CONSTANT_ALL_BOOKS = "All";
        public string CONSTANT_ALL_CHAPTERS = "All Chapters";

        public string CONSTANT_SELECT_BOOK = "Select Book";
        public string CONSTANT_SELECT_CHAPTER = "Select Chapter";

        public string CONSTANT_BOOK = "Book";
        public string CONSTANT_CHAPTER = "Chapter";

        public string CONSTANT_LOADING = "Loading...";

        public string CONSTANT_CONNECTION_FAILED = "There is not internet connection, try again";

        public string CONSTANT_ERROR_TITLE = "Error";

        public string CONSTANT_EMPTY_LIST = "No articles match filter criteria.";

        public int sortType { get; set; }
        public int sortMode { get; set; }
        public bool filterEnabled { get; set; }
        public bool filterFavorites { get; set; }
        public bool filterUnreaded { get; set; }
        public bool filterBookAndChapter { get; set; }
        public int filterBookId { get; set; }
        public int filterChapterId { get; set; }
        public string languagePrevious { get; set; }
        public int lastNode { get; set; }

        public int bookChapterPanelStatus { get; set; }

        public long lastUpdate { get; set; }
        public bool onlyWiFi { get; set; }

        private Realm realm = null;
        RealmConfiguration config = null;

        public Settings settings = null;

        public ListInterface listInterface { get; set; }
        public DetailInterface detailInterface { get; set; }
		SortAndFilterInterface _sortAndFilterInterface = null;
		public SortAndFilterInterface SortAndFilterInterface
		{
			get
			{
				return _sortAndFilterInterface;
			}
			set
			{
				_sortAndFilterInterface = value;
                if (_sortAndFilterInterface != null)
                {
                    _sortAndFilterInterface.reloadData();
                }
			}
		}

        public ObservableCollection<Meta> metaList = new ObservableCollection<Meta>();
        public List<Meta> allMetaList = new List<Meta>();
        public List<Slug> slugsList = new List<Slug>();

        public List<Books> booksList = new List<Books>();

        public KnoWhy()
        {
            locale = "en";
            type = "published";

            sortType = KnoWhy.SORT_BY_DATE;
            sortMode = KnoWhy.SORT_DESC;
            filterEnabled = false;
            filterFavorites = false;
            filterUnreaded = false;
            filterBookAndChapter = false;
            filterBookId = 0;
            filterChapterId = 0;
            onlyWiFi = false;
            languagePrevious = locale;
            lastUpdate = 0;
            lastNode = 0;

            metaList = new ObservableCollection<Meta>();
            allMetaList = new List<Meta>();
            slugsList = new List<Slug>();


        }

        public void loadBooks() {
            int i = 0;
            booksList.Clear();
            while (i <= 16) {
                booksList.Add(new Books(i, locale));
                i++;
            }
        }

        public async Task init(ListInterface _interface) {
            listInterface = _interface;
            listInterface.showProgress();
            listInterface.setup();
            listInterface.updateFilter();
            importOldData();
            settings = Settings.getSettings(true);
            bookChapterPanelStatus = KnoWhy.BOOK_CHAPTER_PANEL_CLOSED;
            if (locale == languagePrevious)
            {
                await loadData(false);
            } else {
                languagePrevious = locale;
                await loadData(true);
            }

            //await listInterface.loadData();
            
            listInterface.hideProgress();
        }

        public Realm getDatabase() {
            if (realm == null)
            {
                realm = Realm.GetInstance(getRealmConfiguration());
            }
            return realm;
        }

        public RealmConfiguration getRealmConfiguration()
        {
            if (config == null)
            {
                Console.WriteLine("Realm config");
                config = new RealmConfiguration
                {
                    SchemaVersion = 7,
                    MigrationCallback = (migration, oldSchemaVersion) =>
                    {
                        try
                        {
                            Console.WriteLine("Migration");
                            //var newPeople = migration.NewRealm.All<Person>();
                            var oldPeople = migration.OldRealm.All("Cache");

                            for (var i = 0; i < oldPeople.Count(); i++)
                            {
                                var oldPerson = oldPeople.ElementAt(i);
                                //var newPerson = newPeople.ElementAt(i);

                                // Migrate Person from version 0 to 1: replace FirstName and LastName with FullName
                                if (oldSchemaVersion < 1)
                                {
                                    //newPerson.FullName = oldPerson.FirstName + " " + oldPerson.LastName;
                                }

                                // Migrate Person from version 1 to 2: replace Age with Birthday
                                if (oldSchemaVersion < 2)
                                {
                                    //newPerson.Birthday = DateTimeOffset.Now.AddYears(-(int)oldPerson.Age);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Realm Migration Error: " + e.Message);
                        }
                    }
                };
            }
            return config;
            //return null;
        }

        public async Task loadData(bool update)
        {
            if (listInterface.isConnected() == true && update)
            {
                emptyListTables();
            }
            await loadMeta();
            await loadSlugs();
            //writeData();
            reorder();
        }

        public async Task reset(bool database)
        {
            if (database == true)
            {
                try
                {
                    //new Task(() =>
                    //{
                        Realm _realm = getDatabase();
                        using (var trans = _realm.BeginWrite())
                        {
                            
                            _realm.RemoveAll();
                            trans.Commit();
                            
                        }
                    //}).Start();

                } catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            } else {
                emptyListTables();
            }
            await init(listInterface);
        }

        public void emptyListTables()
        {
            try
            {
                //new Task(() =>
                //{                
                Realm _realm = getDatabase();
                using (var trans = _realm.BeginWrite())
                {
                    //_realm.RemoveAll("Meta");
                    //_realm.RemoveAll("Article");
                    //_realm.RemoveAll("Slug");
                    _realm.RemoveAll("JsonObject");


                    trans.Commit();

                }
                //}).Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public string getMetaURL()
        {
            return rootNode + "/" + locale + "/" + type + "/" + metaNode;
        }

        public string getSlugsURL()
        {
            return rootNode + "/" + locale + "/" + type + "/" + indicesNode + "/" + slugsNode;
        }

        public string getNodeURL(int _node)
        {
            return rootNode + "/" + locale + "/" + type + "/" + contentNode + "/" + _node.ToString();
        }

        public async Task loadMeta()
        {
            try
            {
                Realm _realm = getDatabase();

                allMetaList.Clear();
                metaList.Clear();
                Console.WriteLine("Start load meta");
                JsonObject.fillSavedMeta();
                Console.WriteLine("End load meta");
                /*if (allMetaList.Count() == 0)
                {*/
                    //using (var trans = _realm.BeginWrite())
                    //{

                Console.WriteLine("Start load remote meta");

                        var firebase = new FirebaseClient(KnoWhy.firebaseURL);

                        var metas = await firebase
                          .Child(KnoWhy.Current.getMetaURL())
                            .OnceSingleAsync<JArray>();
                Console.WriteLine("End load remote meta");
                        //if (metas is JArray)
                        //{
                            if (metas.Count() == allMetaList.Count) {
                                return;
                            }
                Console.WriteLine("Start process remote meta");
                            foreach (JToken meta in metas)
                            {
                                try
                                {
                                    //Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
                                    if (meta.Value<JObject>() != null)
                                    {
                                        KnoWhy.Current.addMeta(meta.Value<JObject>());
                                    }
                                } catch (Exception e) {
                                    Console.WriteLine("Exception: " + e.Message);
                                }
                            }
                Console.WriteLine("End process remote meta");
                        /*} else {
                            var metas2 = await firebase
                              .Child(KnoWhy.Current.getMetaURL())
                              .OnceSingleAsync<List<JObject>>();
                            foreach (JObject meta in metas2)
                            {
                                try
                                {
                                    //Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
                                    if (meta != null)
                                    {
                                        KnoWhy.Current.addMeta(meta);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: " + e.Message);
                                }
                            }
                        }*/

                        JsonObject.saveMeta();
                    //Settings.saveChanges();
                    using (var trans = _realm.BeginWrite())
                    {
                        lastUpdate = DateTime.Today.Ticks;
                        Settings settings_ = Settings.getSettings(false);
                        settings_.save();
                        trans.Commit();
                    }

                        //KnoWhy.Current.sort();

                        //trans.Commit();

                    //}
                /*}/* else
                {
                    foreach (Meta meta in existing)
                    {
                        allMetaList.Add(meta);
                        try
                        {
                            NodePersistance.nodeIsReaded(meta);
                            NodePersistance.nodeIsFavorite(meta);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }*/
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }

            return;
        }

        public async Task loadSlugs()
        {
            try
            {
                slugsList.Clear();

                Realm _realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());

                JsonObject.fillSavedSlug();

                //if (slugsList.Count() == 0)
                //{

                    //using (var trans = _realm.BeginWrite())
                    //{



                        var firebase = new FirebaseClient(KnoWhy.firebaseURL);

                        var slugs = await firebase
                          .Child(KnoWhy.Current.getSlugsURL())
                          .OnceSingleAsync<JObject>();

                        foreach (JToken slugData in slugs.Children())
                        {
                            //Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
                            if (slugData != null)
                            {
                                try
                                {
                                    Slug slug = new Slug();

                                    slug.key = slugData.Path;
                                    slug.nodeId = slugData.First.Value<Int32>();
                                    if (allowInsertSlug(slug.nodeId))
                                    {
                                        slugsList.Add(slug);
                                    }
                                }
                                catch (Exception ex1)
                                {
                                    Console.WriteLine("Error: " + ex1.Message);
                                }
                            }
                        }
                        //KnoWhy.Current.sort();
                        JsonObject.saveSlugs();
                        //trans.Commit();
                    //}
                /*}/* else
                {
                    foreach (Slug slug in existing)
                    {
                        slugsList.Add(slug);
                    }
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return;
        }

        private void importOldData() {
            try {
                Console.WriteLine("Cache");
                Realm _realm = getDatabase();
                using (var trans = _realm.BeginWrite())
                {
                    var cachePrevious = _realm.All<Cache>();
                    foreach (Cache cache in cachePrevious)
                    {
                        Console.WriteLine("Cache item ");
                        //Console.WriteLine("Cache value " + cache.value);
                        if (cache.value != "")
                        {
                            JObject json = JObject.Parse(cache.value);
                            if (cache.key == "MetaStore")
                            {
                                JToken meta = json.GetValue("meta");
                                JArray array = meta.Value<JArray>();
                                try
                                {
                                    foreach (JObject metaItem in array)
                                    {
                                        Console.WriteLine("Meta item ");
                                        JToken nodeIdToken = metaItem.GetValue("nodeId");
                                        int nodeId = nodeIdToken.Value<int>();
                                        Console.WriteLine("Node Id " + nodeId);
                                        JToken isFavorite = metaItem.GetValue("isFavorite");
                                        if (isFavorite.Value<bool>() == true)
                                        {
                                            Console.WriteLine("Is Favorite");
                                            var obj = new NodePersistance
                                            {
                                                nodeId = nodeId,
                                                mode = NodePersistance.FAVORITE
                                            };
                                            _realm.Write(() => _realm.Add(obj));
                                        }
                                        JToken isRead = metaItem.GetValue("isRead");
                                        if (isRead.Value<bool>() == true)
                                        {
                                            Console.WriteLine("Is Read");
                                            var obj = new NodePersistance
                                            {
                                                nodeId = nodeId,
                                                mode = NodePersistance.READED
                                            };
                                            _realm.Write(() => _realm.Add(obj));
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error reading import meta:" + e.Message);
                                }
                            }
                            else if (cache.key == "Settings")
                            {
                                JToken wifi = json.GetValue("updateWifiOnly");
                                if (wifi.Value<bool>() == true)
                                {
                                    Console.WriteLine("Is WiFi");
                                    onlyWiFi = true;
                                }
                                else
                                {
                                    Console.WriteLine("Is Not WiFi");
                                    onlyWiFi = false;
                                }
                            }
                            else if (cache.key == "UIState")
                            {
                                JToken filters = json.GetValue("filtersObject");
                                JObject isFavoriteToken = filters.Value<JObject>("isFavorite");
                                if (isFavoriteToken != null)
                                {
                                    JToken isFavorite = isFavoriteToken.GetValue("active");
                                    if (isFavorite.Value<bool>() == true)
                                    {
                                        Console.WriteLine("Is Favorite");
                                        filterFavorites = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Is Not Favorite");
                                        filterFavorites = false;
                                    }
                                }
                                JObject isReadToken = filters.Value<JObject>("isRead");
                                if (isReadToken != null)
                                {
                                    JToken isRead = isReadToken.GetValue("active");
                                    if (isRead.Value<bool>() == true)
                                    {
                                        Console.WriteLine("Is Read");
                                        filterUnreaded = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Is Not Read");
                                        filterUnreaded = false;
                                    }
                                }
                                JObject chapterToken = filters.Value<JObject>("chapter");
                                if (chapterToken != null)
                                {
                                    JToken chapterV = chapterToken.GetValue("active");
                                    if (chapterV.Value<bool>() == true)
                                    {
                                        Console.WriteLine("Is Chapter");
                                        filterBookAndChapter = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Is Not Chapter");
                                        filterBookAndChapter = false;
                                    }
                                }
                                JToken sortToken = json.GetValue("sort");
                                string sortByV = sortToken.Value<string>("key");
                                if (sortByV == "chapter")
                                {
                                    sortType = KnoWhy.SORT_BY_CHAPTER;
                                }
                                else
                                {
                                    sortType = KnoWhy.SORT_BY_DATE;
                                }
                                string sortModeV = sortToken.Value<string>("order");
                                if (sortModeV == "ascending")
                                {
                                    sortMode = KnoWhy.SORT_ASC;
                                }
                                else
                                {
                                    sortMode = KnoWhy.SORT_DESC;
                                }
                                Console.WriteLine("Import book");
                                int book = json.Value<int>("bookFilterIndex");
                                Console.WriteLine(book);
                                filterBookId = book;
                                Console.WriteLine("Import Chapter");
                                int chapter = json.Value<int>("chapterNumber");
                                Console.WriteLine(chapter);
                                filterChapterId = chapter;
                                bool filtersEnabledValue = json.Value<bool>("filtersActive");
                                Console.WriteLine(filterEnabled);
                                filterEnabled = filtersEnabledValue;
                            }
                        }
                    }

                    _realm.RemoveAll("Cache");
                    trans.Commit();
                }
      
            } catch (Exception e) {
                Console.WriteLine("Import error: " + e.Message);
            }
        }

        public async Task<Article> loadNode(Meta meta, int width, int height)
        {
            try
            {
                Article previousArticle = JsonObject.getArticle(meta.nodeId);
                if (previousArticle == null)
                {
                    var firebase = new FirebaseClient(KnoWhy.firebaseURL);

                    var node = await firebase
                      .Child(KnoWhy.Current.getNodeURL(meta.nodeId))
                      .OnceSingleAsync<JObject>();

                    if (node != null)
                    {
                        Article article = new Article();

                        JToken tokenNodeId = node.GetValue("nodeId");
                        JToken tokenFullHtml = node.GetValue("fullHtml");
                        JToken tokenPdfUrl = node.GetValue("pdfUrl");
                        JToken tokenScriptureQuote = node.GetValue("scriptureQuote");
                        JToken tokenSoundcloudUrl = node.GetValue("soundcloudUrl");
                        JToken tokenSummary = node.GetValue("summary");
                        JToken tokenTimestampUpdated = node.GetValue("timestampUpdated");
                        JToken tokenVimeoUrl = node.GetValue("vimeoUrl");
                        JToken tokenYoutubeUrl = node.GetValue("youtubeUrl");

                        if (tokenNodeId != null)
                        {
                            article.nodeId = tokenNodeId.Value<Int32>();
                        }
                        if (tokenFullHtml != null)
                        {
                            article.fullHtml = tokenFullHtml.Value<string>();
                        }
                        if (tokenPdfUrl != null)
                        {
                            article.pdfUrl = tokenPdfUrl.Value<string>();
                        }
                        if (tokenScriptureQuote != null)
                        {
                            article.scriptureQuote = tokenScriptureQuote.Value<string>();
                        }
                        if (tokenSoundcloudUrl != null)
                        {
                            article.soundcloudUrl = tokenSoundcloudUrl.Value<string>();
                        }
                        if (tokenSummary != null)
                        {
                            article.summary = tokenSummary.Value<string>();
                        }
                        if (tokenTimestampUpdated != null)
                        {
                            article.timestampUpdated = tokenTimestampUpdated.Value<long>();
                        }
                        article.vimeoUrl = "";
                        if (tokenVimeoUrl != null)
                        {
                            article.vimeoUrl = tokenVimeoUrl.Value<string>();
                        }
                        if (article.vimeoUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase) != true)
                        {
                            article.vimeoUrl = "";
                        }
                        article.youtubeUrl = "";
                        if (tokenYoutubeUrl != null)
                        {
                            article.youtubeUrl = tokenYoutubeUrl.Value<string>();
                        }
                        if (article.youtubeUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase) != true)
                        {
                            article.youtubeUrl = "";
                        }

                        /*if (article.timestampUpdated > 0) {
                            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                            dtDateTime = dtDateTime.AddSeconds(article.timestampUpdated / 1000).ToLocalTime();
                            Console.WriteLine(dtDateTime.ToShortDateString());
                        }*/

                        string callInit = "initPage(0);";
                        string favoriteText = "&#xf100;";
                        string noFavoriteText = "&#xf100;";
                        string defaultStyle = "nofavorite";
                        string buttonFavoriteText = noFavoriteText;
                        string isFavorite = "\"0\"";
                        if (meta.isFavorite == true)
                        {
                            isFavorite = "\"1\"";
                            buttonFavoriteText = favoriteText;
                            defaultStyle = "favorite";
                            callInit = "initPage(1);";
                        }

                        
                        string styles = "styles.css";

                        string imageUrl = meta.mainImageURL;
                        string imageWidthAttribute = " width=\"" + meta.mainImageWidth + "px\"";
                        string imageHeightAttribute = " height=\"" + meta.mainImageHeight + "px\"";
                        Image mainImage = null;

                        int viewWidth = width;
                        if (height < width)
                        {
                            viewWidth = height;
                        }
                        if (viewWidth > 0)
                        {
                            if (viewWidth > 500)
                            {
                                viewWidth = 500;
                            }
                            mainImage = meta.getImage(viewWidth, 0);
                        }
                        else
                        {
                            viewWidth = 500;
                            mainImage = meta.getImage(500, 0);
                        }
                        if (mainImage != null)
                        {
                            float widthImage = width;
                            float heightImage = (viewWidth * mainImage.height) / mainImage.width;

                            imageUrl = mainImage.url;
                            imageWidthAttribute = " width: " + Math.Truncate(widthImage).ToString() + "px;";
                            imageHeightAttribute = " height: " + Math.Truncate(heightImage).ToString() + "px;";
                        }

                        float aspectRatio = 16 / 9;
                        int videoWidth = viewWidth - 60;
                        int videoHeight = 250;
                        float videoRatio = videoWidth / videoHeight;

                        if (aspectRatio > videoRatio)
                        {
                            // too wide, shrink height
                            videoHeight = (int)(videoWidth / aspectRatio);
                        }
                        else if (aspectRatio < videoRatio)
                        {
                            // too tall, shrink width
                            videoWidth = (int)(videoHeight * aspectRatio);
                        }

                        string htmlString = "";

                        JArray htmlArray = (JArray)JsonConvert.DeserializeObject(article.fullHtml);

                        foreach (JObject htmlObject in htmlArray)
                        {
                            htmlString = htmlString + "<div class=\"block\">" + getHTMLString(htmlObject, viewWidth) + "</div>";
                        }

                        //string head = "<head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />" + styles + "</head>";
                        string head = "<head><link rel=\"stylesheet\" type=\"text/css\" href=\"" + styles + "\"><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /></head>";
                        string image = "<div class=\"container\"><img class=\"headerImage\" src=\"" + imageUrl + "\" styles=\"" + imageWidthAttribute + imageHeightAttribute + "\" /><div class=\"content\">";
                        string line1 = "<div><div class=\"favoriteContainer\"><button id=\"favoriteButton\" class=\"" + defaultStyle + "\" onclick=\"toggleFavorite()\" /></div><p class=\"subtitle date\">" + meta.longDate + "</p></div>";
                        string line2 = "<p class=\"title\">" + meta.title + "</p>";
                        string line3 = "<p class=\"subtitle quote\">" + article.scriptureQuote + "</p>";
                        string line4 = "<p class=\"subtitle\">" + meta.scriptureReference + "</p><div class=\"fullText\">";
                        string videoUrl = "";
                        if (article.youtubeUrl != null && article.youtubeUrl != "")
                        {
                            string youtubeId = article.youtubeUrl.Substring(article.youtubeUrl.LastIndexOf('/') + 1);
                            videoUrl = "https://www.youtube.com/embed/" + youtubeId;
                        }
                        else if (article.vimeoUrl != null && article.vimeoUrl != "")
                        {
                            string[] array = article.vimeoUrl.Split('/');
                            //string vimeoId = article.vimeoUrl.Substring(article.vimeoUrl.LastIndexOf('/', article.vimeoUrl.LastIndexOf('/') - 1) + 1, article.vimeoUrl.LastIndexOf('/'));
                            string vimeoId = "";
                            if (array.Length > 1)
                            {
                                vimeoId = array[array.Length - 2];
                            }
                            videoUrl = "https://player.vimeo.com/video/" + vimeoId;
                        }
                        string video = "";
                        string initFunction = "";
                        if (videoUrl != null && videoUrl != "")
                        {
                            video = "<div align=\"middle\" height=\"" + videoHeight + "\" width=\"" + videoWidth + "\" class=\"videoContainer\"><iframe id=\"video\" class=\"video\" height=\"" + videoHeight + "\" width=\"" + videoWidth + "\" frameborder=\"0\" src=\"\" allowfullscreen></iframe></div>";
                            //video = "<div class=\"videoContainer\" height=\"" + videoHeight + "\" width=\"" + videoWidth + "\"><video height=\"" + videoHeight + "\" width=\"" + videoWidth + "\" src=\"" + videoUrl + "\" allowfullscreen></video></div>";
                            initFunction = "function initPage(favorite) { document.getElementById(\"video\").src = \"" + videoUrl + "\";if (favorite != val) { toggleFavorite();}}";
                        }
                        string javaScript = "<script>var val = " + isFavorite + "; function toggleFavorite () { if (val == \"1\") { val = \"0\"; document.getElementById(\"favoriteButton\").className = \"nofavorite\" } else { val = \"1\"; document.getElementById(\"favoriteButton\").className = \"favorite\" } try { window.webkit.messageHandlers.toggleFavorite.postMessage(val); } catch(err) { console.log('The native context does not exist yet'); } try { Android.toggleFavorite(val); } catch(err) { console.log('The native context does not exist yet'); } try { window.external.notify(val); } catch(err) { console.log('The native context does not exist yet'); } } " + initFunction + "</script>";
                        article.parsedHTML = "<!DOCTYPE html><html>" + head + "<body onLoad=\"" + callInit + " \">" + image + line1 + line2 + line3 + line4 + video + "<div>" + htmlString + "</div></div></div></div>" + javaScript + "</body></html>";


                        try
                        {
                            //meta.article = article;
                            NodePersistance.saveNodeAsReaded(meta);

                            JsonObject.saveMeta();

                            JsonObject.saveArticle(article);

                            lastNode = meta.nodeId;
                            Settings.saveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        return article;
                    }
                } else
                {
                    string callInit = "initPage(0);";
                    string callInitInv = "initPage(1);";
                    if (meta.isFavorite == true)
                    {
                        callInit = "initPage(1);";
                        callInitInv = "initPage(0);";
                    }
                    string parsedHTML = previousArticle.parsedHTML;
                    if (parsedHTML.IndexOf(callInit, StringComparison.OrdinalIgnoreCase) < 0) {
                        parsedHTML = parsedHTML.Replace(callInitInv, callInit);
                        previousArticle.parsedHTML = parsedHTML;
                        NodePersistance.saveNodeAsReaded(meta);
                        JsonObject.saveArticle(previousArticle);  
                    }
                    NodePersistance.saveNodeAsReaded(meta);
                    Settings.saveChanges();
                    lastNode = meta.nodeId;
                    return previousArticle;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public Meta getFirstMeta() {
            if (metaList.Count > 0)
            {
                if (lastNode == 0)
                {
                    return metaList[0];
                } else {
                    foreach (Meta meta in metaList) {
                        if (meta.nodeId == lastNode) {
                            return meta;
                        }
                    }
                }
            }
            return null;
        }

        public int getFirstMetaIndex()
        {
            if (metaList.Count > 0)
            {
                if (lastNode == 0)
                {
                    return 0;
                }
                else
                {
                    int i = 0;
                    foreach (Meta meta in metaList)
                    {
                        if (meta.nodeId == lastNode)
                        {
                            return i;
                        }
                        i++;
                    }
                }
            }
            return 0;
        }

        private string getHTMLString(JObject htmlObject, int viewWidth)
        {
            string htmlString = "";

            JToken tokenId = htmlObject.GetValue("id");
            JToken tokenType = htmlObject.GetValue("type");
            JToken tokenAttributes = htmlObject.GetValue("attributes");
            JToken tokenChildNodes = htmlObject.GetValue("childNodes");
            JToken tokenValue = htmlObject.GetValue("value");

            string idTag = "";
            string typeTag = "";
            string attributesTag = "";
            string valueTag = "";

            string href = "";
            string title = "";

            if (tokenId != null)
            {
                idTag = tokenId.Value<string>();
            }
            if (tokenType != null)
            {
                typeTag = tokenType.Value<string>();
            }
            string imageWidth = "";
            string imageHeight = "";
            string imageSource = "";
            bool addedId = false;
            bool addLine = false;
            if (tokenAttributes != null)
            {
                JObject attributes = tokenAttributes.Value<JObject>();
                if (attributes != null)
                {
                    foreach (JProperty property in attributes.Properties())
                    {
                        if (property.Name != "length" && property.Value.ToString() != "")
                        {
                            bool addAttribute = true;
                            string value = property.Value.ToString();
                            if (property.Name == "style")
                            {
                                //value = correctStyleValue(value);
                            }
                            if (property.Name == "href")
                            {
                                href = KNOWHY_WEB_URL + value;
                            }
                            if (property.Name == "title")
                            {
                                title = value;
                            }
                            if (typeTag == "img" && property.Name == "src")
                            {
                                addAttribute = false;
                                value = KNOWHY_WEB_URL + value;
                                imageSource = value;
                            } else if (typeTag == "img" && property.Name == "class") 
                            {
                                addAttribute = false;
                            } else if (typeTag == "img" && property.Name == "width")
                            {
                                addAttribute = false;
                                imageWidth = value;
                            }
                            else if (typeTag == "img" && property.Name == "height")
                            {
                                addAttribute = false;
                                imageHeight = value;
                            }
                            if (typeTag == "ul" && property.Name == "class") {
                                if (value == "footnotes")
                                {
                                    addLine = true;
                                    addAttribute = false;
                                }
                            }
                            if (typeTag == "iframe" && property.Name == "src")
                            {
                                if (value.Contains("youtu") == true)
                                {
                                    return "";
                                }
                                else
                                {
                                    if (value.Contains("?") == true)
                                    {
                                        value = value + "&" + "playsinline=0";
                                    }
                                    else
                                    {
                                        value = value + "?" + "playsinline=0";
                                    }
                                }
                            }
                            else if (typeTag == "iframe" && property.Name == "align")
                            {
                                addAttribute = false;
                            }
                            else if (typeTag == "iframe" && property.Name == "width")
                            {
                                addAttribute = false;
                            }
                            else if (typeTag == "iframe" && property.Name == "height")
                            {
                                addAttribute = false;
                            }
                            if (value.StartsWith("/sites", StringComparison.Ordinal) && property.Name == "href")
                            {
                                value = KNOWHY_WEB_URL + value;
                            }
                            if (property.Name =="id")
                            {
                                addedId = true;
                            }
                            if (addAttribute == true)
                            {
                                attributesTag = attributesTag + " " + property.Name + "=\"" + value + "\"";
                            }
                        }
                    }
                }
            }
            if (idTag != "" && addedId == false)
            {
                attributesTag = attributesTag + " id=\"" + idTag + "\"";
            }
            if (tokenValue != null)
            {
                valueTag = tokenValue.Value<string>();
            }
            if (typeTag == "iframe")
            {
                float aspectRatio = 16 / 9;
                int videoWidth = viewWidth;
                int videoHeight = 250;
                float videoRatio = videoWidth / videoHeight;

                if (aspectRatio > videoRatio)
                {
                    // too wide, shrink height
                    videoHeight = (int)(videoWidth / aspectRatio);
                }
                else if (aspectRatio < videoRatio)
                {
                    // too tall, shrink width
                    videoWidth = (int)(videoHeight * aspectRatio);
                }

                htmlString = htmlString + "<div align=\"middle\" class=\"videoContainer\" height=\"" + videoHeight + "\" width=\"" + videoWidth + "\" ><iframe " + attributesTag + "></iframe></div>";
                //video = "<div class=\"videoContainer\" height=\"" + videoHeight + "\" width=\"" + videoWidth + "\"><video height=\"" + videoHeight + "\" width=\"" + videoWidth + "\" src=\"" + videoUrl + "\" allowfullscreen></video></div>";
            }
            else if (typeTag == "img")
            {
                /*string image = imageSource;
                byte[] imageData = new WebClient().DownloadData(image);
                MemoryStream imgStream = new MemoryStream(imageData);
                Image img = Image.FromStream(imgStream);

                int wSize = img.Width;
                int hSize = img.Height;

                if (imageWidth > 0 && imageHeight > 0)
                {
                    float origR = imageWidth / imageHeight;

                    int width = viewWidth;
                    int height = 250;
                    float ratio = width / height;

                    if (origR > ratio)
                    {
                        // too wide, shrink height
                        height = (int)(width / origR);
                    }
                    else if (origR < ratio)
                    {
                        // too tall, shrink width
                        width = (int)(height * origR);
                    }
                }*/
                htmlString = htmlString + "<div class=\"block\">";
                htmlString = htmlString + "<div align=\"middle\">";
                htmlString = htmlString + "<img height=\"" + imageHeight + "\" width=\"" + imageWidth + "\" class=\"media\" src=\"" + imageSource + "\" />";
                htmlString = htmlString + "</div>";
                htmlString = htmlString + "</div>";
            }
            else if (typeTag == "a-img")
            {
                string[] imageParams = getImageParams(tokenChildNodes);
                string width = imageParams[0];
                string height = imageParams[1];
                string src = imageParams[2];

                //htmlString = htmlString + "<img class=\"media\"" + width + height + " src=\"" + href + "\" />";
                //htmlString = htmlString + "<p class=\"footer\">" + title + "</p>";
                htmlString = htmlString + "<div class=\"block\">";
                htmlString = htmlString + "<div align=\"middle\" class=\"footer\">";
                htmlString = htmlString + "<a href=\"" + href + "\"><img height=\"" + height + "\" width=\"" + width + "\" class=\"media\" src=\"" + src + "\" /></a>";
                htmlString = htmlString + "<br>" + title;
                htmlString = htmlString + "</div>";
                htmlString = htmlString + "</div>";
            }
            else if (typeTag == "div-img")
            {
                string[] imageParams = getImageParams(tokenChildNodes);
                string width = imageParams[0];
                string height = imageParams[1];
                string src = imageParams[2];

                //htmlString = htmlString + "<img class=\"media\"" + width + height + " src=\"" + href + "\" />";
                //htmlString = htmlString + "<p class=\"footer\">" + title + "</p>";
                htmlString = htmlString + "<div class=\"block\">";
                htmlString = htmlString + "<div align=\"middle\">";
                htmlString = htmlString + "<img class=\"media\" height=\"" + height + "\" width=\"" + width + "\" src=\"" + src + "\" />";
                htmlString = htmlString + "<p class=\"footer\">" + title + "</p>";
                htmlString = htmlString + "</div>";
                htmlString = htmlString + "</div>";
                /*} else if (typeTag == "blockquote") {
                    */
            }
            else if (typeTag == "ul")
            {
                htmlString = htmlString + "<div>";
                if (addLine == true)
                {
                    htmlString = htmlString + "<div><div class=\"footnoteLine\"/></div>";
                }
                htmlString = htmlString + "<" + typeTag + attributesTag + ">";
            } else if (typeTag == "table") {
                htmlString = htmlString + "<div class=\"tableWrapper\" width=\"400px\">";
                htmlString = htmlString + "<" + typeTag + attributesTag + ">";
            } else if (typeTag == "text")
            {
                //htmlString = htmlString + valueTag + " ";
                htmlString = htmlString + valueTag;
            } else
            {
                htmlString = htmlString + "<" + typeTag + attributesTag + ">";
            }
            if (tokenChildNodes != null && typeTag != "a-img" && typeTag != "div-img")
            {
                JArray childNodes = tokenChildNodes.Value<JArray>();
                if (childNodes != null)
                {
                    foreach (JObject childObject in childNodes)
                    {
                        htmlString = htmlString + getHTMLString(childObject, viewWidth);
                    }
                }
            }
            if (typeTag != "text" && typeTag != "a-img" && typeTag != "div-img")
            {
                htmlString = htmlString + "</" + typeTag + ">";
                if (typeTag == "ul" || typeTag == "table")
                {
                    htmlString = htmlString + "</div>";
                }
            }
            return htmlString;
        }

        public string correctStyleValue(string style)
        {
            try
            {
                string result = style;
                result = result.Replace("{", string.Empty);
                result = result.Replace("}", string.Empty);
                result = result.Replace("\"", string.Empty);
                result = result.Replace(",", ";");
                char[] invalid = System.IO.Path.GetInvalidFileNameChars();
                result = new string(result.Select(c => c < ' ' ? '_' : c).ToArray());
                result = result.Replace("_", string.Empty);
                result = Regex.Replace(result, @"(?<!_)([A-Z])", "-$1");
                return result.ToLower();
            } catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return style;
            }
        }

        public void clearList()
        {
            allMetaList.Clear();
        }

        public string getCurrentChapterDesc()
        {
            string result = "";

            if (filterBookId == 0)
            {
                return CONSTANT_ALL_BOOKS;
            } else
            {
                Books book = booksList.ToArray()[filterBookId];
                result = book.name;
                if (filterChapterId > 0 && book.chapters > 1)
                {
                    result = result + " " + filterChapterId.ToString();
                }
            }


            return result;
        }

        public void moveToPreviousChapter()
        {
            if (filterBookId > 0)
            {
                if (filterChapterId > 1)
                {
                    filterChapterId = filterChapterId - 1;
                }
                else
                {
                    filterBookId = filterBookId - 1;
                    Books newBook = booksList.ToArray()[filterBookId];
                    filterChapterId = newBook.chapters;
                }
            }

            reorder();
        }

        public void moveToNextChapter()
        {
            Books book = booksList.ToArray()[filterBookId];
            if (filterChapterId < book.chapters)
            {
                filterChapterId = filterChapterId + 1;
            }
            else
            {
                filterBookId = filterBookId + 1;
                if (book.chapters > 0)
                {
                    filterChapterId = 1;
                }
                else
                {
                    filterChapterId = 0;
                }
            }

            reorder();
        }

        public void moveToPreviousBook()
        {
            if (filterBookId > 0)
            {
                filterBookId = filterBookId - 1;
                filterChapterId = 0;
            }

            reorder();
        }

        public void moveToNextBook()
        {
            if (filterBookId < booksList.Count)
            {
                filterBookId = filterBookId + 1;
                filterChapterId = 0;
            }

            reorder();
        }

        public void sort()
        {
            IEnumerable<Meta> query = null;
            Console.WriteLine("Sort");
            if (sortType == KnoWhy.SORT_BY_DATE)
            {
                if (sortMode == KnoWhy.SORT_DESC)
                {
                    query = allMetaList.OrderByDescending(x => x.timestampPosted);
                }
                else
                {
                    query = allMetaList.OrderBy(x => x.timestampPosted);
                }
            } else {
				if (sortMode == KnoWhy.SORT_DESC)
				{
                    query = allMetaList.OrderByDescending(x => x.book).ThenBy(x => x.chapter).ThenBy(x => x.verse);
				}
				else
				{
                    query = allMetaList.OrderBy(x => x.book).ThenBy(x => x.chapter).ThenBy(x => x.verse);
				}
            }
            IEnumerable<Meta> queryWhere = query;
            if (filterEnabled == true)
            {
                if (filterFavorites == true && filterUnreaded == false)
                {
                    query = query.Where(x => x.isFavorite == true);
                } else if (filterFavorites == false && filterUnreaded == true)
                {
                    query = query.Where(x => x.isRead == false);
                } else if (filterFavorites == true && filterUnreaded == true)
                {
                    query = query.Where(x => x.isFavorite == true || x.isRead == false);
                }
                if (filterBookAndChapter == true) {
                    if (filterBookId > 0 && filterChapterId > 0) {
                        query = query.Where(x => x.book == filterBookId && x.chapter == filterChapterId);
                    } else if (filterBookId > 0) {
                        query = query.Where(x => x.book == filterBookId);
                    }
                }
            }
            List<Meta> list = query.ToList();
            metaList.Clear();
            foreach (Meta item in list)
            {
                metaList.Add(item);
            }
        }

        public string[] getImageParams(JToken tokenChildNodes) {
            string width = "";
            string height = "";
            string src = "";
            JArray childNodes = tokenChildNodes.Value<JArray>();
            if (childNodes != null)
            {
                foreach (JObject childObject in childNodes)
                {
                    JToken tokenTypeC = childObject.GetValue("type");
                    JToken tokenAttributesC = childObject.GetValue("attributes");

                    string typeTagC = "";

                    if (tokenTypeC != null)
                    {
                        typeTagC = tokenTypeC.Value<string>();
                    }
                    if (typeTagC == "img")
                    {
                        if (tokenAttributesC != null)
                        {
                            JObject attributesC = tokenAttributesC.Value<JObject>();
                            if (attributesC != null)
                            {
                                foreach (JProperty propertyC in attributesC.Properties())
                                {
                                    if (propertyC.Name == "style")
                                    {
                                        foreach (JObject values in propertyC.Values<JObject>())
                                        {
                                            JToken tokenWidth = values.GetValue("width");
                                            JToken tokenHeight = values.GetValue("height");
                                            if (tokenWidth != null)
                                            {
                                                width = tokenWidth.Value<string>();
                                            }
                                            if (tokenHeight != null)
                                            {
                                                height = tokenHeight.Value<string>();
                                            }
                                        }

                                        //style = propertyC.Value.ToString();

                                    } else if (propertyC.Name == "src") {
                                        src = propertyC.Value.ToString();
                                        src = KNOWHY_WEB_URL + src;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*string fullWidth = "";
            string fullHeight = "";
            if (width != "")
            {
                fullWidth = " width=\"" + width + "\"";
            }*//* else
            {
                if (height != "")
                {
                    fullWidth = " width=\"" + height + "\"";
                }
            }*/
            /*if (height != "")
            {
                fullHeight = " height=\"" + height + "\"";
            }*//* else
            {
                if (width != "")
                {
                    fullHeight = " height=\"" + width + "\"";
                }
            }*/
            string[] result = new string[3];
            result[0] = width;
            result[1] = height;
            result[2] = src;
            return result;
        }

        public bool allowInsertMeta(int nodeId, long timeStamp) {
            if (allMetaList.Count > 0) {
                foreach (Meta item in allMetaList) {
                    if (nodeId == item.nodeId) {
                        if (timeStamp > item.timestampPosted) {
                            allMetaList.Remove(item);
                            return true;
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool allowInsertSlug(int nodeId)
        {
            if (slugsList.Count > 0)
            {
                foreach (Slug item in slugsList)
                {
                    if (nodeId == item.nodeId)
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public void addMeta(JObject item)
        {
            try
            {
                JToken tokenNodeId = item.GetValue("nodeId");
                JToken tokenTimestampPosted = item.GetValue("timestampPosted");

                int nodeId = tokenNodeId.Value<Int32>();
                long timeStamp = tokenTimestampPosted.Value<long>();

                if (allowInsertMeta(nodeId, timeStamp))
                {

                    Meta meta = new Meta();

                    JToken tokenKnowhyNumber = item.GetValue("knowhyNumber");

                    JToken tokenChapter = item.GetValue("chapter");
                    JToken tokenTitle = item.GetValue("title");
                    JToken tokenScriptureReference = item.GetValue("scriptureReference");
                    JToken tokenMainImage = item.GetValue("mainImage");

                    meta.nodeId = tokenNodeId.Value<Int32>();
                    if (tokenKnowhyNumber != null)
                    {
                        meta.knowhyNumber = tokenKnowhyNumber.Value<Int32>();
                    }
                    meta.timestampPosted = tokenTimestampPosted.Value<long>();
                    meta.title = tokenTitle.Value<string>();
                    if (tokenChapter != null)
                    {
                        meta.chapterString = tokenChapter.Value<string>();
                        string[] chapterArray = meta.chapterString.Split('.');
                        if (chapterArray.Length == 2)
                        {
                            int fullChapter = Int32.Parse(chapterArray[0]);
                            //meta.chapter = Int32.Parse(chapterArray[0]);
                            meta.verse = Int32.Parse(chapterArray[1]);
                            int[] bookAndChapter = Meta.getBookAndChapter(fullChapter);
                            meta.book = bookAndChapter[0];
                            meta.chapter = bookAndChapter[1];
                        }
                    }
                    meta.scriptureReference = tokenScriptureReference.Value<string>();

                    string mainImage = tokenMainImage.Value<string>();

                    var imageJson = JObject.Parse(mainImage);
                    try
                    {
                        var widths = imageJson.GetValue("sizes");
                        if (widths != null)
                        {
                            foreach (JProperty widthProperty in widths)
                            {
                                int width = int.Parse(widthProperty.Name);
                                //JToken val = token.First.Value<JObject>().First;
                                foreach (JToken heights in widthProperty.Values<JToken>())
                                {
                                    foreach (JProperty heightProperty in heights)
                                    {
                                        int height = int.Parse(heightProperty.Name);
                                        //JValue jValue = heightProperty.Value<JValue>();
                                        foreach (JValue jValue in heightProperty.Values<JValue>())
                                        {
                                            Image image = new Image();
                                            image.nodeId = meta.nodeId;
                                            image.url = jValue.Value<string>();
                                            image.width = width;
                                            image.height = height;
                                            meta.addImage(image);
                                        }

                                    }
                                }
                            }
                        }
                        Image main = meta.getImage(480, 240);
                        if (main != null)
                        {
                            meta.mainImageURL = main.url;
                            meta.mainImageWidth = main.width.ToString();
                            meta.mainImageHeight = main.height.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    try
                    {

                        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(meta.timestampPosted / 1000).ToLocalTime();
                        DateTime today = DateTime.Today;
                        DateTime lastWeek = DateTime.Now.AddDays(-7);
                        DateTime yesterday = DateTime.Now.AddDays(-1);
                        string numberDateString = "";
                        if (meta.knowhyNumber != 0)
                        {
                            numberDateString = meta.knowhyNumber.ToString();
                        }

                        if (today.Day == dtDateTime.Day && today.Month == dtDateTime.Month && today.Year == dtDateTime.Year)
                        {
                            if (locale.StartsWith("es", StringComparison.Ordinal) == true)
                            {
                                meta.formattedDate = numberDateString + " - hoy";
                            }
                            else
                            {
                                meta.formattedDate = numberDateString + " - today";
                            }
                        }
                        else if (yesterday.Day == dtDateTime.Day && yesterday.Month == dtDateTime.Month && yesterday.Year == dtDateTime.Year)
                        {
                            if (locale.StartsWith("es", StringComparison.Ordinal) == true)
                            {
                                meta.formattedDate = numberDateString + " - ayer";
                            }
                            else
                            {
                                meta.formattedDate = numberDateString + " - yesterday";
                            }
                        }
                        else if (lastWeek.Ticks < dtDateTime.Ticks)
                        {
                            meta.formattedDate = numberDateString + " - " + dtDateTime.ToString("dddd", culture);
                        }
                        else
                        {
                            meta.formattedDate = numberDateString + " - " + dtDateTime.ToString("d", culture);
                        }
                        meta.longDate = dtDateTime.ToString(dateLongFormat, culture);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    try
                    {
                        NodePersistance.nodeIsReaded(meta);
                        NodePersistance.nodeIsFavorite(meta);

                        //getDatabase().Add(meta);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    allMetaList.Add(meta);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void sortByDate()
        {
            sortType = KnoWhy.SORT_BY_DATE;
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateSortType(sortType);
        }

        public void sortByChapter()
        {
            sortType = KnoWhy.SORT_BY_CHAPTER;
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateSortType(sortType);
        }

        public void sortAsc()
        {
            sortMode = KnoWhy.SORT_ASC;
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateSortMode(sortMode);
        }

        public void sortDesc()
        {
            sortMode = KnoWhy.SORT_DESC;
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateSortMode(sortMode);
        }

        public void toggleFavorites()
        {
            if (filterFavorites == true)
            {
                filterFavorites = false;
                if (filterUnreaded == false && filterBookAndChapter == false && filterEnabled == true)
                {
                    filterEnabled = false;
                }
            } else
            {
                filterFavorites = true;
                filterEnabled = true;
            }
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateFilterFavorites(filterFavorites);
        }

        public void toggleUnreaded()
        {
            if (filterUnreaded == true)
            {
                filterUnreaded = false;
                if (filterFavorites == false && filterBookAndChapter == false && filterEnabled == true)
                {
                    filterEnabled = false;
                }
            }
            else
            {
                filterUnreaded = true;
                filterEnabled = true;
            }
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateFilterUnreaded(filterUnreaded);
        }

        public void toggleBookAndChapter()
        {
            if (filterBookAndChapter == true)
            {
                filterBookAndChapter = false;
                if (filterUnreaded == false && filterUnreaded == false && filterEnabled == true)
                {
                    filterEnabled = false;
                }
            }
            else
            {
                filterBookAndChapter = true;
                filterEnabled = true;
            }
			if (SortAndFilterInterface != null)
			{
				SortAndFilterInterface.reloadData();
			}
            //Settings.updateFilterBookAndChapter(filterBookAndChapter);
        }

        public void toggleButtonFilter()
        {
            if (filterEnabled == false)
            {
                filterEnabled = true;
                if (filterUnreaded == false && filterFavorites == false && filterBookAndChapter == false)
                {
                    listInterface.showSettings();
                }
                else
                {
                    reorder();
                }
            }
            else
            {
                filterEnabled = false;
                reorder();
            }
            Settings.saveChanges();
            //Settings.updateFilterEnabled(filterEnabled);
        }

        public void setOnlyWiFi(bool val)
        {
            onlyWiFi = val;
            //Settings.updateFilterBookAndChapter(filterBookAndChapter);
        }

        public void updateSettings() {
            Settings.saveChanges();
        }

        public string getSortDesc()
        {
            string result = "";
            if (sortType == KnoWhy.SORT_BY_DATE)
            {
                result = CONSTANT_PUBLISH_DATE;
            }
            else if (sortType == KnoWhy.SORT_BY_CHAPTER)
            {
                result = CONSTANT_CHAPTER_VERSE;
            }
            if (sortMode == KnoWhy.SORT_ASC)
            {
                result = result + " (" + CONSTANT_ASCEND + ")";
            }
            else if (sortMode == KnoWhy.SORT_DESC)
            {
                result = result + " (" + CONSTANT_DESCEND + ")";
            }
            return result;
        }

        public string getFilterDesc()
        {
            string result = "";
            if (filterFavorites == true)
            {
                if (result != "")
                {
                    result = result + ", ";
                }
                result = result + CONSTANT_FAVORITES;
            }
            if (filterUnreaded == true)
            {
                if (result != "")
                {
                    result = result + ", ";
                }
                result = result + CONSTANT_UNREAD;
            }
            if (filterBookAndChapter == true)
            {
                if (result != "")
                {
                    result = result + ", ";
                }
                result = result + CONSTANT_BOOK_CHAPTER;
            }
            return result;
        }

        public void reorder() {
            if (filterEnabled == true && filterUnreaded == false && filterFavorites == false && filterBookAndChapter == false) {
                filterEnabled = false;
            }
            listInterface.updateFilter();
            sort();
            listInterface.refreshList(true);
        }

        public void refreshList() {
            if (listInterface != null) {
                listInterface.refreshList(true);
            }
        }

        public void markMetaAsFavorite(Meta meta) {
            NodePersistance.saveNodeAsFavorite(meta, true);
            JsonObject.saveMeta();
        }

		public void removeMetaAsFavorite(Meta meta)
		{
			NodePersistance.saveNodeAsFavorite(meta, false);
            JsonObject.saveMeta();
		}

        public int getKnowhyNode(string url)
        {
            int resultNumber = -1;
            try
            {
                string matchStrBMC = "knowhy.bookofmormoncentral.org/content/";
                string matchStrES = "bookofmormoncentral.es/content/";
                if (url.Contains(matchStrBMC) == true || url.Contains(matchStrES) == true)
                {
                    string[] items = url.Split('/');
                    if (items.Length > 0)
                    {
                        string result = items[items.Length - 1];
                        if (result.IndexOf('?') >= 0)
                        {
                            result = result.Substring(0, result.IndexOf('?'));
                        }
                        if (result != "")
                        {
                            resultNumber = getSlug(result);

                            if (resultNumber < 0)
                            {
                                result = WebUtility.UrlEncode(result);

                                resultNumber = getSlug(result);
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
            return resultNumber;
        }

        public int getSlug(string name)
        {
            foreach (Slug slug in slugsList)
            {
                if (slug.key == name)
                {
                    return slug.nodeId;
                }
            }
            return -1;
        }

        public Meta getMeta(int node)
        {
            foreach (Meta meta in metaList)
            {
                if (meta.nodeId == node)
                {
                    return meta;
                }
            }
            return null;
        }

        public int getMetaPosition(int node)
        {
            int position = 0;
            foreach (Meta meta in metaList)
            {
                if (meta.nodeId == node)
                {
                    return position;
                }
                position++;
            }
            return -1;
        }

        public void toggleFilterPanel() {
            if (bookChapterPanelStatus == KnoWhy.BOOK_CHAPTER_PANEL_CLOSED)
            {
                bookChapterPanelStatus = KnoWhy.BOOK_CHAPTER_PANEL_CHAPTER;
                listInterface.openFilterPanel();
            } else {
                bookChapterPanelStatus = KnoWhy.BOOK_CHAPTER_PANEL_CLOSED;
                listInterface.closeFilterPanel();
            }
            listInterface.refreshList(false);
            //listInterface.updateFilter();
        }

        public void toggleFilterMode()
        {
            if (bookChapterPanelStatus == KnoWhy.BOOK_CHAPTER_PANEL_BOOK)
            {
                bookChapterPanelStatus = KnoWhy.BOOK_CHAPTER_PANEL_CHAPTER;
                listInterface.hideBooks();
            }
            else
            {
                bookChapterPanelStatus = KnoWhy.BOOK_CHAPTER_PANEL_BOOK;
                listInterface.showBooks();
            }
            listInterface.refreshList(false);
            //listInterface.updateFilter();
        }

        public string getLastUpdate()
        {
            string result = CONSTANT_UPDATE_DATE + ": ";
            if (lastUpdate > 0)
            {
                try
                {
                    DateTime dtDateTime = new DateTime(lastUpdate);
                    DateTime today = DateTime.Today;
                    DateTime lastWeek = DateTime.Now.AddDays(-7);
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    if (today.Day == dtDateTime.Day && today.Month == dtDateTime.Month && today.Year == dtDateTime.Year)
                    {
                        if (locale.StartsWith("es", StringComparison.Ordinal) == true)
                        {
                            result = CONSTANT_UPDATE_DATE  + ": hoy";
                        }
                        else
                        {
                            result = CONSTANT_UPDATE_DATE + ": today";
                        }
                    }
                    else if (yesterday.Day == dtDateTime.Day && yesterday.Month == dtDateTime.Month && yesterday.Year == dtDateTime.Year)
                    {
                        if (locale.StartsWith("es", StringComparison.Ordinal) == true)
                        {
                            result = CONSTANT_UPDATE_DATE + ": ayer";
                        }
                        else
                        {
                            result = CONSTANT_UPDATE_DATE + ": yesterday";
                        }
                    }
                    else if (lastWeek.Ticks < dtDateTime.Ticks)
                    {
                        result = CONSTANT_UPDATE_DATE + ": " + dtDateTime.ToString("dddd", culture);
                    }
                    else
                    {
                        result = CONSTANT_UPDATE_DATE + ": " + dtDateTime.ToString("d", culture);
                    }
                } catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            return result;
        }

        public string[] getLDSUrls(string url) {
            string ldsString = "lds.org";
            if (url.ToLower().Contains(ldsString) == true)
            {
                string partialUrl = url.Substring(url.IndexOf(ldsString, StringComparison.Ordinal) + ldsString.Length + 1);
                string ldsURL = "gospellibrary://content/" + partialUrl;
                string webLDSURL = "https://www.lds.org/" + partialUrl;
                string[] result = new string[2];
                result[0] = ldsURL;
                result[1] = webLDSURL;

                return result;
            }
            else
            {
                return new string[0];
            }
        }

        public void translateConstants() {
            if (locale == "es")
            {
                CONSTANT_PUBLISH_DATE = "Fecha de publicación";
                CONSTANT_CHAPTER_VERSE = "Capítulo y versículo";
                CONSTANT_ASCEND_C = "Ascender";
                CONSTANT_DESCEND_C = "Descender";
                CONSTANT_ASCEND = "ascender";
                CONSTANT_DESCEND = "descender";
                CONSTANT_FAVORITES = "Favoritos";
                CONSTANT_UNREAD = "Sin leer";
                CONSTANT_BOOK_CHAPTER = "Libro y Capítulo";
                CONSTANT_SORTED_BY = "Ordenar por:";
                CONSTANT_FILTER_BY = "Filtrar por:";
                CONSTANT_SORT = "ORDENAR:";
                CONSTANT_FILTER = "FILTRAR:";
                CONSTANT_SORT_AND_FILTER = "Ordenar y Filtrar";
                CONSTANT_DONE = "Cerrar";

                CONSTANT_UPDATE_CONTENT = "Actualizar contenido";
                CONSTANT_UPDATE_DATE = "Última actualización";
                CONSTANT_UPDATING = "Actualizando...";
                CONSTANT_SETTINGS = "Configuración";
                CONSTANT_WEB_LINKS = "Enlaces Web";
                CONSTANT_LINK1 = "https://bookofmormoncentral.es";
                CONSTANT_LINK2 = "";
                CONSTANT_LINK3 = "https://www.bookofmormoncentral.es/content/acerca-de-nosotros-bmc";
                CONSTANT_LINK1_DESC = "Book of Mormon Central en Español";
                CONSTANT_LINK2_DESC = "";
                CONSTANT_LINK3_DESC = "Acerca de Book of Mormon Central en Español";
                CONSTANT_SETTINGS_TITLE = "Configuración";
                CONSTANT_AUTOMATIC_UPDATE = "Actualización automática de contenido con WiFi solamente";
                CONSTANT_VERSION = "Versión de aplicación";
                CONSTANT_BUILD = "Contrucción";
                CONSTANT_RESET_CONTENT = "Resetear contenido";
                CONSTANT_RESET_CONTENT_LABEL = "Bajar la última versión de artículos pero mantener los datos de usuario";
                CONSTANT_RESET_DATABASE = "Resetear base de datos";
                CONSTANT_RESET_DATABASE_LABEL = "Resetear todos los datos de usuario y bajar la última versión de artículos";

                CONSTANT_ALERT_RESET1_1 = "¿Resetear contenido?";
                CONSTANT_ALERT_RESET1_2 = "Se eliminará el contenido descargado. Se guardará los datos de usuario.";
                CONSTANT_ALERT_RESET1_3 = "Cancelar";
                CONSTANT_ALERT_RESET1_4 = "Resetear Contenido";
                CONSTANT_ALERT_RESET2_1 = "¿Resetear base de datos?";
                CONSTANT_ALERT_RESET2_2 = "Se eliminará todos los datos de usuario y contenido descargado.";
                CONSTANT_ALERT_RESET2_3 = "Cancelar";
                CONSTANT_ALERT_RESET2_4 = "Resetear base de datos";

                CONSTANT_ALL_BOOKS = "Todo";
                CONSTANT_ALL_CHAPTERS = "Todos los Capítulos";

                CONSTANT_SELECT_BOOK = "Seleccionar Libro";
                CONSTANT_SELECT_CHAPTER = "Seleccionar Capítulo";

                CONSTANT_BOOK = "Libro";
                CONSTANT_CHAPTER = "Capítulo";

                CONSTANT_LOADING = "Cargando...";

                CONSTANT_CONNECTION_FAILED = "No hay conexión a internet, intente nuevamente";

                CONSTANT_ERROR_TITLE = "Error";

                CONSTANT_EMPTY_LIST = "Ningún artículo coincide con los criterios del filtro.";
            }
        }
    }
}
