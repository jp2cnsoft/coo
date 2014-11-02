using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Seika.Db;
using Seika.COO.DBA.MA;

/// <summary>
/// Summary description for UserOperation
/// </summary>
[WebService(Namespace = "http://www.co-one.com/coos")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UserOperation : Seika.COO.COOS.COOWebService
{

    public UserOperation()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public List<string> GetInvalidUserIdList(string userId)
    {
        List<string> userIdLst = new List<string>();

        userIdLst.Add("001");
        userIdLst.Add("002");
        userIdLst.Add("003");

        return userIdLst;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [WebMethod]
    public string Remove(string userId)
    {
        DBConnect con = null;

        try
        {
            con = GetDbConnect();

            //删除公司表
            (new MA_COMINFO(con)).DeleteCominfo(userId);

            //删除注册表
            (new MA_REGISTER(con)).DeleteRegister(userId);

            //删除网站语言表
            (new BS_WEBLANG(con)).DelWebLang(userId);

            //删除用户帐户表
            (new MA_ACCOUNT(con)).DeleteAccount(userId);

            //删除用户银行信息
            (new MA_BANKLIST(con)).DeleteBankList(userId);

            //删除用户服务表
            (new MA_MEMBER(con)).DelMember(userId);

            //删除招聘表
            (new MA_POST(con)).DeleteJob(userId);

            //删除产品表
            (new MA_PRODUCT(con)).DeleteProd(userId);

            //删除用户XML
            (new MA_USERXML(con)).DeleteXml(userId);

            //删除点击率表
            (new MA_VIEWCOUNT(con)).DeleteViewCount(userId);

            con.Commit();
        }
        catch (Exception)
        {
            con.Rollback();
        }
        finally
        {
            CloseDbConnect(con);
        }

        return "Hello World";
    }
}

