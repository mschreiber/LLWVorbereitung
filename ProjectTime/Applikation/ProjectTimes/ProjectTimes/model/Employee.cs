using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.Model
{
    public class Employee
    {
        [Key, Column("employee_id")]
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        // Returns the First and Lastname of the Employee
        [NotMapped]
        public string Fullname { get { return $"{Firstname} {Lastname}"; } }
    }
}
