using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public interface IStateRepository
    {
        State GetStateByName(string name);
        List<State> GetAllStates();
    }
}
