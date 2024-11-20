
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Ticket
    {
        [Key]
        public int No { get; set; }
        public string? TicketStatus { get; set; }
        public string? StatusName { get; set; }
        public string? NoRFC { get; set; }
        public string? NoRLM { get; set; }
        public string? Subject { get; set; }
        public string? PIC { get; set; }
        public DateTime? DeploymentDate { get; set; }
        public string? Description { get; set; }
        public string? Account { get; set; }
    }
}
