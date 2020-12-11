using System;
using System.Collections.Generic;

namespace Capstone.Entities
{
    public partial class Worker
    {
        public int WorkerId { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
