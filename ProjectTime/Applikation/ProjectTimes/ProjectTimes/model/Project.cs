using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.model
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

    }
}
