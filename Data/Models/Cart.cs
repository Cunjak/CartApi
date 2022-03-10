using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Data.Models
{
    [Table("Cart")]
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        public DateTimeOffset? TimeCreated { get; set; }
        public DateTimeOffset? TimeUpdated { get; set; }
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        [InverseProperty(nameof(CartItem.Cart))]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
