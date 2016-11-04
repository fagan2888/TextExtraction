using System;
using NetOffice.WordApi;

namespace TextExtration.TextObject {
    public class DocTextObject : ITextObject {

        private static Application application_;
        private static bool applicationActiveState_;
        private static DocTextObject currentDocTextObject_;

        private Document doc_;
        private string path_;
        private string text_ = String.Empty;

        public DocTextObject(string path) {
            path_ = path;
        }

        public void open() {
            try {
                if (!applicationActiveState_) 
                    setApplication();

                if (currentDocTextObject_ != this) {
                    currentDocTextObject_?.close();

                    doc_ = application_.Documents.Open(path_);
                    currentDocTextObject_ = this;
                }
            }
            catch (Exception e) {
                throw new InvalidOperationException(message:$"DocTextObject: fail to open document {path_}\r\n{e.Message}");
            }
        }

        public void close() {
            if (currentDocTextObject_ == this) currentDocTextObject_ = null;

            doc_.Close();
            doc_ = null;
        }

        public string path() => path_;
        
        public string text() {
            
            if (string.IsNullOrEmpty(text_)) {
                if (doc_ == null) open();
                text_ = doc_.Range().Text;
            }

            return text_;
        }

        public dynamic toObject() => doc_;

        public Application application() => application_;

        private void setApplication() {
            application_ = new Application() { Visible = true };
            applicationActiveState_ = true;
            application_.QuitEvent+= () => { applicationActiveState_ = false; };
            currentDocTextObject_ = null;
        }
        

    }
}
