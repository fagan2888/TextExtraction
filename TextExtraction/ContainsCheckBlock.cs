using System;

namespace TextExtration {
    public class ContainsCheckBlock {
        public IExtractionStrategy extractionStrategy { get; set; }
        public string findTarget { get; set; }

        public bool contains(ITextObject text) {
            try {
                var extractedTexts = extractionStrategy.extract(text);
                foreach (var t in extractedTexts){
                    if (t.Contains(findTarget))
                        return true;
                }

                return false;
            }
            catch (Exception e) {
                if (e.Message.Contains("PatternMatchingExtraction.cutText")) {
                    return false;
                }
                else {
                    throw e;
                }
            }
        }
    }
}