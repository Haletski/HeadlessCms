using System;

namespace Articles.WebAPI.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}