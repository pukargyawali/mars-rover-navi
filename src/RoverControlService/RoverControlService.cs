
using System.Collections.Generic;
using RoverControlService.Rover;
using RoverControlService.RoverMovementData.DirectionData;
using RoverFileProcessor.FileData;
using System.Linq;

namespace RoverControlService
{
    public class RoverService : IControlService
    {        
        private readonly IList<IProcessedRoverData> _processedRoverData;
        private readonly IList<IRover> _rovers = new List<IRover>();

        public RoverService(IList<IProcessedRoverData> processedRoverData)
        {
            _processedRoverData = processedRoverData;
            GenerateRoverMovementCommands();
        }

        public IList<IRover> rovers => _rovers;      


        /// <summary>
        /// Executes the IRover movement commands against the IRover encapsulated in this IRoverController.
        /// </summary>
        public void GenerateRoverMovementCommands()
        {
            //create rover object for each processedData
            foreach (var roverData in _processedRoverData)
            {
                var roverDirectionInformation = new List<IDirectionInformation>();
                //var initialPosition = new RoverDirectionInformation(roverData.StartingDirection, roverData.StartingX, roverData.StartingY, 0);
                
                int count = 0;
                var currentOrientation = roverData.StartingDirection[0];
                var curentX = (roverData.StartingX) * 100;
                var currentY = (roverData.StartingY) * 100;
                //roverDirectionInformation.Add(new RoverDirectionInformation(roverData.StartingDirection, curentX, currentY, 0));
                foreach (char command in roverData.RoverCommands)
                {
                    if (command == 'L'|| command == 'R')
                    {
                        var newOrientation = (GetOrientationMap()
                                            .Where(t => t.Item1 == currentOrientation && t.Item2 == command))
                                            .Select(tr => tr.Item3).FirstOrDefault();
                        currentOrientation = newOrientation;
                    }
                    
                    else if (command == 'M')
                    {
                        count += 1;
                        switch (currentOrientation)
                        {
                            case 'N':
                                currentY = currentY - 100;
                                roverDirectionInformation.Add(new RoverDirectionInformation("U", curentX, currentY, count));
                                break;
                            case 'S':
                                currentY = currentY + 100;
                                roverDirectionInformation.Add(new RoverDirectionInformation("D", curentX, currentY, count));
                                break;
                            case 'E':
                                curentX = curentX + 100;
                                roverDirectionInformation.Add(new RoverDirectionInformation("R", curentX, currentY, count));
                                break;
                            case 'W':
                                curentX = curentX - 100;
                                roverDirectionInformation.Add(new RoverDirectionInformation("L", curentX, currentY, count));
                                break;
                            default:
                                break;
                        }
                        
                    }                   
                }
                _rovers.Add(GetRover(roverDirectionInformation, roverData.StartingX, roverData.StartingY));
            }
        }

        private IRover GetRover(IList<IDirectionInformation> roverDirectionInformation, int startingX, int startingY)
        {
            return new MarsRover(roverDirectionInformation, startingX, startingY);
            
        }

        private IEnumerable<(char, char, char)> GetOrientationMap() 
        {            
            var orientation = new List<(char, char, char)>();
            orientation.Add(('N', 'R', 'E'));
            orientation.Add(('N', 'L', 'W'));
            orientation.Add(('S', 'R', 'W'));
            orientation.Add(('S', 'L', 'E'));
            orientation.Add(('E', 'R', 'S'));
            orientation.Add(('E', 'L', 'W'));
            orientation.Add(('W', 'R', 'N'));
            orientation.Add(('W', 'L', 'S'));
            return orientation;
        
        }

        
    }
}
