using MemoryPack;
using System.Text;

namespace MutliCacheCompare
{
    [MemoryPackable]
    public partial class UserPacked
    {
        [MemoryPackOrder(0)]
        public int Id { get; set; }

        [MemoryPackOrder(1)]
        public string Name { get; set; } = default!;

        [MemoryPackOrder(2)]
        public Guid Unique { get; set; }

        [MemoryPackOrder(3)]
        public string Data { get; set; } = default!;

        [MemoryPackOrder(4)]
        public int Size { get; set; }
    }

}
