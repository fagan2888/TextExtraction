using System.Collections.Generic;

namespace TextExtration {
    public class ExtractionResult {
        public int id { get; private set; }
        public string name { get; private set; }
        public IEnumerable<string> result { get; private set; }

        public ExtractionResult(int id, string name, IEnumerable<string> result) {
            this.id = id;
            this.name = name;
            this.result = result;
        }
    }
}