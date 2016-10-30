using System;
using System.Collections.Generic;
using System.Linq;

namespace TextExtration
{
    public class ExtractionPipeline {
        public IEnumerable<ExtractionBlock> extractionBlocks { get; private set; }
        public IEnumerable<TransformationBlock> transformationBlocks { get; private set; }
        
        public ExtractionPipeline(IEnumerable<ExtractionBlock> extractionBlocks, IEnumerable<TransformationBlock> transformationBlocks) {
            blockValidation(extractionBlocks, transformationBlocks);

            this.extractionBlocks = extractionBlocks.OrderBy(b=>b.order);
            this.transformationBlocks = transformationBlocks.OrderBy(b => b.order);
        }

        public IEnumerable<ExtractionResult> extract(ITextObject text) {
            IEnumerable<ExtractionResult> originExtractionResults = performExtractionBlock(text);
            IEnumerable<ExtractionResult> results = performTransformationBlock(originExtractionResults);
            return results;
        }

        private IEnumerable<ExtractionResult> performExtractionBlock(ITextObject text) {
            foreach (var extractionBlock in extractionBlocks) {
                var id = extractionBlock.order;
                var name = extractionBlock.name;
                var result = extractionBlock.extract(text);
                yield return new ExtractionResult(id, name, result);
            }
        }
        
        private IEnumerable<ExtractionResult> performTransformationBlock(IEnumerable<ExtractionResult> originExtractionResults)
        {
            foreach (var transformationBlock in transformationBlocks) {
                var target = originExtractionResults.FirstOrDefault(r => r.id == transformationBlock.targetExtractionId);
                if (target == null) 
                    throw new NullReferenceException("ExtractionPipeline_performTransformationBlock: tartget extraction id not founded");       

                yield return new ExtractionResult(target.id, target.name, transformationBlock.transform(target.result));
            }
        }

        private static void blockValidation(IEnumerable<ExtractionBlock> extractionBlocks, IEnumerable<TransformationBlock> transformationBlocks) {
            foreach (var extractionBlock in extractionBlocks)
                extractionBlock.validateFields();

            foreach (var transformationBlock in transformationBlocks)
                transformationBlock.validateFields();
        }
    }
}