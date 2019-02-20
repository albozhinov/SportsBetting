using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBetting.Web.Models
{
    public class AllEventViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public string PageMode { get; set; }
    }
}
