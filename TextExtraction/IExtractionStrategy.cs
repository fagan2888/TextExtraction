using System.Collections.Generic;

namespace TextExtration {
    public interface IExtractionStrategy {
        List<string> extract(ITextObject textObject);
    }
}