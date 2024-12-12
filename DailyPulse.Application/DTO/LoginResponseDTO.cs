namespace DailyPulse.Application.DTO
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string Role {  get; set; }
    }
}