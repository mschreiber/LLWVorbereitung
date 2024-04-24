using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRental.Model
{
    public class Item
    {
        [Key, Column("item_id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column("rent_price")]
        public float? Price { get; set; }
        [ForeignKey("category_id")]
        public Category? Category { get; set; }
    }
}
