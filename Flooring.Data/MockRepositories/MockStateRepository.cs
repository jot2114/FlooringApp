
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class MockStateRepository: IStateRepository
    {
        private List<State> states;

        public MockStateRepository()
        {
            states = new List<State>()
            {
                new State() {StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 4.5m},
                new State() {StateAbbreviation = "MI", StateName = "Michigan",TaxRate = 5.5m},
                new State() {StateAbbreviation = "CA", StateName = "California", TaxRate = 6.5m}
            };
        }

        public State GetStateByName(string name)
        {
            return states.FirstOrDefault(o => o.StateAbbreviation == name);
        }

        public List<State> GetAllStates()
        {
            return states;
        }
    }
}
