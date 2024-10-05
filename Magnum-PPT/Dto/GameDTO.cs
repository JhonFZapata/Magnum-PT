namespace Magnum_PPT.Dto
{
    public class GameDTO
    {
        public int Id { get; set; }
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
        public bool IsFinished { get; set; }
        public int? WinnerPlayerId { get; set; }
        public List<RoundDTO> Rounds { get; set; }
    }
}
