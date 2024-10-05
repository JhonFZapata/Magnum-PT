using System.ComponentModel.DataAnnotations;

namespace Magnum_PPT.Dto
{
    public class PlayerDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del jugador es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre del jugador no debe exceder los 100 caracteres")]
        public string Name { get; set; }
    }
}
