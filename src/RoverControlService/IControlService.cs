using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoverControlService.Rover;
using RoverControlService.RoverMovementData.DirectionData;

namespace RoverControlService
{
    public interface IControlService
    {
       
        /// <summary>
        /// Executes the IRover movement commands against the IRover encapsulated in this IRoverController.
        /// </summary>
        public void GenerateRoverMovementCommands();

        /// <summary>
        /// List of Rover that contains its movement in the graph 
        /// </summary>
        public IList<IRover> rovers { get; }

    }
}
