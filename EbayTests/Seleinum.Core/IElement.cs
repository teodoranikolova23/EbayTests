namespace SeleniumTests.Seleinum.Core
{
    public interface IElement
    {
        void Click();
        void TypeText(string text);
        void Clear();
        string Text { get; }
        bool Displayed { get; }
        bool Enabled { get; }
        string ElementName { get; }
        bool IsDisplayed(int? timeOut = null);
    }
}
