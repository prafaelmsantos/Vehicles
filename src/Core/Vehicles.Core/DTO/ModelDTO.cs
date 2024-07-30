namespace Vehicles.Core.DTO
{
    public class ModelDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long MarkId { get; set; }
        public MarkDTO? Mark { get; set; }
    }
}
