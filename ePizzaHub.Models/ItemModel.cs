﻿using Microsoft.AspNetCore.Http;

namespace ePizzaHub.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public IFormFile File { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity  { get; set; }
        public decimal Total { get; set; }
        public string ImageUrl { get; set; }
       

    }
}
