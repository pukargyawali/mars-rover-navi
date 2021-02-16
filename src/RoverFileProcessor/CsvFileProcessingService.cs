
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RoverFileProcessor.FileData;

namespace RoverFileProcessor
{
    public class CsvFileProcessingService : IFileProcessingService
    {
        private readonly IList<IProcessedRoverData> _processedRoverData = new List<IProcessedRoverData>();
        private readonly int _plateuWidth;
        private readonly int _plateuHeight;

        /// <summary>
        /// parses the incomming file stream to retrieve
        /// coordinate and direction data for the rover
        /// </summary>
        public CsvFileProcessingService(int plateuWidth, int plateuHeight) => (_plateuWidth, _plateuHeight) = (plateuWidth, plateuHeight);

        public IList<IProcessedRoverData> ProcessedRoverData => _processedRoverData;

        public async Task ReadFileAsync(Stream data)
        {                  

            try
            {
                using (var streamReader = new StreamReader(data))
                {
                    // The first line is the top-right coordinates
                    var currentLine = String.Empty;
                    if (currentLine == null)
                    {
                        //populate the API object with the error message.
                        throw new Exception("No rover data encountered in input file");
                    }                    
                  
                    // Now handle all other lines per usual for rover data
                    
                    bool isRoverDataPresent = false;

                    while ((currentLine = await streamReader.ReadLineAsync()) != null)
                    {
                        isRoverDataPresent = true;
                        _processedRoverData.Add(ValidateInput(currentLine));
                    }

                    if (!isRoverDataPresent)
                    {
                        throw new Exception("No rover data encountered in input file");
                    }
                }
            }
            catch (EndOfStreamException ex)
            {
                throw new Exception("The input file cannot be found.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("The input file cannot be read (" + ex.Message + ")", ex);
            }
           
        }

        private IProcessedRoverData ValidateInput(string currentLine)
        {            
            Regex roverInitialPositionCheckPattern = new Regex("^[0-9]+ [0-9]+ [NESW]$");
            Regex roverMovementCheckPattern = new Regex("^[LRM]+$");

            var roverDataFromFile= currentLine.Trim().Split('|');
            var roverInitialPostion = roverDataFromFile[0];
            var roverMovement = roverDataFromFile[1];

            if (!roverInitialPositionCheckPattern.IsMatch(roverInitialPostion))
            {
                //add Error Info to the API Data
                throw new Exception("Mal-formed rover initial location data encountered in input file: ");
            }
            if (!roverMovementCheckPattern.IsMatch(roverMovement))
            {
                //add Error Infor to the API Data
                throw new Exception("Mal-formed rover control commands data encountered in input file: ");
            }
            var intialPositionDataString = roverInitialPostion.Trim().Split(' ');
            var initialX = Convert.ToInt32(intialPositionDataString[0]);
            var initialY = Convert.ToInt32(intialPositionDataString[1]);
            var intialDirection = intialPositionDataString[2];

            if (initialX < 0
               || initialX > _plateuWidth)
            {
                //input is not valid, coordinates outside of the grid.
            }
            else if (initialY < 0
                || initialY > _plateuHeight)
            {
                //input is not valid, coordinates outside of the grid.
            }
            return new ProcessedRoverData(initialX, initialY, intialDirection, roverMovement);

        }
    }
}
