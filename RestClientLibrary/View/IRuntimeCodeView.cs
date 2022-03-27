namespace RestClientLibrary.View
{
    using DataLibrary;

    public interface IRuntimeCodeView : IBaseView
    {
        void ViewCodePreview(string code);
    }
}
