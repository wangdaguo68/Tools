using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using King.Help;
namespace King.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnTestClick(object sender, EventArgs e)
        {
            string html = "<table cellspacing=\"0\" style=\"border-width:0px;width:1000px;border-collapse:collapse;\" id=\"gvData\" rules=\"all\" class=\"GridView\"><tbody><tr class=\"GridViewHeader\"><th style=\"width:50px;\" scope=\"col\">操作</th><th scope=\"col\">报警内容</th><th style=\"width:100px;\" scope=\"col\">报警类型</th><th style=\"width:100px;\" scope=\"col\">接报人</th><th style=\"width:100px;\" scope=\"col\">报修时间</th><th style=\"width:100px;\" scope=\"col\">处理人</th><th style=\"width:100px;\" scope=\"col\">处理部门</th><th style=\"width:100px;\" scope=\"col\">接报状态</th><th style=\"width:100px;\" scope=\"col\">当前状态</th></tr><tr onmouseout=\"this.style.backgroundColor= c\" onmouseover=\"c=this.style.backgroundColor;this.style.backgroundColor='#f4f2f3'\"class=\"GridViewRow\"></tr></tbody></table>";
            CommonHelp.StreamExport("Test.xls",this.Page,new System.Text.StringBuilder(html));
        }
    }
}