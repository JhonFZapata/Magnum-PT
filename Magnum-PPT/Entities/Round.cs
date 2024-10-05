using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Magnum_PPT.Entities
{
    public class Round
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public string PlayerOneMove { get; set; }

        public string PlayerTwoMove { get; set; }

        public int? WinnerPlayerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
