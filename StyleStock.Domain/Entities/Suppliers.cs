using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace StyleStock.Domain.Entities
{
	public class Suppliers
    {
        [Key]
        public int SupplierID { get; set; }
      
        public string Name { get; set; }
     
        public string Phone { get; set; }
   
        public string Email { get; set; }

        public string Adress { get; set; }
    }
}
