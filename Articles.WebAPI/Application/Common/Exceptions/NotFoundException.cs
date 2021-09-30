using System;

namespace Articles.WebAPI.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, int id)
            : base(message)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
