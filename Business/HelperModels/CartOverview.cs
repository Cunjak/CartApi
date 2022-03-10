using System;
using System.Collections.Generic;

namespace Business.HelperModels
{
    public class CartOverview
    {
        public int Id { get; set; }      
        public string Status { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
        public DateTimeOffset? TimeUpdated { get; set; }       
        public string CreatedBy { get; set; }
        public List<CartItemBasicAttributes> CartItemsBasicAttributes { get; set; }
    }
}
