using Microsoft.EntityFrameworkCore;
using StyleStock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Domain.DTOS
{
    public class CreateSuppliersDTO
    {
 
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}
