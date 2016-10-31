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
            foreach (var originResult in originExtractionResults) {
                var transformStrategiesForCurrentOriginResult =
                    transformationBlocks.Where(b => b.targetExtractionId == originResult.id)
                        .Select(b => b.transformationStrategy).ToList();

                if (transformStrategiesForCurrentOriginResult.Count != 0) {
                    IEnumerable<string> tmpResult = originResult.result;
                    foreach (var strategy in transformStrategiesForCurrentOriginResult) {
                        tmpResult = strategy.transform(tmpResult);
                    }
                    yield return new ExtractionResult(originResult.id, originResult.name, tmpResult);
                }
            }
        }

        private static void blockValidation(
            IEnumerable<ExtractionBlock> extractionBlocks
            , IEnumerable<TransformationBlock> transformationBlocks) {
            foreach (var extractionBlock in extractionBlocks)
                extractionBlock.validateFields();

            foreach (var transformationBlock in transformationBlocks)
                transformationBlock.validateFields();
        }
    }
}