using System;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TextExtration.TextObject {
    public class PdfTextObject:ITextObject {
        private string path_;
        private string text_ = String.Empty;

        public PdfTextObject(string path) {
            path_ = path;
        }

        public void open() {
            ProcessStartInfo info = new ProcessStartInfo(path_);
            info.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(info);
        }

        public void close() {
            throw new System.NotImplementedException(message: "cannot close pdf process");
        }

        public string path() => path_;

        public string text() {
            if (string.IsNullOrEmpty(text_)) {

                using (PdfReader reader = new PdfReader(path_)){
                    for (int i = 0; i < reader.NumberOfPages; i++){
                        text_ += PdfTextExtractor.GetTextFromPage(reader, i + 1);
                    }
                }
                text_ = text_.Replace("\n", "");
            }
            return text_;
        }

        public dynamic toObject() => new PdfReader(path_);
    }
}