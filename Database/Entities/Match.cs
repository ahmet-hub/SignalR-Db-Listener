using System.ComponentModel.DataAnnotations;

namespace SignalR_Db_Listener.Database.Entities
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
