namespace TextExtration.ExtractionStrategy {
    public class TextExtractionPattern {
        public string cutBegin { get; private set; } = string.Empty;
        public string cutEnd { get; private set; } = string.Empty;
        public int cutIndex { get; private set; } = 0;
        public string lookBehind { get; private set; } = string.Empty;
        public string lookAhead { get; private set; } = string.Empty;
        public string pattern { get; private set; } = string.Empty;

        public TextExtractionPattern withCutting(string cutBegin, string cutEnd, int cutIndex = 0) {
            this.cutBegin = cutBegin;
            this.cutEnd = cutEnd;
            this.cutIndex = cutIndex;

            return this;
        }

        public TextExtractionPattern withLookAround(string lookBehind, string lookAhead) {
            this.lookBehind = lookBehind;
            this.lookAhead = lookAhead;

            return this;
        }

        public TextExtractionPattern withPattern(string pattern) {
            this.pattern = pattern;

            return this;
        }

        public bool isCuttable() => cutBegin != string.Empty && cutBegin != "";
        public bool hasLookAround() => !(lookBehind == string.Empty && lookAhead == string.Empty);
        public bool hasPattern() => pattern != string.Empty && pattern != "";
    }
}