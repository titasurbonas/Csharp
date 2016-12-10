using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LogTest
{
    /// <summary>
    /// The class is controller for all and checking for the next day to create a new file
    /// </summary>
    public class AsyncLog : ILog
    {
        private Thread appThread;
        private int sleep;

        private List<LogLine> toWrite;
        private List<LogLine> CopytoWrite;

        private bool Run;
        private bool stopWithFlush;

        private DateTime startDate;
        private CreateFile fileCreater;
        private LogWriter Writer;

        public string fullFilePath
        {
            get;
            private set;
        }
        public string filename
        {
            get;
            private set;
        }
        public string filePath
        {
            get;
            private set;
        }

        public AsyncLog(string FileName, string FilePath, int ThreadSleep)
        {
            this.filename = FileName;
            this.filePath = FilePath;
            this.sleep = ThreadSleep;

            fileCreater = new CreateFile();

            fileCreater.makeLogFile(filename, FilePath);
            fullFilePath = fileCreater.fullfilePath;

            Writer = new LogWriter(fileCreater.fullfilePath);
            Writer.insertFirstLine();

            toWrite = new List<LogLine>();

            Run = true;
            stopWithFlush = false;

            startDate = DateTime.Now;

            appThread = new Thread(Main);
            appThread.Start();
        }

        private void Main()
        {

            while (Run)
            {
                if (!stopWithFlush)
                {
                    CopytoWrite = new List<LogLine>(toWrite);
                }

                foreach (var logLine in CopytoWrite)
                {
                    toWrite.Remove(logLine);
                }

                if (CopytoWrite.Any())
                {
                    foreach (LogLine logLine in CopytoWrite)
                    {

                        if (!Run)
                            break;

                        if (DateTime.Now.Day - startDate.Day != 0)
                        {
                            startDate = DateTime.Now;
                            fileCreater.makeLogFile(filename, filePath);
                            Writer.insertFirstLine();

                            Writer.writeLine(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff")
                                + " | " + logLine.Text.ToString());

                        }

                        Writer.writeLine(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff")
                            + " | " + logLine.Text.ToString());


                        Thread.Sleep(sleep);
                    }

                    CopytoWrite.Clear();
                }
            }
        }

        public void StopWithFlush()
        {
            this.stopWithFlush = true;
        }

        public void StopWithoutFlush()
        {
            this.Run = false;
        }

        public void Write(string text)
        {
            if (Run & !stopWithFlush)
            {
                this.toWrite.Add(new LogLine()
                { Text = text, Timestamp = DateTime.Now });
            }
        }

        public void setDate(DateTime time)
        {
            this.startDate = time;
        }
    }
}