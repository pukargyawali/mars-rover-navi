using System;
using System.Collections.Generic;
using RoverControlService.RoverMovementData.DirectionData;

namespace RoverControlService.Rover
{
    public class MarsRover : IRover
    {
        private readonly IList<IDirectionInformation> _directionInformation;
        private readonly int _startingX;
        private readonly int _startingY;
        public int roverId { get; set; }


        public MarsRover(IList<IDirectionInformation> directionInformation, int startingX, int startingY)
        {
            _directionInformation = directionInformation;
            _startingX = startingX;
            _startingY = startingY;
        }

        public IList<IDirectionInformation> graphNodes => _directionInformation;

        /// <summary>
        /// Get the initial x axis of the rover
        /// Multiplying by 100 to match the scale in the UI
        /// </summary>
        public int StartingX => _startingX * 100;

        /// <summary>
        /// Get the initial y axis of the rover
        /// Multiplying by 100 to match the scale in the UI
        /// </summary>
        public int StartingY => _startingY * 100;
    }
}
