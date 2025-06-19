namespace BuildingBlocks.Model.Data
{
    public class EntityBase
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }

    public class EntityBase<T> : EntityBase where T : struct
    {
        public T Id { get; set; }
    }
}
