using System.Collections.Generic;

namespace TextExtration {
    public class ContainsCheckPipeline {
        private readonly IDictionary<ContainsCheckBlock, bool> containsCheckBlocksAndExpectedResultDirDictionary_;

        public ContainsCheckPipeline(IDictionary<ContainsCheckBlock, bool> containsCheckBlocksAndExpectedResultDirDictionary) {
            containsCheckBlocksAndExpectedResultDirDictionary_ = containsCheckBlocksAndExpectedResultDirDictionary;
        }

        public bool check(ITextObject text) {
            foreach (var checkBlock in containsCheckBlocksAndExpectedResultDirDictionary_) {
                if(checkBlock.Key.contains(text) != checkBlock.Value)
                    return false;
            }

            return true;
        }
    }
}