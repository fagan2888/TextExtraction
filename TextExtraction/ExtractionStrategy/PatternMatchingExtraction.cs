using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextExtration.ExtractionStrategy {
    public class PatternMatchingExtraction : IExtractionStrategy {

        public TextExtractionPattern textPattern { get; set; }

        public PatternMatchingExtraction(TextExtractionPattern textPattern) {
            this.textPattern = textPattern;
        }

        public List<string> extract(ITextObject textObject) {

            string cuttedText = cutText(textObject.text());
            string pattern = patternWithLookAround();

            MatchCollection match;
            try {
                match = Regex.Matches(cuttedText, pattern);
            }
            catch (Exception e) {
                throw new InvalidOperationException("PatternMatchingExtraction.cutText: text matching failed\r\n" + e.Message);
            }

            return matchToStringList(match).ToList();
        }

        private string cutText(string text){
            try{
                if (textPattern.isCuttable())
                    return Regex.Matches(text, $@"(?<={textPattern.cutBegin})(.+|\d+|\d+\.\d+)(?={textPattern.cutEnd})")[
                            textPattern.cutIndex].Value;
                return text;
            }
            catch (System.Exception e){
                throw new InvalidOperationException("PatternMatchingExtraction.cutText: text cutting error\r\n" + e.Message);
            }

        }

        private string patternWithLookAround(){
            var beginPattern = string.IsNullOrEmpty(textPattern.lookBehind)
                ? string.Empty
                : $"(?<={textPattern.lookBehind})";
            var endPattern = string.IsNullOrEmpty(textPattern.lookAhead)
                ? string.Empty
                : $"(?={textPattern.lookAhead})";
            var pattern = textPattern.hasPattern()
                ? textPattern.pattern
                : @"(.+|\d+|\d+\.\d+)";

            return $"{beginPattern}{pattern}{endPattern}";
        }

        private IEnumerable<string> matchToStringList(MatchCollection match){
            foreach (Match m in match)
                yield return m.Value;
        }
        
    }
}