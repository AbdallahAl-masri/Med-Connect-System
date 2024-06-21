using MCS.Entities;

namespace MCS.Models
{
    public class UpdateStorageViewModel
    {
        public long MedicineId { get; set; }
        public string MedicineName { get; set; }
        public long Stock { get; set; }
        public long Shelf { get; set; }
        public List<Pharmacy> ExistingStorage { get; set; } = new List<Pharmacy>();
    }
}
