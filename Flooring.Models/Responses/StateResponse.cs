using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class StateResponse : Response
    {
        public List<State> StateList { get; set; }
    }
}
