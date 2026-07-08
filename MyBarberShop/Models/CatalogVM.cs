namespace MyBarberShop.Models
{
    public class CatalogVM
    {
        public IEnumerable<CategoryVM> Categories{  get; set; }
        public IEnumerable<CorteCabelloVM> cortesCabellos{  get; set; }
        public string filterBy{  get; set; }



    }
}
