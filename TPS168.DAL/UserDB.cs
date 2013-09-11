using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TPS168.DAL
{
    public class UserDB
    {
        protected DBHelper _dbHelper = new DBHelper();

        public int UserLoginValidate(String _userName, String _pwd, String _isSysCon, String _IPAddress)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserLoginValidate", _userName, _pwd, _isSysCon, _IPAddress, null);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserCreate(String _userName, String _pwd, String _regIP, String _validationCode)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserCreate", _userName, _pwd, _regIP, _validationCode, null);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserDetailsInsert(String[] _userDetailsStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserDetailsInsert", _userDetailsStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserDetailsUpdate(String[] _userDetailsStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserDetailsUpdate", _userDetailsStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserPayInfoUpdate(String[] _userDetailsStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserPayInfoUpdate", _userDetailsStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserPasswordUpdate(String _uid, String _pwdOld, String _pwdNew)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserPasswordUpdate", _uid, _pwdOld, _pwdNew, null);
            _dbHelper.Close();
            return _retVal;
        }

        public int UserPasswordUpdateAdmin(String _uid, String _pwdNew)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserPasswordUpdateAdmin", _uid, _pwdNew, null);
            _dbHelper.Close();
            return _retVal;
        }

        public DataSet UserQueryByUserName(String _userName, String _queryMode)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.UserQueryByUserName", _userName, _queryMode, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet UserQueryByUserId(String _userId, String _queryMode)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.UserQueryByUserId", _userId, _queryMode, null);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet UserQueryByUserState(String _userState, String _queryMode, String _pageIndex, String _pageSize, out int _pageAmount)
        {

            _dbHelper.Open();
            _pageAmount = _dbHelper.ExecuteSPNonQuery("dbo.UserQueryByUserState", _userState, _queryMode, _pageIndex, _pageSize, null);
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.UserQueryByUserState", _userState, _queryMode, _pageIndex, _pageSize, null);
            _dbHelper.Close();
            return _ds;
        }

        public int UserStateUpdate(String _adminId, String _uid, String _userState)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserStateUpdate", _adminId, _uid, _userState, null);
            _dbHelper.Close();
            return _retVal;
        }




        public DataSet GroupInfoQueryByGroupId(String _groupId)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.GroupInfoQueryByGroupId", _groupId);
            _dbHelper.Close();
            return _ds;
        }

        public DataSet GroupInfoQueryList()
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.GroupInfoQueryList");
            _dbHelper.Close();
            return _ds;
        }

        public int GroupInfoInsert(String[] _groupInfoStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GroupInfoInsert", _groupInfoStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int GroupInfoUpdate(String[] _groupInfoStr)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GroupInfoUpdate", _groupInfoStr);
            _dbHelper.Close();
            return _retVal;
        }

        public int GroupInfoDelete(String _groupId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.GroupInfoDelete", _groupId, null);
            _dbHelper.Close();
            return _retVal;
        }

        public DataSet UserQueryByUserGroupId(String _groupId)
        {
            _dbHelper.Open();
            DataSet _ds = _dbHelper.ExecuteSPDataSet("dbo.UserQueryByUserGroupId", _groupId);
            _dbHelper.Close();
            return _ds;
        }

        public int UserGroupOfUserInfoUpdate(String _uid, String _groupId)
        {
            _dbHelper.Open();
            int _retVal = _dbHelper.ExecuteSPNonQuery("dbo.UserGroupOfUserInfoUpdate", _uid, _groupId, null);
            _dbHelper.Close();
            return _retVal;
        }

    }
}
