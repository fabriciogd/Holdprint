using Holdprint.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holdprint.Domain.Models
{
    public class Operation: Poco
    {
        [Required]
        [MaxLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("TIME")]
        public int Time { get; set; }

        [Required]
        [Column("COST")]
        public double Cost { get; set; }
    }
}
