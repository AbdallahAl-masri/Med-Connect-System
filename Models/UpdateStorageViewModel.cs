using MCS.Entities;

namespace MCS.Models
{
    public class UpdateStorageViewModel
    {
        public string MedicineName { get; set; }
        public long Stock { get; set; }
        public long Shelf { get; set; }
    }
}
