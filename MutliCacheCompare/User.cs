namespace MutliCacheCompare
{
    public sealed class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid Unique { get; set; }
    }
}
