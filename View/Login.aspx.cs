using KPC_Monitoring.Controller;
using KPC_Monitoring.Db;
using System;
using System.Data;
using System.Web.UI;

namespace KPC_Monitoring.View
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           this.UserPw.Attributes["onkeyPress"] = "if(event.keyCode == 13) {" +
           Page.GetPostBackEventReference(this.btnLogin) + "; return false;}";

           this.UserId.Attributes["onkeyPress"] = "if(event.keyCode == 13) {" +
           Page.GetPostBackEventReference(this.btnLogin) + "; return false;}";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginDb.LoginCheck(UserId.Text, UserPw.Text))
                {
                    LoginDb.LoginDateUpdate(UserId.Text);

                    DataSet ds = LoginDb.getUserMenu(UserId.Text);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        Session["USER_ID"] = UserId.Text;
                        Session["USER_NAME"] = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                        Session["AUTHORITY_CODE"] = ds.Tables[0].Rows[0]["AUTHORITY_CODE"].ToString();
                        Session["USER_MENU"] = ds.Tables[0].Rows[0]["MENU_CODE"].ToString();
                        Response.Redirect(string.Format(CommonController.RedirectUrl(ds.Tables[0].Rows[0]["AUTHORITY_CODE"].ToString())), false);
                    }
                }
                else
                {
                    CommonController.MessageBox(this, "로그인 정보가 잘못되었습니다. ");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void modalbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginDb.LoginCheck(modalId.Text, modalCurrent.Text))
                {
                    LoginDb.PasswordChange(modalId.Text, modalCurrent.Text, modalNew.Text);
                    CommonController.MessageBox(this, "성공 했습니다. ");
                }
                else
                {
                    CommonController.MessageBox(this, "사용자 정보가 잘못되었습니다. ");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}