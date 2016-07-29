using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class StateRepository : IStateRepository
    { 
        private const string _FilePath = @"DataFiles/Taxes.txt";

        public StateRepository()
        {
            GetAllStates();
        }

        List<State> results = new List<State>();
        public List<State> GetAllStates()
        {
          
            var rows = File.ReadAllLines(_FilePath);

            for (int i = 1; i < rows.Length; i++)
            {
                var columns = rows[i].Split('|');
                var state = new State();
                state.StateAbbreviation = columns[0];
                state.StateName = columns[1];
                state.TaxRate = decimal.Parse(columns[2]);
                results.Add(state);
            }
            return results;
        }

        public State GetStateByName(string name)
        {
            return results.FirstOrDefault(a => a.StateAbbreviation == name);
        }
    }
}
