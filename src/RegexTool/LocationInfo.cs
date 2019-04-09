namespace RegexTool
{
    public class LoadResult
    {
        public string FileOrUrl { get; private set; }
        public string Text { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public LoadResult(string fileOrUrl, string text)
            : this(fileOrUrl, text, true, string.Empty)
        {
        }

        public LoadResult(string fileOrUrl, string text, bool isSuccess, string errorMsg)
        {
            this.FileOrUrl = fileOrUrl;
            IsSuccess = isSuccess;
            Text = text;
            Error = errorMsg;
        }
    }
}