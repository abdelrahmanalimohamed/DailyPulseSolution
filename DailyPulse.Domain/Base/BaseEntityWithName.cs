namespace DailyPulse.Domain.Base
{
    public abstract class BaseEntityWithName : BaseEntity
    {
        public string Name { get; set; }
    }
}