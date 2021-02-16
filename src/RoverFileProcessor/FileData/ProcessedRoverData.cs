namespace RoverFileProcessor.FileData
{
    public class ProcessedRoverData : IProcessedRoverData
    {
        private readonly int _startingX;
        private readonly int _startingY;
        private readonly string _startingDirection;
        private readonly string _roverCommand;

        /// <summary>
        /// The ProcessedRoverData constructor  
        /// This entire class is read-only.
        /// </summary>
        public ProcessedRoverData(
            int startX,
            int startY,
            string startingDirection,
            string roverCommand) =>
            (_startingX, _startingY, _startingDirection, _roverCommand) =
                (startX, startY, startingDirection, roverCommand);

        public int StartingX => _startingX;

        public int StartingY => _startingY;

        public string StartingDirection => _startingDirection;

        public string RoverCommands => _roverCommand;
    }

}
