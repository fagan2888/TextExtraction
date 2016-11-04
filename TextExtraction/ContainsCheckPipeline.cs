using System;
using System.Collections.Generic;
using System.Linq;

namespace TextExtration {
    public class ContainsCheckPipeline {
        public IEnumerable<ContainsCheckBlock> containsCheckBlocks { get; set; }
        public IEnumerable<bool> expectedResults { get; set; }

        public ContainsCheckPipeline() {}
        public ContainsCheckPipeline(IEnumerable<ContainsCheckBlock> containsCheckBlocks, IEnumerable<bool> expectedResults) {
            this.containsCheckBlocks = containsCheckBlocks;
            this.expectedResults = expectedResults;
            lengthValidation(containsCheckBlocks, expectedResults);
        }
        
        public bool check(ITextObject text) {
            lengthValidation(containsCheckBlocks, expectedResults);
            var lstExpectedResults = expectedResults.ToList();
            foreach (var checkBlock in containsCheckBlocks.Select((v, i)=> new {value = v, index = i})) {
                if(checkBlock.value.contains(text) != lstExpectedResults[checkBlock.index])
                    return false;
            }

            return true;
        }

        private static void lengthValidation(IEnumerable<ContainsCheckBlock> containsCheckBlocks, IEnumerable<bool> expectedResults)
        {
            if (containsCheckBlocks.Count() != expectedResults.Count()){
                throw new ArgumentException(
                    message: "ContainsCheckPipeline: containsCheckBlocks and extectedResults must have same length");
            }
        }
    }
}