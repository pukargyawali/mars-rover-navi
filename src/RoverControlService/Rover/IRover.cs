using System;
using System.Collections.Generic;
using RoverControlService.RoverMovementData.DirectionData;

namespace RoverControlService.Rover
{
    public interface IRover
    {
        /// <summary>
        /// This property contains the entire path map of the rover movement.
        /// UI will be following this co-ordinates to map the path for the rover
        /// </summary>        
        IList<IDirectionInformation> graphNodes { get; }

        /// <summary>
        /// 
        /// </summary>
        int StartingX { get; }
        /// <summary>
        /// 
        /// </summary>
        int StartingY { get; }
    }
}
