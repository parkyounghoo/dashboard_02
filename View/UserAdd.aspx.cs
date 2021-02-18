using KPC_Monitoring.Controller;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KPC_Monitoring.View
{
    public partial class UserAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserList();
                HdnRadio.Value = "A03";
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
                        Response.Redirect(string.Format("/View/CollectionModule.aspx"), false);
                    }
                    else
                    {
                        sp_userName.InnerText = Session["USER_NAME"].ToString() + "님 환영 합니다.";
                        M02.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M02");
                        M03.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M03");
                        M04.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M04");
                        M05.Visible = CommonController.setMenuList(Session["USER_MENU"].ToString(), "M05");
                    }
                }
            }
        }

        private void UserList()
        {
            UserController controller = new UserController();

            DataSet ds = controller.getUserList();

            string value = "";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                value += "사용자 ID :" + dr["USER_ID"] + "</br>";
                value += "사용자 이름 :" + dr["USER_NAME"] + "</br>";
                value += "사용자 권한 :" + dr["AUTHORITY_NAME"] + "</br></br>";
            }

            //divUserList.InnerHtml = value;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (USER_ID.Text == "")
            {
                CommonController.MessageBox(this, "ID를 입력해주세요. ");
            }
            else if (USER_NAME.Text == "")
            {
                CommonController.MessageBox(this, "이름을 입력해주세요. ");
            }
            else
            {
                string pw = CommonController.SHA256Hash(USER_ID.Text);

                string queryString = "INSERT INTO MONITORING_USER VALUES('" + USER_ID.Text + "','" + USER_NAME.Text + "','" + pw + "','" + HdnRadio.Value + "',GETDATE(),'')";

                try
                {
                    using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString))
                    {
                        DataSet ds = new DataSet();
                        SqlCommand sqlComm = new SqlCommand(queryString, sqlConn);
                        sqlComm.Connection.Open();
                        sqlComm.ExecuteNonQuery();

                        CommonController.MessageBox(this, "등록 완료. ");
                    }
                }
                catch (Exception)
                {
                    CommonController.MessageBox(this, "등록 실패. ");
                }
                finally
                {
                    USER_ID.Text = "";
                    USER_NAME.Text = "";
                }
            }
        }
    }
}