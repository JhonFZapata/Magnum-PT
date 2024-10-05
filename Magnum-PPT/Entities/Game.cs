using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Magnum_PPT.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PlayerOneId { get; set; }

        [ForeignKey("PlayerOneId")]
        public Player PlayerOne { get; set; }

        [Required]
        public int PlayerTwoId { get; set; }

        [ForeignKey("PlayerTwoId")]
        public Player PlayerTwo { get; set; }

        public bool IsFinished { get; set; }

        public int? WinnerPlayerId { get; set; }

        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}