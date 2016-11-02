namespace TextExtration {
    public interface ITextObject {
        void open();
        void close();
        string path();
        string text();
        dynamic toObject();
    }
}