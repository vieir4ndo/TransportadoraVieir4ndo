using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV.DAL.Entities
{
    public class Value
    {
        public int Id {get; set;}
        public string Name { get; set; }
    }
}