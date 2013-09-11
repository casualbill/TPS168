using System;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public partial class test1 : System.Web.UI.Page
    {
        protected GameLogic _gameLogic = new GameLogic();
        protected authcode _authCode = new authcode();
        protected OrderLogic _ol = new OrderLogic();
        protected errorlog _err = new errorlog();

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.MapPath(".");
            int a = 1;
            int b = 0;

            Response.Write(a / b);
        }

        protected void ddl_changed(object sender, EventArgs e)
        {

            lb.Text = ddl.SelectedValue;
        }

        protected void btn_click(object sender, EventArgs e)
        {
            DateTime _dateTime = DateTime.Now;

            Random _ran=new Random();
            String _r = _ran.Next(0, 100).ToString("000");
            //lb2.Text = _dateTime.ToString("yyyyMMddhhmmss");
            lb2.Text = _r;

            lb.Text = Session["authcode"].ToString();
        }
    }
}