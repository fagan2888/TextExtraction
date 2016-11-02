using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using TextExtration.ExtractionStrategy;

namespace TextExtration.Tests
{
    [TestFixture()]
    public class ContainsCheckPipelineTests
    {
        private ITextObject testTextObject_;
        private Dictionary<ContainsCheckBlock, bool> blockResultDictionary_ = new Dictionary<ContainsCheckBlock, bool>();
        private readonly ExtractionBlock testExtractionBlock1_;
        private readonly ExtractionBlock testExtractionBlock2_;

        public ContainsCheckPipelineTests() {
            var pattern = new TextExtractionPattern().withCutting("one", "three");
            testExtractionBlock1_ = new ExtractionBlock()
            {
                order = 1,
                name = "find star",
                extractionStrategy = new PatternMatchingExtraction(pattern)
            };
            testExtractionBlock2_ = new ExtractionBlock()
            {
                order = 2,
                name = "find cow",
                extractionStrategy = new PatternMatchingExtraction(pattern)
            };
        }

        [SetUp]
        public void setUp()
        {
            testTextObject_ = Substitute.For<ITextObject>();
            testTextObject_.text().Returns("one: star two: cow  three: dog four: QuestionMark"
                + "하나- 별, 둘- 소, 셋- 개, 넷- 물음표");
        }
        [Test()]
        public void checkTest_true() {
            blockResultDictionary_ = new Dictionary<ContainsCheckBlock, bool>();
            blockResultDictionary_.Add(new ContainsCheckBlock() {extractionBlock = testExtractionBlock1_, findTarget = "star"}, true);
            blockResultDictionary_.Add(new ContainsCheckBlock() { extractionBlock = testExtractionBlock2_, findTarget = "cow" }, true);
            
            var pipeline = new ContainsCheckPipeline(blockResultDictionary_);
            var result = true;

            Assert.AreEqual(result, pipeline.check(testTextObject_));

            blockResultDictionary_ = new Dictionary<ContainsCheckBlock, bool>();
            blockResultDictionary_.Add(new ContainsCheckBlock() { extractionBlock = testExtractionBlock1_, findTarget = "star" }, true);
            blockResultDictionary_.Add(new ContainsCheckBlock() { extractionBlock = testExtractionBlock2_, findTarget = "dog" }, false);

            Assert.AreEqual(result, pipeline.check(testTextObject_));
        }

        [Test()]
        public void checkTest_false()
        {
            blockResultDictionary_ = new Dictionary<ContainsCheckBlock, bool>();
            blockResultDictionary_.Add(new ContainsCheckBlock() { extractionBlock = testExtractionBlock1_, findTarget = "star" }, true);
            blockResultDictionary_.Add(new ContainsCheckBlock() { extractionBlock = testExtractionBlock2_, findTarget = "dog" }, true);

            var pipeline = new ContainsCheckPipeline(blockResultDictionary_);
            var result = false;
            Assert.AreEqual(result, pipeline.check(testTextObject_));

        }
    }
}