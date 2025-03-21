namespace DailyPulse.Application.DTO
{
    internal record ProjectByNameDTO(
        Guid id ,
        string name , 
        string region , 
        string location , 
        string createdDate ,
        string createdBy,
        string trades);
}
