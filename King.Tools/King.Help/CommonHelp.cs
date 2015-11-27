using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King.Help
{
    /// <summary>
    /// 常用工具 王达国 2015.11.27
    /// </summary>
    public class CommonHelp
    {
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="pages">当前页</param>
        /// <param name="sb">构造Table的html代码</param>
        /// <returns></returns>
        public static bool StreamExport(string fileName, System.Web.UI.Page pages, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            StringBuilder content = new StringBuilder();
            content.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>");
            content.Append("<head><title></title><meta http-equiv='Content-Type' content=\"text/html; charset=gb2312\">");
            content.Append("</head><body>");
            content.Append(sb);
            content.Append("</body></html>");
            content.Replace("&nbsp;", "");
            pages.Response.Clear();
            pages.Response.Buffer = true;
            pages.Response.ContentType = "application/vnd.ms-excel";  //"application/ms-excel";
            pages.Response.Charset = "GB2312";
            pages.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            fileName = System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            pages.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            pages.Response.Write(content.ToString());
            pages.Response.End();  //注意，若使用此代码结束响应可能会出现“由于代码已经过优化或者本机框架位于调用堆栈之上,无法计算表达式的值。”的异常。
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); //用此行代码代替上一行代码，则不会出现上面所说的异常。
            return true;
        }
    }
}
