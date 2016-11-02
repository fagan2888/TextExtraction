using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextExtration.TextObject;

namespace TextExtraction.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //doc test scenario
            //1. open and close doc text object
            System.Console.WriteLine("open and close doc text object");
            DocTextObject docText1 = new DocTextObject(@" .\testdocuments\Test document.docx");
            docText1.open();
            Thread.Sleep(1000);
            docText1.close();

            System.Console.ReadLine();
            //2. open and close other doc text object
            System.Console.WriteLine("");
            //
        }
    }
}
