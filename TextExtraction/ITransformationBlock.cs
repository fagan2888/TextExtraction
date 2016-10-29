using System.Collections.Generic;

namespace TextExtration {
    public interface ITransformationBlock:IBlock {
        IEnumerable<string> transform(IEnumerable<string> target);
    }
}
