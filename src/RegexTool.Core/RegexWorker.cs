using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTool.Core
{
    public class RegexValidationResult
    {
        public bool IsValid { get; set; }
        public string InvalidReason { get; set; }
    }

    public interface IRegexWorker
    {
        IRegexBasicInfo RegexBasicInfo { get; }

        IWorkResult<MatchCollection> Excute();
    }

    public interface IRegexMatchWorker
    {
        IRegexBasicInfo RegexBasicInfo { get; }

        IWorkResult<MatchCollection> ExcuteMatches();

        IWorkResult<Match> ExcuteMatch();
    }

    public interface IRegexReplaceWorker
    {
        IRegexReplaceInfo RegexBasicInfo { get; }

        IWorkResult<string> ExecuteReplace();
    }

    public interface IResultProcessor
    {
        IWorkResult<string> Process(MatchCollection matches);
    }


    public class RegexBaseWorker
    {
        protected readonly IRegexBasicInfo _regexInfo = null;

        protected IWorkResult<MatchCollection> GetMatches()
        {
            var validation = _regexInfo.Validate();

            if (validation.IsValid == false)
            {
                return new RegexWorkResult<MatchCollection>(null, _regexInfo.RegexObj, false, validation.InvalidReason);
            }
            try
            {
                var mc = _regexInfo.RegexObj.Matches(_regexInfo.Input);

                return new RegexWorkResult<MatchCollection>(mc, _regexInfo.RegexObj);
            }
            catch (Exception ex)
            {
                return new RegexWorkResult<MatchCollection>(null, _regexInfo.RegexObj, false, ex.Message);
            }
        }

        public RegexBaseWorker(IRegexBasicInfo model)
        {
            this._regexInfo = model;

            if (model == null)
                throw new ArgumentNullException("model");
        }
    }

    public class TemplateResultParameter
    {
        public OrderInfo OrderInfo { get; set; }
        public BatchInfo BatchInfo { get; set; }
        public string[] RowNumberDelimeters { get; set; }
        public string RowNumFormat { get; set; }
        public string RowNumPlaceHolder { get; set; }

        //public string Format { get; set; }

        public TemplateResultParameter()
        {
        }

        public TemplateResultParameter(TemplateParameters tp, string template)
        {
            //var oi = new OrderInfo() { Group = "$0", OrderType = OrderOption.Asc };
            this.OrderInfo = tp.OrderInfo ?? new OrderInfo() { OrderType = OrderOption.None };
            this.BatchInfo = tp.BatchInfo;
            this.RowNumberDelimeters = tp.RowNumberDelimiter;

            AnalyzeBemplate(template);
        }

        private void AnalyzeBemplate(string template)
        {
            if (string.IsNullOrEmpty(template)) return;
            try
            {
                string rowNumPlaceHolder = string.Format("{0}({1})(:((?!{2}).)+)?{2}",
                    Regex.Escape(this.RowNumberDelimeters[0]),
                    TemplateParameters.STR_ROWNUM_TEXT,
                    Regex.Escape(this.RowNumberDelimeters[1]));
                Debug.WriteLine(rowNumPlaceHolder);
                Regex reg = new Regex(rowNumPlaceHolder);

                var m = reg.Match(template);


                if (m.Success)
                {
                    this.RowNumPlaceHolder = m.Groups[0].Value;
                    this.RowNumFormat = string.Format("{{0{0}}}", m.Groups[2].Value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HasRowNumberSpaceHolder: " + ex.Message);
            }
        }
    }

    public class TemplateResult
    {
        public string Result { get; set; }

        private int _duplicated = -1;

        public int Duplicated
        {
            get { return _duplicated; }
            set { _duplicated = value; }
        }

        public int ItemsCount { get; set; }

        public int ItemsOringinalCount { get; set; }
    }


    public class RegexWorker : IRegexMatchWorker, IRegexReplaceWorker
    {
        private readonly IRegexFullInfo _regexFullInfo;

        public IRegexBasicInfo RegexBasicInfo
        {
            get
            {
                return _regexFullInfo;
            }
        }

        IRegexReplaceInfo IRegexReplaceWorker.RegexBasicInfo
        {
            get
            {
                return _regexFullInfo;
            }
        }

        //TODO REG_NUMBER is not a precise regex for checking numbers, however, it should works in most cases.
        private static readonly Regex REG_NUMBER = new Regex("^\\d{1,18}$", RegexOptions.Compiled);

        protected IWorkResult<Match> _matchResult = null;
        protected IWorkResult<MatchCollection> _matchedResult = null;

        protected IWorkResult<MatchCollection> GetMatchCollection()
        {
            var validation = _regexFullInfo.Validate();

            if (validation.IsValid == false)
            {
                return new RegexWorkResult<MatchCollection>(null, _regexFullInfo.RegexObj, false, validation.InvalidReason);
            }
            try
            {
                var mc = _regexFullInfo.RegexObj.Matches(_regexFullInfo.Input);

                return new RegexWorkResult<MatchCollection>(mc, _regexFullInfo.RegexObj);
            }
            catch (Exception ex)
            {
                return new RegexWorkResult<MatchCollection>(null, _regexFullInfo.RegexObj, false, ex.Message);
            }
        }

        protected IWorkResult<Match> GetMatch()
        {
            var validation = _regexFullInfo.Validate();

            if (validation.IsValid == false)
            {
                return new RegexWorkResult<Match>(null, _regexFullInfo.RegexObj, false, validation.InvalidReason);
            }
            try
            {
                var mc = _regexFullInfo.RegexObj.Match(_regexFullInfo.Input);

                return new RegexWorkResult<Match>(mc, _regexFullInfo.RegexObj);
            }
            catch (Exception ex)
            {
                return new RegexWorkResult<Match>(null, _regexFullInfo.RegexObj, false, ex.Message);
            }
        }


        public IWorkResult<MatchCollection> ExcuteMatches()
        {
            if (_matchedResult != null)
                return _matchedResult;

            _matchedResult = GetMatchCollection();

            return _matchedResult;
        }

        public IWorkResult<Match> ExcuteMatch()
        {
            if (_matchResult != null)
                return _matchResult;

            _matchedResult = GetMatchCollection();

            return _matchResult;
        }

        public IWorkResult<string> ExecuteReplace()
        {
            var validation = _regexFullInfo.Validate();

            if (validation.IsValid == false
                || (string.IsNullOrEmpty(_regexFullInfo.Replacement) && false == _regexFullInfo.AllEmpty))
            {
                return new RegexWorkResult<string>(null, _regexFullInfo.RegexObj, false, validation.InvalidReason);
            }
            try
            {
                var replaceResult = _regexFullInfo.RegexObj.Replace(_regexFullInfo.Input, _regexFullInfo.Replacement ?? string.Empty);

                return new RegexWorkResult<string>(replaceResult, _regexFullInfo.RegexObj);
            }
            catch (Exception ex)
            {
                return new RegexWorkResult<string>(null, _regexFullInfo.RegexObj, false, ex.Message);
            }
        }

        private bool IsNumber(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            else return REG_NUMBER.IsMatch(str);
        }

        public IWorkResult<TemplateResult> ExecuteTemplate()
        {
            var validation = _regexFullInfo.Validate();

            if (validation.IsValid == false)
            {
                return new RegexWorkResult<TemplateResult>(null, _regexFullInfo.RegexObj, false, validation.InvalidReason);
            }
            try
            {
                if (string.IsNullOrWhiteSpace(_regexFullInfo.Template))
                    return new RegexWorkResult<TemplateResult>(null, _regexFullInfo.RegexObj);

                _matchedResult = _matchedResult ?? GetMatchCollection();

                var triEqualityComparer = new TemplateResultItemEqualityComparer();

                if (_matchedResult.IsSuccess)
                {
                    //List<string> items = new List<string>();
                    var items = new List<TemplateResultItem>();

                    var dic = new Dictionary<string, int>();

                    int duplicated = 0;

                    StringBuilder sb = new StringBuilder();

                    var trp = new TemplateResultParameter(_regexFullInfo.TplParameters, _regexFullInfo.Template);

                    //bool gotRowNumberHolder = _regexFullInfo.Template

                    foreach (Match match in _matchedResult.Data)
                    {
                        var str = _regexFullInfo.RegexObj.Replace(match.Value, _regexFullInfo.Template);

                        string str2 = string.Empty;

                        if (trp.OrderInfo.OrderType != OrderOption.None)
                        {
                            if (trp.OrderInfo.GroupForSort == _regexFullInfo.Template)
                            {
                                str2 = str;
                            }
                            else
                            {
                                str2 = _regexFullInfo.RegexObj.Replace(match.Value, trp.OrderInfo.GroupForSort);
                            }
                        }

                        if (dic.ContainsKey(str))
                        {
                            if (false == _regexFullInfo.TplParameters.IgnoreDuplicated)
                            {
                                //items.Add(str);
                                items.Add(new TemplateResultItem(str, str2));
                            }
                            dic[str]++;
                        }
                        else
                        {
                            //items.Add(str);
                            items.Add(new TemplateResultItem(str, str2));
                            dic.Add(str, 1);
                        }
                    }

                    if (_regexFullInfo.TplParameters.ShowDuplicatedOnly)
                    {
                        var kx = from item in items
                                 join item2 in dic.Where(x => x.Value > 1).Select(kv => kv.Key).ToList() on item.Line equals item2
                                 select item;

                        if (_regexFullInfo.TplParameters.IgnoreDuplicated)
                        {
                            kx = kx.Distinct(triEqualityComparer);
                        }

                        GetTemplateResultString(kx.ToList(), trp, sb);
                    }
                    else
                    {
                        if (_regexFullInfo.TplParameters.IgnoreDuplicated)
                        {
                            //dic.Select(kv => kv.Key).ToList().ForEach(s => sb.Append(s));
                            var ky = items.Distinct(triEqualityComparer);

                            GetTemplateResultString(items, trp, sb);
                        }
                        else
                        {
                            GetTemplateResultString(items, trp, sb);

                            //items.ForEach(s => sb.Append(s));
                        }
                    }

                    var result = sb.ToString();

                    return new RegexWorkResult<TemplateResult>(
                        new TemplateResult
                        {
                            Result = result,
                            ItemsOringinalCount = _matchedResult.Data.Count,
                            Duplicated = duplicated,
                            ItemsCount = _matchedResult.Data.Count - duplicated
                        },
                        _regexFullInfo.RegexObj);
                }

                return new RegexWorkResult<TemplateResult>(
                    new TemplateResult
                    {
                        Result = string.Empty,
                        ItemsOringinalCount = -1,
                        Duplicated = -1,
                        ItemsCount = -1
                    },
                    _regexFullInfo.RegexObj);
            }
            catch (Exception ex)
            {
                return new RegexWorkResult<TemplateResult>(null, _regexFullInfo.RegexObj, false, ex.Message);
            }
        }

        private void GetTemplateResultString(List<TemplateResultItem> items,
            TemplateResultParameter trp,
            StringBuilder sb)
        {
            if (trp.OrderInfo.OrderType == OrderOption.Asc)
            {
                if (trp.OrderInfo.AutoTurnDigitsToNumber && items.All(f => IsNumber(f.DataForSort)))
                {
                    var x = from item in items
                            orderby Convert.ToInt64(item.DataForSort)
                            select item;
                    BuildTemplateResult(x, trp, sb);
                }
                else
                {
                    var x = from item in items
                            orderby item.DataForSort
                            select item;
                    BuildTemplateResult(x, trp, sb);

                    //TODO do we need to return some message?
                    //if (oi.AutoTurnDigitsToNumber)
                    //{
                    //    sb.AppendLine("\r\n")
                    //        .Append("WARNING: NOT ALL STRING CAN BE SUCCESSFULLY TREATED AS A NUMBER, SO SORT IN ASCII INSTEAD OF NUMBER");
                    //}
                }

            }
            else if (trp.OrderInfo.OrderType == OrderOption.Desc)
            {
                if (trp.OrderInfo.AutoTurnDigitsToNumber && items.All(f => IsNumber(f.DataForSort)))
                {
                    var x = from item in items
                            orderby Convert.ToInt64(item.DataForSort) descending
                            select item;
                    BuildTemplateResult(x, trp, sb);

                }
                else
                {
                    var x = from item in items
                            orderby item.DataForSort descending
                            select item;

                    BuildTemplateResult(x, trp, sb);

                    //if (trp.OrderInfo.AutoTurnDigitsToNumber)
                    //{
                    //    sb.AppendLine("\r\n")
                    //        .Append("WARNING: NOT ALL STRING CAN BE SUCCESSFULLY TREATED AS A NUMBER, SO SORT IN ASCII INSTEAD OF NUMBER");
                    //}
                }
            }
            else
            {
                //items.ForEach(s => sb.Append(s.Line));
                BuildTemplateResult(items, trp, sb);
            }
        }

        private void BuildTemplateResult(IEnumerable<TemplateResultItem> items,
            TemplateResultParameter trp,
            StringBuilder sb)
        {
            if (trp.BatchInfo != null && trp.BatchInfo.ItemsPerBatch > 0 &&
                !string.IsNullOrEmpty(trp.BatchInfo.BatchSeparator))
            {
                int idx = 0;

                foreach (var item in items)
                {
                    ++idx;

                    if (!string.IsNullOrEmpty(trp.RowNumFormat) && !string.IsNullOrEmpty(trp.RowNumPlaceHolder))
                    {
                        sb.Append(item.Line.Replace(trp.RowNumPlaceHolder, string.Format(trp.RowNumFormat, idx)));
                    }
                    else
                    {
                        sb.Append(item.Line);
                    }

                    if (idx % trp.BatchInfo.ItemsPerBatch == 0)
                        sb.Append(trp.BatchInfo.BatchSeparator);
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(trp.RowNumFormat) && !string.IsNullOrEmpty(trp.RowNumPlaceHolder))
                {
                    int idx = 0;

                    foreach (var item in items)
                    {
                        ++idx;
                        sb.Append(item.Line).Replace(trp.RowNumPlaceHolder, string.Format(trp.RowNumFormat, idx));
                    }
                }
                else
                {
                    items.ToList().ForEach(tri => sb.Append(tri.Line));
                }
            }
        }

        public RegexWorker(IRegexFullInfo model)
        {
            this._regexFullInfo = model;
        }
    }

}
