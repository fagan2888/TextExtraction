using System.Collections.Generic;

namespace TextExtration {
    public interface ITransformationStrategy {
        IEnumerable<string> transform(IEnumerable<string> target);
    }
}