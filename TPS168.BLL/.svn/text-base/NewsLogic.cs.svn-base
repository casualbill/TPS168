using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using TPS168.DAL;

namespace TPS168.BLL
{
    public class NewsLogic
    {
        protected NewsDB _newsDB = new NewsDB();



        //根据新闻类型查询新闻信息（分页列表）
        public DataSet NewsQueryByNewsType(String _newsType, int _queryMode, int _lengthLimit, int _pageIndex, int _pageSize, out int _pageAmount)
        {
            int _intNum;

            if (!int.TryParse(_newsType, out _intNum))
            {
                _newsType = "-1";
            }

            DataSet _ds;
            int _index = 0;
            _ds = _newsDB.NewsQueryByNewsType(_newsType, _queryMode.ToString(), _pageIndex.ToString(), _pageSize.ToString(), out _pageAmount);

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("NewsTypeStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _newsTypeStr = _dr[3].ToString();
                    String _noHtmlStr = remove_html_tag(_ds.Tables[0].Rows[_index][2].ToString());
                    int _length = _noHtmlStr.Length;

                    switch (_newsTypeStr)
                    {
                        case "1": _ds.Tables[0].Rows[_index]["NewsTypeStr"] = "站内公告"; break;
                        case "2": _ds.Tables[0].Rows[_index]["NewsTypeStr"] = "游戏资讯"; break;
                        case "3": _ds.Tables[0].Rows[_index]["NewsTypeStr"] = "常见问题"; break;

                    }

                    if (_length > _lengthLimit)
                    {
                        _noHtmlStr = _noHtmlStr.Substring(0, _lengthLimit);
                        _noHtmlStr += "...";
                    }

                    _ds.Tables[0].Rows[_index][2] = _noHtmlStr;

                    _index++;
                }

            }

            return _ds;
        }

        //根据新闻ID返回新闻内容
        public String[] NewsContentQueryByNewsId(String _newsId)
        {
            int _intNum;

            if (!int.TryParse(_newsId, out _intNum))
            {
                _newsId = null;
            }


            DataSet _ds = _newsDB.NewsContentQueryByNewsId(_newsId);
            String[] _newsStr = new String[10];

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        _newsStr[i] = _ds.Tables[0].Rows[0][i].ToString();
                    }
                }
                else
                {
                    _newsStr[0] = null;
                }
            }
            else
            {
                _newsStr[0] = null;
            }

            return _newsStr;
        }


        //新闻修改
        public int NewsContentUpdate(String[] _newsStr)
        {
            return _newsDB.NewsContentUpdate(_newsStr);
        }

        //新闻添加
        public int NewsContentInsert(String[] _newsStr)
        {
            return _newsDB.NewsContentInsert(_newsStr);
        }

        //新闻删除
        public int NewsDelete(String _newsId)
        {
            int _intNum;

            if (!int.TryParse(_newsId, out _intNum))
            {
                _newsId = null;
            }

            return _newsDB.NewsDelete(_newsId);
        }


        //去除html标签元素
        public static string remove_html_tag(string _htmlString)
        { 
            _htmlString = Regex.Replace(_htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
             
            _htmlString = Regex.Replace(_htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"-->", "", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);

            _htmlString = Regex.Replace(_htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            _htmlString = Regex.Replace(_htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            _htmlString.Replace("<", "");
            _htmlString.Replace(">", "");
            _htmlString.Replace("\r\n", "");
            _htmlString = HttpContext.Current.Server.HtmlEncode(_htmlString).Trim();

            return _htmlString;
        }

    }
}
