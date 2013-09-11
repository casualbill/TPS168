﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using TPS168.BLL;

namespace TPS168.WebSite.admin
{
    /// <summary>
    /// ordercfg1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ordercfg1 : System.Web.Services.WebService
    {
        protected OrderLogic _orderLogic = new OrderLogic();

        [WebMethod(EnableSession = true)]
        public int OrderOperationShortcut(String _token, String _orderId, int _orderState, String _serial)
        {
            int _retVal;

            if (_token != Session["token"].ToString())
            {
                _retVal = -4;
            }
            else
            {
                _retVal = _orderLogic.OrderOperation(Session["adminid"].ToString(), _orderId, _orderState.ToString(), _serial, "");
            }

            return _retVal;
        }
    }
}
