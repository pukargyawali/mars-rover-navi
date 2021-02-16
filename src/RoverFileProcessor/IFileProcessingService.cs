using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RoverFileProcessor.FileData;

namespace RoverFileProcessor
{
    /// <summary>
    /// interface defining the attributes of the file processor
    /// </summary>
    public interface IFileProcessingService
    {     
        /// <summary>
        /// A Property to get a list of objects representing parsed rover data.
        /// </summary>
        public IList<IProcessedRoverData> ProcessedRoverData{ get; }

        /// <summary>
        /// a functionality exposed to read the file directly from the memory stream
        /// </summary>
        /// <param name="file"></param>
        public Task ReadFileAsync(Stream data);
    }
}
