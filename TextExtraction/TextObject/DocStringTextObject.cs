using System;
using NetOffice.WordApi;
using Utility.syonoki.ExtensionMethods;
using Document = Spire.Doc.Document;

namespace TextExtration.TextObject {
    public class DocStringTextObject:ITextObject {

        private readonly string path_;
        private string text_ = String.Empty;
        private readonly DocTextObject docTextObject_;

        public DocStringTextObject(string path) {
            path_ = path;
            docTextObject_ = new DocTextObject(path);
        }

        public void open()
        {
            try{
                docTextObject_.open();
            }
            catch (Exception e){
                throw new InvalidOperationException(message: $"DocStringTextObject: fail to open document {path_}\r\n{e.Message}");
            }
        }

        public void close() {
            docTextObject_.close();
        }

        public string path() => path_;

        public string text() {
            if (string.IsNullOrEmpty(text_)) {
                var document = new Document();
                document.LoadFromFile(path_);
                text_ = document.GetText().replace(new string[,] {{"\r\n", " "}, {"\t", " "} });
            }
            return text_;
        }

        public dynamic toObject() {
            if(!docTextObject_.isActive())
                docTextObject_.open();
            return docTextObject_.toObject();
        }

        public bool isActive() {
            return docTextObject_.isActive();
        }

        public Application application() => docTextObject_.application();
    }
}