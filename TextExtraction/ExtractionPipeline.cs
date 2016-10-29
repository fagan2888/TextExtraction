using System;
using System.Collections.Generic;
using System.Linq;

namespace TextExtration
{
    public class ExtractionPipeline {
        public IEnumerable<IBlock> blocks { get; private set; }

        public ExtractionPipeline(IEnumerable<IBlock> blocks) {
            var someBlockHasNoName = blocks.Any(b=>b.name.Equals(string.Empty));
            if (someBlockHasNoName) {
                throw new ArgumentException(message: "block name should be given");
            }

            this.blocks = blocks.OrderBy(b=>b.order);
        }

        public List<string> extract(ITextObject text) {
            var extractedTexts = new List<string>();
            var tmpExtractedTexts = new List<string>();
            foreach (var block in blocks) {
                if (block is IExtractionBlock) {
                    
                }

                if (block is ITransformationBlock) {
                    tmpExtractedTexts = (block as ITransformationBlock).transform(tmpExtractedTexts).ToList();
                }

                extractedTexts.AddRange(tmpExtractedTexts);
            }

            return extractedTexts;
        }
    }
}