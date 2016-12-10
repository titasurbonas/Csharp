using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogTest;
using System.Threading;
using System.IO;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestLog
    {
        /// <summary>
        /// The first test is to check are all logs are written to the file
        /// </summary>

        private string filePath = @"C:\LogTest";
        private string fileName;
        private int threadSleep = 50;

        [TestMethod]
        public void testLogWriten()
        {
            fileName = "test1";
            AsyncLog test = new AsyncLog(fileName, filePath, threadSleep);
            {
                int logSize = 100;
                int lines;

                for (int i = 0; i < logSize; i++)
                {
                    test.Write("Number with Flush: " + i.ToString());
                    Thread.Sleep(50);
                }
                Thread.Sleep(500);

                logSize++;

                lines = File.ReadLines(test.fullFilePath).Count();

                Assert.AreEqual(logSize, lines);
            }
        }

        /// <summary>
        /// The second test is to see if the file on the new day is created it is not that accurate, because it is automatic in the class.
        /// </summary>
        [TestMethod]
        public void testNewLogFileNextDay()
        {
            fileName = "test2";
            AsyncLog test2 = new AsyncLog(fileName, filePath, threadSleep);
            DateTime today = new DateTime();
            Random rnd = new Random();

            int startFiles;
            int finishFiles;
            int logSize = 100;
            int randomNr = rnd.Next(26, 28);

            startFiles = Directory.GetFiles(filePath).Length;
            today = DateTime.Now;

            for (int i = 0; i < logSize; i++)
            {
                if (i == randomNr)
                {
                    test2.setDate(today.AddDays(randomNr));
                }
                test2.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }
            finishFiles = Directory.GetFiles(filePath).Length;
            startFiles++;

            Assert.AreEqual(finishFiles, startFiles);
        }

        /// <summary>
        /// The third test is for checking if the stop with flush is working as supposed to.
        /// </summary>
        [TestMethod]
        public void testStopWithFlush()
        {
            fileName = "test3";
            AsyncLog test3 = new AsyncLog(fileName, filePath, threadSleep);
            Random rnd = new Random();

            int logSize = 100;
            int lines;
            int randomNr = rnd.Next(1, 99);

            for (int i = 0; i < logSize; i++)
            {
                if (i == randomNr)
                {
                    test3.StopWithFlush();
                }
                test3.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }
            Thread.Sleep(500);

            randomNr++;
            

            lines = File.ReadLines(test3.fullFilePath).Count();

            Assert.AreEqual(randomNr, lines);
        }

        /// <summary>
        /// The last test is for stopping the class instantly without waiting it to finish
        /// </summary>
        [TestMethod]
        public void testStopWithOutflush()
        {
            fileName = "test4";
            AsyncLog test4 = new AsyncLog(fileName, filePath, threadSleep);
            Random rnd = new Random();

            int lines;
            int randomNr = rnd.Next(1, 99);

            for (int i = 0; i < randomNr; i++)
            {
                if (i == randomNr)
                {
                    test4.StopWithoutFlush();
                }
                test4.Write("Number with out Flush: " + i.ToString());

                Thread.Sleep(20);
            }
            randomNr++;
            

            lines = File.ReadLines(test4.fullFilePath).Count();

            Assert.IsTrue(randomNr > lines);
        }
    }
}
