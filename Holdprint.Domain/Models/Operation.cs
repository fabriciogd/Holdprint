using Holdprint.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holdprint.Domain.Models
{
    public class Operation: Poco
    {
        [Column("NAME")]
        public string Name { get; set; }

        [Column("TIME")]
        public int Time { get; set; }

        [Column("COST")]
        public double Cost { get; set; }
    }
}
