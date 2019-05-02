using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KLoveDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Column("varchar(50)")]
        public string Department { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage ="This field is required.")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(10)")]
        [DisplayName("Phone Number")]
        public string PhoneNum { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
    }
}
