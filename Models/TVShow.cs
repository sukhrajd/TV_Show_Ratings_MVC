namespace TV_Show_Ratings_MVC.Models
{
    public class TVShow
    {
        public int Id { get; set; }

        public int TVChannelId { get; set; }

        public string ShowName { get; set; }

        public TVChannel TvChannel { get; set; }
    }
}
