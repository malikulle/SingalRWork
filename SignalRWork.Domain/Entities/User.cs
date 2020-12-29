using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRWork.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Team Team { get; set; }
    }
}
