using System;
using System.Linq;
using System.Threading;
using Realms;
using System.Threading.Tasks;

namespace KnoWhy.Model
{
    public class NodePersistance : RealmObject
    {
        public static int FAVORITE = 1;
        public static int READED = 2;

		public int nodeId { get; set; }
		public int mode { get; set; }

        public NodePersistance()
        {
        }

        public static bool nodeIsFavorite(Meta meta) {

            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            var items = realm.All<NodePersistance>().Where(d => d.nodeId == meta.nodeId && d.mode == NodePersistance.FAVORITE);
            if (items.Count() > 0) {
                meta.isFavorite = true;
                return true;
            } else {
                return false;
            }
        }

		public static bool nodeIsReaded(Meta meta)
        {
            var realm = Realm.GetInstance(KnoWhy.Current.getRealmConfiguration());
            // Use LINQ to query
            var items = realm.All<NodePersistance>().Where(d => d.nodeId == meta.nodeId && d.mode == NodePersistance.READED);
			if (items.Count() > 0)
			{
                meta.isRead = true;
                return true;
			}
			else
			{
                meta.isRead = false;
                return false;
			}
		}

        public static void saveNodeAsFavorite(Meta meta, bool isFavorite)
        {
            meta.isFavorite = isFavorite;
            if (KnoWhy.Current.filterEnabled == true && KnoWhy.Current.filterFavorites == true)
            {
                KnoWhy.Current.reorder();
            }
            else
            {
                KnoWhy.Current.refreshList();
            }
            //new Task(() =>
            //{
            var realm2 = KnoWhy.Current.getDatabase();

                var persistanceItem = realm2.All<NodePersistance>().Where(d => d.nodeId == meta.nodeId && d.mode == NodePersistance.FAVORITE);
                if (isFavorite == true)
                {
                    if (persistanceItem.Count() == 0)
                    {
                        var obj = new NodePersistance
                        {
                            nodeId = meta.nodeId,
                            mode = NodePersistance.FAVORITE
						};
						realm2.Write(() => realm2.Add(obj));
                    }
                } else {
                    if (persistanceItem.Count() > 0)
					{
                        
						using (var trans = realm2.BeginWrite())
						{
                            realm2.Remove(persistanceItem.First());
							trans.Commit();
						}
						

					} 
                }

			//}).Start();

        }

        public static void saveNodeAsReaded(Meta meta)
        {
            meta.isRead = true;
            KnoWhy.Current.refreshList();
            //new Task(() =>
            //{
            var realm2 = KnoWhy.Current.getDatabase();

                var persistanceItem = realm2.All<NodePersistance>().Where(d => d.nodeId == meta.nodeId && d.mode == NodePersistance.READED);
                if (persistanceItem.Count() == 0)
				{
					var obj = new NodePersistance
					{
						nodeId = meta.nodeId,
                        mode = NodePersistance.READED
					};
					realm2.Write(() => realm2.Add(obj));
				}

			//}).Start();
        }
    }
}
