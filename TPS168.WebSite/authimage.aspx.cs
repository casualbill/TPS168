using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS168.WebSite
{
    public partial class authimage : authcode
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawImage(4);
        }
    }
}