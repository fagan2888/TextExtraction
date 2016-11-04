using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TextExtration;
using TextExtration.ExtractionStrategy;
using TextExtration.TextObject;

namespace TextExtraction.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;) {
                System.Console.WriteLine();
                System.Console.WriteLine("select test:");
                System.Console.WriteLine("1. open and close doc text object");
                System.Console.WriteLine("2. open and close other doc text object");
                System.Console.WriteLine("3. Quit application and open doc text object");
                System.Console.WriteLine("4. attempt to reopen same doc ");
                System.Console.WriteLine("5. deserialize json extractionblock");
                System.Console.WriteLine("6. deserialize json extraction pipeline");
                System.Console.WriteLine("7. deserialize json contains check pipeline");
                System.Console.WriteLine("or type 'exit'");
                var selectedTest = System.Console.ReadLine();

                if (selectedTest == "1"){
                    //doc test scenario
                    //1. open and close doc text object
                    System.Console.WriteLine("set doctextobject");
                    System.Console.ReadKey();
                    DocTextObject docText1 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document.docx");

                    System.Console.WriteLine("docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();
                    
                    System.Console.WriteLine("docText1.close()");
                    System.Console.ReadKey();
                    docText1.close();

                    System.Console.WriteLine("test1 finished");
                }
                if (selectedTest == "2") {
                    //2. open and close other doc text object
                    System.Console.WriteLine("set doctextobject 1");
                    System.Console.ReadKey();
                    DocTextObject docText1 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document.docx");
                    
                    System.Console.WriteLine("set doctextobject 2");
                    System.Console.ReadKey();
                    DocTextObject docText2 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document2.docx");

                    System.Console.WriteLine("docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();
                    
                    System.Console.WriteLine("docText2.open()");
                    System.Console.ReadKey();
                    docText2.open();

                    System.Console.WriteLine("docText2.close()");
                    System.Console.ReadKey();
                    docText2.close();

                    System.Console.WriteLine("test2 finished");
                }
                if (selectedTest == "3") {
                    //2. open and close other doc text object
                    System.Console.WriteLine("set doctextobject 1");
                    System.Console.ReadKey();
                    DocTextObject docText1 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document.docx");

                    System.Console.WriteLine("set doctextobject 2");
                    System.Console.ReadKey();
                    DocTextObject docText2 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document2.docx");

                    System.Console.WriteLine("docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();

                    System.Console.WriteLine("quit application");
                    System.Console.ReadKey();
                    docText1.application().Quit();

                    System.Console.WriteLine("docText2.open()");
                    System.Console.ReadKey();
                    docText2.open();

                    System.Console.WriteLine("docText2.close()");
                    System.Console.ReadKey();
                    docText2.close();

                    System.Console.WriteLine("test3 finished");
                }
                if (selectedTest == "4")
                {
                    //2. open and close other doc text object
                    System.Console.WriteLine("set doctextobject 1");
                    System.Console.ReadKey();
                    DocTextObject docText1 = new DocTextObject($@"{Directory.GetCurrentDirectory()}\testdata\Test document.docx");

                    System.Console.WriteLine("docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();

                    System.Console.WriteLine("again docText1.open()");
                    System.Console.ReadKey();
                    docText1.open();

                    System.Console.WriteLine("docText1.close()");
                    System.Console.ReadKey();
                    docText1.close();

                    System.Console.WriteLine("test4 finished");
                }
                if (selectedTest == "5") {
                    System.Console.WriteLine("read testblock.json");
                    System.Console.ReadKey();
                    string json = File.ReadAllText("testdata\\testblock.json");

                    System.Console.WriteLine("set json deserialier");
                    System.Console.ReadKey();
                    var settings = new JsonSerializerSettings() {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    System.Console.WriteLine("deserialize json");
                    System.Console.ReadKey();
                    var block = JsonConvert.DeserializeObject<ExtractionBlock>(json, settings);

                    System.Console.WriteLine("print result");
                    System.Console.ReadKey();
                    System.Console.WriteLine($@"block.order: {block.order}");
                    System.Console.WriteLine($@"block.name: {block.name}" );
                    System.Console.WriteLine($@"block.extractionStrategy.getType(): {block.extractionStrategy.GetType()}");
                    var textPattern = (block.extractionStrategy as PatternMatchingExtraction).textPattern;
                    System.Console.WriteLine($@"textPattern.cutBegin: {textPattern.cutBegin}");
                    System.Console.WriteLine($@"textPattern.cutEnd: {textPattern.cutEnd}");
                    System.Console.WriteLine($@"textPattern.pattern: {textPattern.pattern}");
                }
                if (selectedTest == "6")
                {
                    System.Console.WriteLine("read testpipeline.json");
                    System.Console.ReadKey();
                    string json = File.ReadAllText("testdata\\testpipeline.json");

                    System.Console.WriteLine("set json deserialier");
                    System.Console.ReadKey();
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    System.Console.WriteLine("deserialize json");
                    System.Console.ReadKey();
                    var pipeline = JsonConvert.DeserializeObject<ExtractionPipeline>(json, settings);

                    System.Console.WriteLine("print result");
                    System.Console.ReadKey();
                    var extractionBlock2 = pipeline.extractionBlocks.ToList()[1];
                    var transformationBlock2 = pipeline.transformationBlocks.ToList()[1];
                    System.Console.WriteLine("extraction block: ");
                    System.Console.WriteLine($@"extraction block.order: {extractionBlock2.order}");
                    System.Console.WriteLine($@"extraction block.name: {extractionBlock2.name}");
                    System.Console.WriteLine($@"extraction block.extractionStrategy.getType(): {extractionBlock2.extractionStrategy.GetType()}");
                    var textPattern = (extractionBlock2.extractionStrategy as PatternMatchingExtraction).textPattern;
                    System.Console.WriteLine($@"extraction textPattern.cutBegin: {textPattern.cutBegin}");
                    System.Console.WriteLine($@"extraction textPattern.cutEnd: {textPattern.cutEnd}");
                    System.Console.WriteLine($@"extraction textPattern.pattern: {textPattern.pattern}");
                    System.Console.WriteLine();
                    System.Console.WriteLine("transformation block: ");
                    System.Console.WriteLine($@"transformation block.order: {transformationBlock2.order}");
                    System.Console.WriteLine($@"transformation block.name: {transformationBlock2.name}");
                    System.Console.WriteLine($@"transformation block.targetExtractionId: {transformationBlock2.targetExtractionId}");
                    System.Console.WriteLine($@"transformation block..extractionStrategy.GetType(): {transformationBlock2.transformationStrategy.GetType()}");
                }
                if (selectedTest == "7") {
                    System.Console.WriteLine("read testcheckpipeline.json");
                    System.Console.ReadKey();
                    string json = File.ReadAllText("testdata\\testcheckpipeline.json");

                    System.Console.WriteLine("set json deserialier");
                    System.Console.ReadKey();
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    System.Console.WriteLine("deserialize json");
                    System.Console.ReadKey();
                    var pipeline = JsonConvert.DeserializeObject<ContainsCheckPipeline>(json, settings);

                    System.Console.WriteLine("print result");
                    System.Console.ReadKey();
                    var checkBlock = pipeline.containsCheckBlocks.ToList()[1];
                    var textPattern = (checkBlock.extractionStrategy as PatternMatchingExtraction).textPattern;
                    System.Console.WriteLine($@"checkpipeline textPattern.cutBegin: {textPattern.cutBegin}");
                    System.Console.WriteLine($@"checkpipeline textPattern.cutEnd: {textPattern.cutEnd}");
                    System.Console.WriteLine($@"checkpipeline textPattern.pattern: {textPattern.pattern}");
                    System.Console.WriteLine();
                    System.Console.WriteLine($@"checkpipeline result: {pipeline.expectedResults.ToList()[1]}");
                    System.Console.WriteLine($@"checkpipeline findText: {checkBlock.findTarget}");
                }
                if (selectedTest == "exit") {
                    return;
                }
            }
        }
    }
}
