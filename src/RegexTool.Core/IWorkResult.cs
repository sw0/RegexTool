using System.Text.RegularExpressions;
namespace RegexTool.Core
{
    public interface IWorkResult<T> where T : class
    {
        Regex RegexObj { get; }
        T Data { get; }
        bool IsSuccess { get; }
        string ErrorMessage { get; }
    }
}