using System.Collections.Generic;
using System.Linq;
using Utility.syonoki.ExtensionMethods;

namespace TextExtration.TransformationStrategy {
    public class RemoveSpecializedSymbol:ITransformationStrategy {
        public IEnumerable<string> transform(IEnumerable<string> target) 
            => target.Select(s => StringExtension.replace(s, new string[,] { { ":", "" }, { "\r", "" }, { "\n", "" }, { ",", "" } }));
    }
}