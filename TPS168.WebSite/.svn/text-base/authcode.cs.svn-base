using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using TPS168.BLL;

namespace TPS168.WebSite
{
    public class authcode : System.Web.UI.Page
    {
        protected UserLogic _userLogic = new UserLogic();

        /// <summary>
        /// 随机码认证
        /// </summary>
        /// <param name="code">生成认证长度</param>
        public void DrawImage(int _codeCount)
        {
            String _authStr = _userLogic.CreateRandomCode(_codeCount);
            Session["authcode"] = _authStr.ToLower();
            CreateImages(Session["authcode"].ToString());
        }
        /// <summary>
        /// /// 生成验证图片
        /// /// </summary>
        /// /// <param name="_authCode">验证字符</param>
        protected void CreateImages(string _authCode)
        {
            int iwidth = (int)(_authCode.Length * 15);
            System.Drawing.Bitmap _image = new System.Drawing.Bitmap(iwidth, 30);
            Graphics _garph = Graphics.FromImage(_image);
            _garph.Clear(Color.White);
            //定义颜色
            Color[] _color = { Color.Red, Color.Green, Color.Blue, Color.Black, Color.Purple, Color.Orange, Color.Brown, Color.Navy };
            //定义字体 
            string[] _fontStr = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体", "Comic Sans MS" };
            Random _ran = new Random();
            ////随机输出噪点
            for (int i = 0; i < 10; i++)
            {
                int x = _ran.Next(_image.Width);
                int y = _ran.Next(_image.Height);
                _garph.DrawPie(new Pen(Color.LightGray, 0), x, y, 6, 6, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < _authCode.Length; i++)
            {
                int _cIndex = _ran.Next(7);
                int _fIndex = _ran.Next(6);
                Font _font = new System.Drawing.Font(_fontStr[_fIndex], 14, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(_color[_cIndex]);
                int j = 4;
                if ((i + 1) % 2 == 0)
                {
                    j = 2;
                }
                _garph.DrawString(_authCode.Substring(i, 1), _font, b, 3 + (i * 12), j);
            }

            //画一个边框
            _garph.DrawRectangle(new Pen(Color.Red, 0), 100, 0, _image.Width - 1, _image.Height - 1);
            //输出到浏览器
            System.IO.MemoryStream _ms = new System.IO.MemoryStream();
            _image.Save(_ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(_ms.ToArray());
            _garph.Dispose();
            _image.Dispose();
        }

    }
}