using afternoon.Models;

namespace afternoon.Models
{
    public class Undertaking
    {
        public string docketId { get; set; }
        public int Id { get; set; }
        public string Body { get; set; }
        public bool Public { get; set; }

        public string creatorId { get; set; }

        public Profile Creator { get; set; }
    }
}