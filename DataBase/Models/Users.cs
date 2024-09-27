using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public int IdRole { get; set; }
        [Required]
        public byte[]? SaltPassword { get; set; }
        [Required]
        public byte[]? HashPassword { get; set; }
        [ForeignKey("IdRole")]
        public virtual Rols? Role { get; set; }
    }
}
