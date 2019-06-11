using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsHabr.Models
{
    public class IndexViewModel
    {
        public IEnumerable<NewsModel> News { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string sort { get; set; }
        public string filter { get; set; }
    }
}
