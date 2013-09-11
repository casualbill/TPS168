using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Web.Security;
using System.Data;
using System.Security.Cryptography;
using TPS168.DAL;


namespace TPS168.BLL
{
    public class UserLogic
    {
        protected UserDB _userDB = new UserDB();

        //登录验证 返回用户基本信息
        public String[] UserLoginValidate(int _isConSys, String _userName, String _pwd, String _IPAddress)  // _isConSys：是否为管理员
        {
            String[] _userStr = new String[10];
            String _pwdMD5 = GetMD5HashCode(_pwd);
            DataTable _userDT;

            _userStr[0] = _userDB.UserLoginValidate(_userName, _pwdMD5, _isConSys.ToString(), _IPAddress).ToString();

            if (_userStr[0] != "0")
            {
                _userDT = _userDB.UserQueryByUserName(_userName, "0").Tables[0];

                
                for (int i = 1; i < 9; i++)
                {
                    _userStr[i] = _userDT.Rows[0][i - 1].ToString();

                }

            }

            return _userStr;
        }

        //用户注册 填写信息
        public int UserCreate(String _userName, String _pwd, String _ipAddress, String[] _userDetailsStr,out String[] _userStr)
        {
            int _retVal;
            String _pwdMD5 = GetMD5HashCode(_pwd);

            if (_userName.Length >= 4 && _pwd.Length >= 6)
            {
                _retVal = _userDB.UserCreate(_userName, _pwdMD5, _ipAddress, CreateRandomCode(6));
            }
            else
            {
                _retVal = -1;
            }

            if (_retVal == 1)   //注册成功
            {
                _userStr = UserLoginValidate(0, _userName, _pwd, _ipAddress);

                _userDetailsStr[0] = _userStr[1];

                if (_userDB.UserDetailsInsert(_userDetailsStr) == 1) _retVal = 2;   //填写信息成功
                else _retVal = 1;
            }
            else
            {
                _userStr = new String[10];
            }

            return _retVal;
        }

        //用户密码修改
        public int UserPasswordUpdate(String _uid, String _pwdOld, String _pwdNew)
        {
            return _userDB.UserPasswordUpdate(_uid, GetMD5HashCode(_pwdOld), GetMD5HashCode(_pwdNew));
        }

        //用户密码修改（不验证）
        public int UserPasswordUpdateAdmin(String _uid, String _pwdNew)
        {
            return _userDB.UserPasswordUpdateAdmin(_uid, GetMD5HashCode(_pwdNew));
        }

        //用户详细信息修改
        public int UserDetailsUpdate(String[] _userDetailsStr)
        {
            return _userDB.UserDetailsUpdate(_userDetailsStr);
        }

        //用户支付信息修改
        public int UserPayInfoUpdate(String[] _userDetailsStr)
        {
            return _userDB.UserPayInfoUpdate(_userDetailsStr);
        }

        //根据用户名或ID查询用户详细信息   传参_queryType:0为用户名查询，1为ID查询   返回数组 0为用户名 1为ID
        public String[] UserQueryByUserNameOrUserId(String _userStr,int _queryType)
        {
            DataSet _ds;
            String[] _userInfo = new String[30];
            int _uid;

            if (_queryType == 0)
            {
                _ds = _userDB.UserQueryByUserName(_userStr, "2");
            }
            else
            {
                if (int.TryParse(_userStr, out _uid))
                {
                    _ds = _userDB.UserQueryByUserId(_uid.ToString(), "2");
                }
                else
                {
                    _userInfo[1] = null;
                    return _userInfo;
                }
            }


            if (_ds.Tables.Count != 0)
            {
                if (_ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 1; i < 25; i++)
                    {
                        _userInfo[i] = _ds.Tables[0].Rows[0][i - 1].ToString();
                    }

                    switch (_userInfo[3])
                    {
                        case "0": _userInfo[25] = "尚未填写用户信息"; break;
                        case "1": _userInfo[25] = "等待审核"; break;
                        case "2": _userInfo[25] = "未通过审核"; break;
                        case "3": _userInfo[25] = "正常"; break;
                        case "4": _userInfo[25] = "禁止访问"; break;
                    }

                }
                else
                {
                    _userInfo[1] = null;
                }
            }
            else
            {
                _userInfo[1] = null;
            }

            return _userInfo;
        }




        //根据状态查询用户信息（分页列表）
        public DataSet UserQueryByUserState(int _userState, int _queryMode, int _pageIndex, out int _pageAmount)
        {
            DataSet _ds;
            int _index = 0;
            _ds = _userDB.UserQueryByUserState(_userState.ToString(), _queryMode.ToString(), _pageIndex.ToString(), "20", out _pageAmount);

            if (_ds.Tables.Count != 0)
            {
                _ds.Tables[0].Columns.Add("UserStateStr", Type.GetType("System.String"));

                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    String _userStateStr = _dr[4].ToString();

                    switch (_userStateStr)
                    {
                        case "0": _ds.Tables[0].Rows[_index]["UserStateStr"] = "尚未填写用户信息"; break;
                        case "1": _ds.Tables[0].Rows[_index]["UserStateStr"] = "等待审核"; break;
                        case "2": _ds.Tables[0].Rows[_index]["UserStateStr"] = "未通过审核"; break;
                        case "3": _ds.Tables[0].Rows[_index]["UserStateStr"] = "正常"; break;
                        case "4": _ds.Tables[0].Rows[_index]["UserStateStr"] = "禁止访问"; break;
                    }

                    _index++;
                }

            }

            return _ds;
        }

        //更新用户状态
        public int UserStateUpdate(String _adminId, String _uid, int _userState)
        {
            return _userDB.UserStateUpdate(_adminId, _uid, _userState.ToString());
        }


        //根据管理员所在用户组验证管理权限  _index 2:系统用户管理 3:基本设置 4:用户管理 5:游戏管理 6:新闻设置 7:公告设置 8:订单处理
        public int AdminConfigAuthority(String _groupId, int _index)
        {
            int _retVal = 0;
            String _authorityInfo;
            DataSet _ds = _userDB.GroupInfoQueryByGroupId(_groupId);

            if (_ds.Tables.Count > 0)
            {
                _authorityInfo = _ds.Tables[0].Rows[0][_index].ToString();
                _retVal = Convert.ToInt16(_authorityInfo);
            }

            return _retVal;
        }

        //查询所有用户组信息
        public DataSet UserGroupQueryList()
        {
            DataSet _dsSrc = _userDB.GroupInfoQueryList();
            DataSet _dsObj = new DataSet();

            _dsObj = _dsSrc.Clone();

            for (int i = 2; i < 9; i++)
            {
                _dsObj.Tables[0].Columns[i].DataType = Type.GetType("System.String");
            }

            if (_dsSrc.Tables.Count > 0)
            {
                foreach (DataRow _drSrc in _dsSrc.Tables[0].Rows)
                {
                    DataRow _drObj = _dsObj.Tables[0].NewRow();

                    _drObj[0] = _drSrc[0];
                    _drObj[1] = _drSrc[1];

                    for (int i = 2; i < 9; i++)
                    {
                        if (_drSrc[i].ToString() == "0") { _drObj[i] = "否"; }
                        if (_drSrc[i].ToString() == "1") { _drObj[i] = "是"; }
                        if (_drSrc[i].ToString() == "2") { _drObj[i] = "完全"; }
                    }

                    _dsObj.Tables[0].Rows.Add(_drObj);
                }
            }

            return _dsObj;
        }


        //根据用户组ID查询用户（管理员）信息
        public DataSet UserQueryByUserGroupId(String _groupId)
        {
            int _intNum;

            if (!int.TryParse(_groupId, out _intNum))
            {
                _groupId = "-1";
            }

            return _userDB.UserQueryByUserGroupId(_groupId);

        }


        //查询用户组详细信息
        public String[] UserGroupQueryDetails(String _groupId)
        {
            String[] _groupInfoStr = new String[10];
            int _intNum;

            if (!int.TryParse(_groupId, out _intNum))
            {
                _groupId = null;
            }

            DataSet _ds = _userDB.GroupInfoQueryByGroupId(_groupId);

            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        _groupInfoStr[i] = _ds.Tables[0].Rows[0][i].ToString();
                    }

                }
                else
                {
                    _groupInfoStr[0] = null;
                }
            }
            else
            {
                _groupInfoStr[0] = null;
            }

            return _groupInfoStr;
        }

        //用户组添加
        public int GroupInfoInsert(String[] _groupInfoStr)
        {
            return _userDB.GroupInfoInsert(_groupInfoStr);
        }


        //用户组修改
        public int GroupInfoUpdate(String[] _groupInfoStr)
        {
            return _userDB.GroupInfoUpdate(_groupInfoStr);
        }


        //用户组删除
        public int GroupInfoDelete(String _groupId)
        {
            int _intNum;

            if (!int.TryParse(_groupId, out _intNum))
            {
                _groupId = null;
            }

            return _userDB.GroupInfoDelete(_groupId);
        }


        //更改用户所在用户组
        public int UserGroupOfUserInfoUpdate(String _uid, String _groupId)
        {
            int _intNum;

            if (!int.TryParse(_uid, out _intNum))
            {
                _groupId = null;
            }

            if (!int.TryParse(_groupId, out _intNum))
            {
                _groupId = null;
            }

            return _userDB.UserGroupOfUserInfoUpdate(_uid, _groupId);
        }


        //获取MD5 32位Hash值
        protected String GetMD5HashCode(String _sourceStr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(_sourceStr, "MD5"); 
        }


        //获得随机字符串
        public string CreateRandomCode(int _codeCount)
        {
            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            Random _ran = new Random(BitConverter.ToInt32(b, 0));
            string _str = null, str = "123456789abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

            for (int i = 0; i < _codeCount; i++) { _str += str.Substring(_ran.Next(0, str.Length - 1), 1); }

            return _str;
        }

    }
}
