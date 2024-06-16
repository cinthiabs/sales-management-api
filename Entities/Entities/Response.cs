using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Response<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = default!;
        public T? Dados { get; set; } = default!;
    }
}
