using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TextExtration.ExtractionStrategy;
using TextExtration.TransformationStrategy;

namespace TextExtration.Tests
{
    [TestFixture()]
    public class ExtractionPipelineTests {
        private ITextObject testTextObject_;

        [SetUp]
        public void setUp() {
            testTextObject_ = Substitute.For<ITextObject>();
            testTextObject_.text.Returns("one: star \r\n two: cow \r\n three: dog \r\n four: QuestionMark"
                + "하나- 별, 둘- 소, 셋- 개, 넷- 물음표");
        }

        [Test()]
        public void extractTest() {
            var pattern1 = new TextExtractionPattern().withLookAround(@"\: ", string.Empty).withPattern(@"\w+ \r\n");
            var pattern2 = new TextExtractionPattern().withLookAround(@"\-", string.Empty).withPattern(@"\s[가-힣]+");

            var testExtractionBlocks = new List<ExtractionBlock>() {
                new ExtractionBlock() { order = 1, name = "english", extractionStrategy = new PatternMatchingExtraction(pattern1)}
                , new ExtractionBlock() {order = 2, name = "korean", extractionStrategy = new PatternMatchingExtraction(pattern2)}
            };

            var testTransformationBlocks = new List<TransformationBlock>() {
                new TransformationBlock() {
                    order = 1,
                    name = "remove specialized character",
                    targetExtractionId = 1,
                    transformationStrategy = new RemoveSpecializedSymbol()
                },
                new TransformationBlock() {
                    order = 2,
                    name = "remove gap of english",
                    targetExtractionId = 1,
                    transformationStrategy = new RemoveGap()
                },
                new TransformationBlock() {
                    order = 3,
                    name = "remove gap of korean",
                    targetExtractionId = 2,
                    transformationStrategy = new RemoveGap()
                }
            };

            var pipeline = new ExtractionPipeline(testExtractionBlocks, testTransformationBlocks);
            var extractedResult = pipeline.extract(testTextObject_);
            var target1 = extractedResult.First(r => r.id == 1);
            var target2 = extractedResult.First(r => r.id == 2);

            var result1 = new ExtractionResult(1, "english", new List<string>() {"star", "cow", "dog"});
            var result2 = new ExtractionResult(2, "korean", new List<string>() { "별", "소", "개", "물음표" });

            Assert.AreEqual(target1.id, result1.id);
            Assert.AreEqual(target1.name, result1.name);
            CollectionAssert.AreEqual(target1.result.ToList(), result1.result);
            Assert.AreEqual(target2.id, result2.id);
            Assert.AreEqual(target2.name, result2.name);
            CollectionAssert.AreEqual(target2.result.ToList(), result2.result);
        }

        [Test()]
        public void extract_InvalidStrategy_ThrowException() {
            var pattern1 = new TextExtractionPattern().withLookAround(@"\: ", string.Empty).withPattern(@"\w+ \r\n");

            var testExtractionBlocks = new List<ExtractionBlock>() {
                new ExtractionBlock() { order = 1, name = "english", extractionStrategy = new PatternMatchingExtraction(pattern1)}
            };

            var testTransformationBlocks = new List<TransformationBlock>() {
               new TransformationBlock() {
                order = 1,
                targetExtractionId = 1}
            };

            var ex = Assert.Throws<NotSupportedException>( ()=> new ExtractionPipeline(testExtractionBlocks, testTransformationBlocks));
            Assert.IsTrue(ex.Message.Contains("TransformationBlock error: transformation strategy should be given"));
        }
        
    }
}