using System;
using System.Collections.Generic;

namespace TextExtration {
    public class TransformationBlock {
        public int order { get; set; }
        public string name { get; set; }
        public int targetExtractionId { get; set; }
        public ITransformationStrategy transformationStrategy { get; set; }

        public IEnumerable<string> transform(IEnumerable<string> target) {
            return transformationStrategy.transform(target);
        }
        
        public bool validateFields()
        {
            if (name == string.Empty) { throw new NotSupportedException(message: "TransformationBlock error: name should be given"); }
            if (transformationStrategy == null) { throw new NotSupportedException(message: "TransformationBlock error: transformation strategy should be given"); }

            return true;
        }
    }
}
