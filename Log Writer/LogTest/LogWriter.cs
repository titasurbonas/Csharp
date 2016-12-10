using System.IO;

namespace LogTest
{
    /// <summary>
    /// this class responsible for writing the logs in the file
    /// </summary>
    class LogWriter
    {
        private string filePath;

        public LogWriter(string filePath)
        {
            this.filePath = filePath;
        }
        public void insertFirstLine()
        {
            using (var Writer = new StreamWriter(filePath,true )) { 
            Writer.WriteLine("Timestamp".PadRight(24, ' ') +
                "| " + "Data".PadRight(15, ' ') + "\t" );
                
        }
        }
        public void writeLine(string log)
        {
            using (var Writer = new StreamWriter(filePath, true))
            Writer.WriteLine(log);
        }
    }
}
