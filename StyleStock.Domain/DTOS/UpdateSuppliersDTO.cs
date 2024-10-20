using StyleStock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleStock.Domain.DTOS
{
	public class UpdateSuppliersDTO : CreateSuppliersDTO
	{
		[Key]
        public int SupplierID { get; set; }

		public static implicit operator UpdateSuppliersDTO(Suppliers v)
		{
			throw new NotImplementedException();
		}
	}
}
