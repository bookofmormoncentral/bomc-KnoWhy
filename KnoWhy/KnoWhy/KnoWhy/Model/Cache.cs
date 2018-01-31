using System;
using Realms;

namespace KnoWhy.Model
{
    public class Cache : RealmObject
    {
        public string key { get; set; }
        public string value { get; set; }

        public Cache()
        {
        }
    }
}
