using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace Data.Models
{
    [Table("CartItem")]
    public partial class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public DateTimeOffset? TimeCreated { get; set; }
        public DateTimeOffset? TimeUpdated { get; set; }
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CartId))]
        [InverseProperty("CartItems")]
        public virtual Cart Cart { get; set; }
    }
}
