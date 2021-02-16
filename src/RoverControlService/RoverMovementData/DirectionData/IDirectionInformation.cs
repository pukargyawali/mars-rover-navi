using System;
using System.Collections.Generic;

namespace RoverControlService.RoverMovementData.DirectionData
{
    public interface IDirectionInformation
    {
        /// <summary>
        /// A Property that gets the human-friendly name of the direction.
        /// 'L' Left
        /// 'U' Move Up
        /// 'R' Move Right
        /// 'D' Move Down
        /// </summary>
        string Name { get; }
        /// <summary>
        /// A Property that gets the X offset of the direction.
        /// </summary>
        int XOffset { get; }
        /// <summary>
        /// A Property that gets the Y offset of the direction.
        /// </summary>
        int YOffset { get; }
        /// <summary>
        /// A property that sets the order of the movement for the rover. 
        /// </summary>
        int DirectionOrder { get; } 
    }
}
