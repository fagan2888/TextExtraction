using NUnit.Framework;
using System.Collections.Generic;

namespace TextExtration.TransformationBlock.Tests
{
    [TestFixture()]
    public class RemoveSpecializedSymbolBlockTests {
        private RemoveSpecializedSymbolBlock block_;
        [SetUp]
        public void setUp() {
            block_ = new RemoveSpecializedSymbolBlock();
        }
        [Test()]
        public void transformTest()
        {
            List<string> testList = new List<string>() {":32", "\rffe", "\r\nfewa", ",ffe,"};
            var result = new List<string>() {"32", "ffe", "fewa", "ffe"};
            Assert.AreEqual(block_.transform(testList), result);
        }
    }
}