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
        private List<ContainsCheckBlock> checkBlocks_;
        private List<bool> results_;
        private TextExtractionPattern pattern = new TextExtractionPattern().withCutting("one", "three");
        private IExtractionStrategy extractionStrategy_;
        public ContainsCheckPipelineTests() {
            extractionStrategy_ = new PatternMatchingExtraction(pattern);
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
            checkBlocks_ = new List<ContainsCheckBlock>();
            checkBlocks_.Add(new ContainsCheckBlock() {extractionStrategy = extractionStrategy_, findTarget = "star"});
            checkBlocks_.Add(new ContainsCheckBlock() { extractionStrategy = extractionStrategy_, findTarget = "cow" });
            
            results_ = new List<bool>();
            results_.Add(true);
            results_.Add(true);
            var pipeline = new ContainsCheckPipeline(checkBlocks_, results_);
            var result = true;

            Assert.AreEqual(result, pipeline.check(testTextObject_));

            checkBlocks_ = new List<ContainsCheckBlock>();
            checkBlocks_.Add(new ContainsCheckBlock() { extractionStrategy = extractionStrategy_, findTarget = "star" });
            checkBlocks_.Add(new ContainsCheckBlock() { extractionStrategy = extractionStrategy_, findTarget = "dog" });

            results_ = new List<bool>();
            results_.Add(true);
            results_.Add(false);
            Assert.AreEqual(result, pipeline.check(testTextObject_));
        }

        [Test()]
        public void checkTest_false()
        {
            checkBlocks_ = new List<ContainsCheckBlock>();
            checkBlocks_.Add(new ContainsCheckBlock() { extractionStrategy = extractionStrategy_, findTarget = "star" });
            checkBlocks_.Add(new ContainsCheckBlock() { extractionStrategy = extractionStrategy_, findTarget = "dog" });

            results_ = new List<bool>();
            results_.Add(true);
            results_.Add(true);
            var pipeline = new ContainsCheckPipeline(checkBlocks_, results_);
            var result = false;
            Assert.AreEqual(result, pipeline.check(testTextObject_));

        }
    }
}