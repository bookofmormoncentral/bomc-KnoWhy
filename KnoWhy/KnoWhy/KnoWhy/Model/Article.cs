using System;
using System.Collections.Generic;
using System.Text;

namespace KnoWhy.Model
{
    public class Article
    {
        public Article()
        {

        }

        public int nodeId { get; set; }
        public bool isLoading { get; set; }
        public string fullHtml { get; set; }
        public string pdfUrl { get; set; }
        public string scriptureQuote { get; set; }
        public string soundcloudUrl { get; set; }
        public string summary { get; set; }
        public long timestampUpdated { get; set; }
        public string vimeoUrl { get; set; }
        public string youtubeUrl { get; set; }

        public string parsedHTML { get; set; }
    }
}
