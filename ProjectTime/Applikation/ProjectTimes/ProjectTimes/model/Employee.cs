using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.model
{
    public class Employee
    {
        [Key]
        public int Employee_id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

    }
}
