namespace Magnum_PPT.Dto
{
    public class RoundDTO
    {
        public int Id { get; set; }  // ID de la ronda
        public string PlayerOneMove { get; set; }  // Movimiento del Jugador 1 (como string)
        public string PlayerTwoMove { get; set; }  // Movimiento del Jugador 2 (como string)
        public int? WinnerPlayerId { get; set; }  // ID del jugador que ganó la ronda (null si es empate)
        public DateTime CreatedAt { get; set; }  // Fecha de creación de la ronda
    }
}
