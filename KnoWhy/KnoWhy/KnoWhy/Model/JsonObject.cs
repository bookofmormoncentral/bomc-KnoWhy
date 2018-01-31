using Newtonsoft.Json.Linq;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KnoWhy.Model
{
    public class JsonObject : RealmObject
    {
        public static int META = 1;
        public static int SLUG = 2;
        public static int ARTICLE = 3;

        public int identificator { get; set; }
        public string json { get; set; }
        public int mode { get; set; }

        public JsonObject()
        {
        }

        public static void fillSavedMeta()
        {
            List<JObject> list = new List<JObject>();
            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            JsonObject jsonObject = null;
            var savedItems = realm.All<JsonObject>().Where(d => d.mode == JsonObject.META);
            if (savedItems != null) {
                if (savedItems.Count() > 0) {
                    jsonObject = savedItems.First();
                }
            }
            string result = null;
            if (jsonObject != null) {
                if (jsonObject.json != "") {
                    result = jsonObject.json;
                }
            }
            List<Meta> metas = new List<Meta>();
            if (result != null) {
                metas = JsonConvert.DeserializeObject<List<Meta>>(result);
                KnoWhy.Current.allMetaList = metas;
            }
        }

        public static void saveMeta()
        {
            //var realm2 = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            var realm2 = KnoWhy.Current.getDatabase();

            string json = JsonConvert.SerializeObject(KnoWhy.Current.allMetaList);

            JsonObject jsonObject = null;
            var savedItems = realm2.All<JsonObject>().Where(d => d.mode == JsonObject.META);
            if (savedItems != null)
            {
                if (savedItems.Count() > 0)
                {
                    jsonObject = savedItems.First();
                }
            }
            if (jsonObject == null)
            {
                var obj = new JsonObject
                {
                    json = json,
                    mode = JsonObject.META
                };
                realm2.Write(() => realm2.Add(obj));
            } else {
                realm2.Write(() => { jsonObject.json = json; });
            }
        }

        public static void fillSavedSlug()
        {
            List<JObject> list = new List<JObject>();
            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            JsonObject jsonObject = null;
            var savedItems = realm.All<JsonObject>().Where(d => d.mode == JsonObject.SLUG);
            if (savedItems != null)
            {
                if (savedItems.Count() > 0)
                {
                    jsonObject = savedItems.First();
                }
            }
            string result = null;
            if (jsonObject != null)
            {
                if (jsonObject.json != "")
                {
                    result = jsonObject.json;
                }
            }
            List<Slug> slugs = new List<Slug>();
            if (result != null)
            {
                slugs = JsonConvert.DeserializeObject<List<Slug>>(result);
                KnoWhy.Current.slugsList = slugs;
            }
        }

        public static void saveSlugs()
        {
            //var realm2 = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            var realm2 = KnoWhy.Current.getDatabase();

            string json = JsonConvert.SerializeObject(KnoWhy.Current.slugsList);

            JsonObject jsonObject = null;
            var savedItems = realm2.All<JsonObject>().Where(d => d.mode == JsonObject.SLUG);
            if (savedItems != null)
            {
                if (savedItems.Count() > 0)
                {
                    jsonObject = savedItems.First();
                }
            }
            if (jsonObject == null)
            {
                var obj = new JsonObject
                {
                    json = json,
                    mode = JsonObject.SLUG
                };
                realm2.Write(() => realm2.Add(obj));
            }
            else
            {
                realm2.Write(() => { jsonObject.json = json; });
            }
        }

        public static Article getArticle(int nodeId)
        {
            List<JObject> list = new List<JObject>();
            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            JsonObject jsonObject = null;
            var savedItems = realm.All<JsonObject>().Where(d => d.mode == JsonObject.ARTICLE && d.identificator == nodeId);
            if (savedItems != null)
            {
                if (savedItems.Count() > 0)
                {
                    jsonObject = savedItems.First();
                }
            }
            string result = null;
            if (jsonObject != null)
            {
                if (jsonObject.json != "")
                {
                    result = jsonObject.json;
                }
            }
            Article article = null;
            if (result != null)
            {
                article = JsonConvert.DeserializeObject<Article>(result);
            }
            return article;
        }

        public static void saveArticle(Article article)
        {
            //var realm2 = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            var realm2 = KnoWhy.Current.getDatabase();

            string json = JsonConvert.SerializeObject(article);

            JsonObject jsonObject = null;
            var savedItems = realm2.All<JsonObject>().Where(d => d.mode == JsonObject.ARTICLE && d.identificator == article.nodeId);
            if (savedItems != null)
            {
                if (savedItems.Count() > 0)
                {
                    jsonObject = savedItems.First();
                }
            }
            if (jsonObject == null)
            {
                var obj = new JsonObject
                {
                    identificator = article.nodeId,
                    json = json,
                    mode = JsonObject.ARTICLE
                };
                realm2.Write(() => realm2.Add(obj));
            }
            else
            {
                realm2.Write(() => { jsonObject.json = json; });
            }
        }
    }
}
