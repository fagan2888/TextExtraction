using System.Collections.Generic;
using NUnit.Framework;
using TextExtration.TransformationStrategy;

namespace TextExtraction.Tests.TransformationStrategy
{
    [TestFixture()]
    public class RemoveGapTests
    {
        private RemoveGap strategy_;
        [SetUp]
        public void setUp()
        {
            strategy_ = new RemoveGap();
        }
        [Test()]
        public void transformTest()
        {
            List<string> testList = new List<string>() { " 32", " ffe", " fewa", " ffe " };
            var result = new List<string>() { "32", "ffe", "fewa", "ffe" };
            Assert.AreEqual(strategy_.transform(testList), result);
        }
    }
}