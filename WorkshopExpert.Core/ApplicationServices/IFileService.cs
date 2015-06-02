using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopExpert.Core.ApplicationServices
{
    public interface IFileService
    {
        string UploadFile(Stream inputStream, string destinationPath, string fileName, int fileSize);
        //string UploadFiles(Stream inputStreams, string destinationPath, string fileName, int fileSize);
    }
}
