﻿namespace IbulakStoreServer.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageFileName { get; set;}
    }
}
