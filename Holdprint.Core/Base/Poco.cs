using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holdprint.Core.Base
{
    public abstract class Poco
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public virtual int Id { get; set; }
    }
}
