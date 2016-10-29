using System.Collections.Generic;
using System.Linq;
using Utility.syonoki.ExtensionMethods;

namespace TextExtration.TransformationBlock {
    public class RemoveSpecializedSymbolBlock : ITransformationBlock {
        public int order { get; set; }
        public string name { get; set; }

        public IEnumerable<string> transform(IEnumerable<string> target) 
            => target.Select(s => StringExtension.replace(s, new string[,] { { ":", "" }, { "\r", "" }, { "\n", "" }, { ",", "" } }));
    }
}