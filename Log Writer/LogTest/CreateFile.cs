using System;
using System.IO;

namespace LogTest
{
    /// <summary>
    /// This class is responsible for creation of the file with the right name
    /// </summary>
    class CreateFile
    {
        public string fullfilePath;

        public void makeLogFile(string fileName, string filePath)
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            creatFile(fileName, filePath);
        }

        public void creatFile(string filename, string filePath)
        {
            fullfilePath = (filePath + "/" + filename +
               DateTime.Now.ToString("yyyy.MM.dd HH mm ss fff") + ".log");
            using (var myFile = File.Create(fullfilePath)) ;
            
        }
    }
}
