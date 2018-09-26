using System;
using System.ComponentModel.DataAnnotations;

namespace taskApi.DAL
{
    public abstract class Entity
    {
        /// <summary>
        /// Primary key for tables
        /// </summary>
        [Key]
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}