using OpenQA.Selenium;
using SeleniumTests.Seleinum.Core;

namespace Selenium.Core.Interfaces
{
    public interface IElementsList : IEnumerable<IElement>
    {
        By By { get; }

        int Count { get; }

        IElement this[int i] { get; }

        void ForEach(Action<IElement> action);
    }
}
