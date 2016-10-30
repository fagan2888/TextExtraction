using System.Collections.Generic;
using NUnit.Framework;
using TextExtration.TransformationStrategy;

namespace TextExtraction.Tests.TransformationStrategy
{
    [TestFixture()]
    public class RemoveSpecializedSymbolTests {
        private RemoveSpecializedSymbol stretegy_;
        [SetUp]
        public void setUp() {
            stretegy_ = new RemoveSpecializedSymbol();
        }
        [Test()]
        public void transformTest()
        {
            List<string> testList = new List<string>() {":32", "\rffe", "\r\nfewa", ",ffe,"};
            var result = new List<string>() {"32", "ffe", "fewa", "ffe"};
            Assert.AreEqual(stretegy_.transform(testList), result);
        }
    }
}