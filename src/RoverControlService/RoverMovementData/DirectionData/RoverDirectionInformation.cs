using System;
using System.Collections.Generic;

namespace RoverControlService.RoverMovementData.DirectionData
{
    public class RoverDirectionInformation : IDirectionInformation
    {
        private readonly string _name;
        private readonly int _xOffset;
        private readonly int _yOffset;
        private readonly int _directionOrder;

        /// <summary>
        /// The DirectionsInformation constructor populates the name and X and Y offsets of 
        /// the direction.
        /// </summary>
        /// <param name="name">The human-friendly name of the direction.</param>
        /// <param name="xOffset">The X offset of the direction.</param>
        /// <param name="yOffset">The Y offset of the direction.</param>
        public RoverDirectionInformation(string name, int xOffset, int yOffset,
            int directionOrder) => (_name, _xOffset, _yOffset,_directionOrder) = (name, xOffset, yOffset, directionOrder);


        /// <summary>
        /// A Property that gets the human-friendly name of the direction.
        /// </summary>
        public string Name => _name;


        /// <summary>
        /// A Property that gets the X offset of the direction.
        /// </summary>
        public int XOffset => _xOffset;


        /// <summary>
        /// A Property that gets the Y offset of the direction.
        /// </summary>
        public int YOffset => _yOffset;

        /// <summary>
        /// 
        /// </summary>
        public int DirectionOrder => _directionOrder;
       
    }
}
