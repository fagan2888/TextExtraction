using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TextExtration;
using TextExtration.TextObject;

namespace TextExtraction.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;) {
                System.Console.WriteLine("select test:");
                System.Console.WriteLine("1. open and close doc text object");
                System.Console.WriteLine("2. open and close other doc text object");
                System.Console.WriteLine("or type 'exit'");
                var selectedTest = System.Console.ReadLine();

                if (selectedTest == "1"){
                    //doc test scenario
                    //1. open and close doc text object
                    System.Console.WriteLine("set doctextobject");
                    DocTextObject docText1 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document.docx");
                    System.Console.WriteLine("docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();
                    Thread.Sleep(1000);
                    System.Console.WriteLine("docText1.close()");
                    System.Console.ReadKey();
                    docText1.close();
                }
                if (selectedTest == "2") {
                    //2. open and close other doc text object
                    System.Console.WriteLine("");
                    //    
                }
                if (selectedTest == "3") {
                    string json = File.ReadAllText("testdata\\testblock.json");
                    var block = JsonConvert.DeserializeObject<ExtractionBlock>(json);
                     System.Console.WriteLine(block.order);
                    System.Console.WriteLine(block.name);
                    System.Console.WriteLine(block.extractionStrategy.GetType());
                }

                if (selectedTest == "exit") {
                    return;
                }
            }
        }
    }
}
