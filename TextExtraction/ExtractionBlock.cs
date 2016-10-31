using System;
using System.Collections.Generic;

namespace TextExtration {
    public class ExtractionBlock {
        public int order { get; set; }
        public string name { get; set; }
        public IExtractionStrategy extractionStrategy { get; set; }

        public List<string> extract(ITextObject textObject) {
            return extractionStrategy.extract(textObject);
        }

        public bool validateFields() {
            if (name == string.Empty) {throw new NotSupportedException(message: "ExtractionBlock error: name should be given");}
            if (extractionStrategy == null) { throw new NotSupportedException(message: "ExtractionBlock error: extraction strategy should be given"); }

            return true;
        }
    }
}