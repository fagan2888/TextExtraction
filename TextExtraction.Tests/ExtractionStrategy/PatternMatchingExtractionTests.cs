using NSubstitute;
using NUnit.Framework;
using TextExtration;
using TextExtration.ExtractionStrategy;

namespace TextExtraction.Tests.ExtractionStrategy
{
    [TestFixture()]
    public class PatternMatchingExtractionTests {
        public string testText =
            "this is test text. cutting before/ i want to find this text. /after cutting after cutting";
        
        [Test()]
        public void extractTest()
        {
            var pattern = new TextExtractionPattern();
            pattern.withCutting("cutting", "cutting");
            pattern.withLookAround("before/", "/after");
            pattern.withPattern(@"\s+?i want to find this text\.\s+?");
            var block = new PatternMatchingExtraction(pattern);

            var textObject = Substitute.For<ITextObject>();
            textObject.text.Returns(testText);

            var result = " i want to find this text. ";
            Assert.AreEqual(block.extract(textObject)[0], result);
        }
    }
}