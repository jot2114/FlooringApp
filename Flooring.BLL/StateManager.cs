using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class StateManager
    {
        private readonly IStateRepository stateRepo;

        public StateManager()
        {
            stateRepo = FactoryRepository.GetTaxRepository();
        }

        public StateResponse GetState(string stateAbbr)
        {
            var result = new StateResponse();

            try
            {
                var state = stateRepo.GetStateByName(stateAbbr);

                if (state == null)
                {
                    result.Success = false;
                    result.Message = "State name was not found";
                }
                else if (state.StateAbbreviation != "OH" && state.StateAbbreviation != "MI" && state.StateAbbreviation != "IL" && state.StateAbbreviation != "PA")
                {
                    result.Success = false;
                    result.Message = "Invalid entry";
                }
                else
                {
                    result.Success = true;
                    List<State> states = new List<State>();
                    states.Add(state);
                    result.StateList = states;
                }
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "There was an error. Please try again";
            }
            return result;
        }
    }
}
