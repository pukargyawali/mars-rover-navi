using System;
namespace RoverFileProcessor.FileData
{
    public interface IProcessedRoverData
    {
        /// <summary>
        /// A Property that gets starting X Coordinate of Rover
        /// </summary>
        int StartingX { get; }
        /// <summary>
        /// A Property that gets starting X Coordinate of Rover
        /// </summary>
        int StartingY { get; }
        /// <summary>
        /// A property to get the starting Direction of rover(N, E, W, S)
        /// </summary>
        string StartingDirection { get; }
        /// <summary>
        /// A property to get the string of movement commands for the rover to perform.
        /// </summary>
        string RoverCommands { get; }       
    }
}
