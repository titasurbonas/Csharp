using System;

namespace LogUsers
{
    using System.Threading;

    using LogTest;
    class Program
    {


        static void Main(string[] args)
        {
            string fileName = "Log";
            string filePath = @"C:\LogTest";
            int threadSleep = 50;

            ILog logger = new AsyncLog(fileName,filePath, threadSleep);
            while (true)
            {
                for (int i = 0; i < 15; i++)
                {
                    logger.Write("Number with Flush: " + i.ToString());
                    Thread.Sleep(50);
                }

                logger.StopWithFlush();
                Thread.Sleep(50);

                for (int i = 15; i < 50; i++)
                {
                    logger.Write("Number with Flush: " + i.ToString());
                    Thread.Sleep(50);
                }
                 fileName = "Log";
                 filePath = @"C:\LogTest";
                ILog logger2 = new AsyncLog(fileName,filePath, threadSleep);

                for (int i = 100; i > 0; i--)
                {
                    logger2.Write("Number with No flush: " + i.ToString());
                    Thread.Sleep(20);
                }
                
                logger2.StopWithoutFlush();
                Console.WriteLine("finish");
                Console.ReadLine();
            }
        }
    }
}
