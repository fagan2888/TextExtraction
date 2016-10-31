using NUnit.Framework;
using TextExtration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using TextExtration.ExtractionStrategy;

namespace TextExtration.Tests
{
    [TestFixture()]
    public class ContainsCheckBlockTests
    {
        private ITextObject testTextObject_;

        [SetUp]
        public void setUp()
        {
            testTextObject_ = Substitute.For<ITextObject>();
            testTextObject_.text.Returns("one: star two: cow  three: dog four: QuestionMark"
                + "하나- 별, 둘- 소, 셋- 개, 넷- 물음표");
        }
        [Test()]
        public void containsTest()
        {
            var pattern = new TextExtractionPattern().withCutting("one", "two");
            var testExtractionBlock = new ExtractionBlock() {
                order = 1, name = "find star", extractionStrategy = new PatternMatchingExtraction(pattern)
            };

            var testContainsCheckBlock = new ContainsCheckBlock() {
                extractionBlock = testExtractionBlock,
                findTarget = "star"
            };

            var result = true;
            Assert.AreEqual(result, testContainsCheckBlock.contains(testTextObject_));
        }
    }
}