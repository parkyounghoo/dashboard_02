using KPC_Monitoring.Controller;
using System;

namespace KPC_Monitoring.View
{
    public partial class Indicators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //사용자 메뉴 체크
            if (Session["USER_ID"] == null)
            {
                Response.Redirect(string.Format("/View/Login.aspx"), false);
            }
            //메뉴 설정
            else
            {
                if (Session["AUTHORITY_CODE"].ToString() == "A03")
                {
                    Response.Redirect(string.Format("/View/Indicators.aspx"), false);
                }
                else
                {
                    sp_userName.InnerText = Session["USER_NAME"].ToString() + "님 환영 합니다."; ;
                    M02.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M02");
                    M03.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M03");
                    M04.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M04");
                    M05.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M05");
                }
            }
        }
    }
}