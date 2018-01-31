using System;
using System.Collections.Generic;
using System.Text;

namespace KnoWhy.Model
{
    public class Image
    {
        public Image()
        {
        }

        public int nodeId { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }
}
