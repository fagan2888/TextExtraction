using System;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Path = System.IO.Path;

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
            FindAndKillProcess(Path.GetFileNameWithoutExtension(path_));
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
        public bool isActive() {
            return FindAndKillProcess(Path.GetFileNameWithoutExtension(path_), false);
        }

        private bool FindAndKillProcess(string name, bool killFlag = true){
            foreach (Process clsProcess in Process.GetProcesses()){
                if (clsProcess.MainWindowTitle.Contains(name)){
                    if (killFlag) clsProcess.Kill();
                    return true;
                }
            }
            return false;
        }
    }
}