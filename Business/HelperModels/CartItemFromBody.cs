using System.ComponentModel.DataAnnotations;

namespace Business.HelperModels
{
    public class CartItemFromBody
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }
    }
}
