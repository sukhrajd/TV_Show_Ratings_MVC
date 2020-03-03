using System;
using System.ComponentModel.DataAnnotations;

namespace TV_Show_Ratings_MVC.Models
{

    public class Rating
    {
        public int Id { get; set; }

        public int SubscriberId { get; set; }

        public int TVShowId { get; set; }

        public Subscriber Subscriber { get; set; }

        public TVShow TVShow { get; set; }

        [Range(0, 5)]
        public int RatingValue { get; set; }



    }
}
