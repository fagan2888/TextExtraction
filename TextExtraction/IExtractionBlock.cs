using System.Collections.Generic;

namespace TextExtration {
    public interface IExtractionBlock:IBlock {
        List<string> extract(ITextObject textObject);
    }
}