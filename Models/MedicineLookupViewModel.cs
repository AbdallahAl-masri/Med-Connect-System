namespace MCS.Models
{
    public class MedicineLookupViewModel
    {
        public string MedicineName { get; set; }
        public IEnumerable<MedicineViewModel>? SearchResults { get; set; }
    }

    public class MedicineViewModel
    {
        public string MedicineName { get; set; }
        public long Stock { get; set; }
        public string Shelf { get; set; }
    }
}
