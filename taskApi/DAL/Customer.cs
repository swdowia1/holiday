﻿namespace taskApi.DAL
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
    }
}