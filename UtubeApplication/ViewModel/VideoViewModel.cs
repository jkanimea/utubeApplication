using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtubeApplication.ViewModel
{
    public class VideoViewModel
    {
       public int ChannelDropDown { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public string Url { get; set;  }
    }

    public class ChannelViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}