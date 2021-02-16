using NUnit.Framework;
using RoverControlService;
using RoverFileProcessor;
using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace RoverAppTests.RoverControlServiceTests
{

    [TestFixture]    
    public class RoverContolServiceTest
    {

        [Test]
        public async void GenerateRoverMovementCommandsIsNotNull()
        {
            string test1 = "1 2 N|LMLMLMM";
            
            // convert string to stream
            byte[] byteArray1 = Encoding.ASCII.GetBytes(test1);
            MemoryStream stream = new MemoryStream(byteArray1);            

            IFileProcessingService fileData = new CsvFileProcessingService(100,500);
            await fileData.ReadFileAsync(stream);

            IControlService roverService = new RoverService(fileData.ProcessedRoverData);

            roverService.GenerateRoverMovementCommands();
            var result = roverService.rovers;
            
            Assert.IsNotNull(result);
        }



        [Test]
        public async void GenerateRoverMovementCommands()
        {
            
            string test1 = "20,20 N|LMLMLMM";// test if code handles extremme data
            // convert string to stream
            byte[] byteArray1 = Encoding.ASCII.GetBytes(test1);
            MemoryStream stream = new MemoryStream(byteArray1);           

            IFileProcessingService fileData = new CsvFileProcessingService(100, 500);
            await fileData.ReadFileAsync(stream);

            IControlService roverService = new RoverService(fileData.ProcessedRoverData);

            roverService.GenerateRoverMovementCommands();
            var result = roverService.rovers;

            Assert.IsNotNull(result);
        }

    }
}
