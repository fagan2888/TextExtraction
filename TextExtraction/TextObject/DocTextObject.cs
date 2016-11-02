using System;
using Microsoft.Office.Interop.Word;

namespace TextExtration.TextObject {
    public class DocTextObject : ITextObject {

        private static Application application { get; set; }
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

                currentDocTextObject_?.close();

                doc_ = application.Documents.Open(path_);
                currentDocTextObject_ = this;
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

        private void setApplication() {
            application = new Application() { Visible = true };
            applicationActiveState_ = true;
            ((ApplicationEvents4_Event)application).Quit
                += new ApplicationEvents4_QuitEventHandler(() => { applicationActiveState_ = false; });
            currentDocTextObject_ = null;
        }
        
    }
}
