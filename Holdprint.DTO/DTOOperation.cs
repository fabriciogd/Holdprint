using Holdprint.Core.Base;
using System.ComponentModel.DataAnnotations;

namespace Holdprint.DTO
{
    public class DTOOperation: Dto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public double Cost { get; set; }
    }
}
