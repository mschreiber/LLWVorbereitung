using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.Model
{
    public class Project
    {

        [Key, Column("project_id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Budget { get; set; }

        [ForeignKey("leader_id")]
        public Employee? Leader { get; set; }


        // Method that returns the leaders full name or n/a if no leader set
        // not mapped tells ef core that this is not a db column
        [NotMapped]
        public string LeaderName
        {
            get
            {
                if (Leader == null)
                {
                    return "n/a";
                }
                return Leader.Fullname;
            }
        }
    }
}
