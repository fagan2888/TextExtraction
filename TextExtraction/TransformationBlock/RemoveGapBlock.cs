using System.Collections.Generic;
using System.Linq;
using Utility.syonoki.ExtensionMethods;

namespace TextExtration.TransformationBlock {
    public class RemoveGapBlock : ITransformationBlock {
        public int order { get; set; }
        public string name { get; set; }

        public IEnumerable<string> transform(IEnumerable<string> target) 
            => target.Select(s=> StringExtension.removeGap(s));
    }
}