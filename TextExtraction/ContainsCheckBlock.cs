namespace TextExtration {
    public class ContainsCheckBlock {
        public ExtractionBlock extractionBlock { get; set; }
        public string findTarget { get; set; }
        
        public bool contains(ITextObject text) {
            var extractedTexts = extractionBlock.extract(text);
            foreach (var t in extractedTexts) {
                if (t.Contains(findTarget)) 
                    return true;       
            }

            return false;
        }
    }
}