using System;
using System.ComponentModel.DataAnnotations;

namespace TV_Show_Ratings_MVC.Models
{
    public class TVChannel
    {

        public int Id { get; set; }

        public string ChannelName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Established { get; set; }
    }
}
