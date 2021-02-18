using KPC_Monitoring.Controller;
using KPC_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPC_Monitoring.View
{
    public partial class CollectionModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Check"] = false;
                Session["DateCheck"] = false;
                Session["PageCheck"] = false;
                Session["ExcelDateCheck"] = true;
                HdnRadio.Value = "XML";
                //사용자 메뉴 체크
                if (Session["USER_ID"] == null)
                {
                    Response.Redirect(string.Format("/View/Login.aspx"), false);
                }
                //메뉴 설정
                else
                {
                    if (Session["AUTHORITY_CODE"].ToString() == "A02")
                    {
                        Response.Redirect(string.Format("/View/Monitoring.aspx"), false);
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

                tblBatch.Disabled = true;
                tbSDateParameter.Enabled = true;
                tbEDateParameter.Enabled = true;
                tbPageResultName.Enabled = true;
                tbPageOfRows.Enabled = true;
                tbTotalCountName.Enabled = true;
                dropzone.Visible = true;
                grdTable.Visible = false;
                tbCycle.Enabled = false;
                tbSDateParameter.Enabled = false;
                tbEDateParameter.Enabled = false;
                tbPageResultName.Enabled = false;
                tbPageOfRows.Enabled = false;
                tbTotalCountName.Enabled = false;

                //ApiList
                monthpickerS.Disabled = true;
                monthpickerE.Disabled = true;
                string date = DateTime.Now.ToString("yyyy-MM");
                HdnSDate.Value = HdnEDate.Value = date;
                monthpickerS.Value = monthpickerE.Value = date;

                CollectionController module = new CollectionController();
                DataSet ds = module.ApiList(Session["USER_ID"].ToString());

                if (ds.Tables[0].Rows.Count == 0)
                {
                    divApiList.Visible = false;
                }

                grdApiList.DataSource = ds;
                grdApiList.DataBind();
            }
            else
            {
                UploadFile(sender, e);
            }
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            if (Request.Files.Count != 0)
            {
                dropzone.Visible = false;
                grdTable.Visible = true;

                string filePath = "";
                //drag drop 파일 저장
                HttpFileCollection files = Request.Files;
                foreach (string key in files)
                {
                    HttpPostedFile file = files[key];

                    string fileName = "";
                    if (file.FileName.Contains("\\"))
                    {
                        fileName = file.FileName.Substring(file.FileName.LastIndexOf("\\")).Replace("\\", "");
                    }
                    else
                    {
                        fileName = file.FileName;
                    }

                    filePath = Server.MapPath("~/Uploads/" + fileName);
                    file.SaveAs(filePath);
                }

                ConfigurationManager.AppSettings.Set("filePath", filePath);
            }
        }

        //엑셀 템플릿 다운로드
        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            HttpResponse response = this.Response;

            string fileName = "TableTemplate.xlsx";
            string filePath = Server.MapPath(Path.Combine("~/Template", "TableTemplate.xlsx"));

            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("content-disposition",
                            "attachment; filename=" + fileName + ";");
            response.TransmitFile(filePath);

            response.Flush();
            response.End();
        }

        private void ApiCollection(string gubun)
        {
            bool check = true;

            tbResult.Text = "";

            #region 유효성 체크

            if (tbOpenApiName.Text.Trim() == "")
            {
                tbResult.Text += "오픈 API 명을 입력해 주세요.\n";

                check = false;
            }

            if (tbPurpose.Text.Trim() == "")
            {
                tbResult.Text += "사용목적을 입력해 주세요.\n";

                check = false;
            }

            if (tbUrl.Text.Trim() == "")
            {
                tbResult.Text += "호출 URL을 입력해 주세요.\n";

                check = false;
            }
            else if (!CommonController.CheckURLValid(tbUrl.Text))
            {
                tbResult.Text += "URL 형식이 아닙니다.\n";

                check = false;
            }

            if (tbTableName.Text == "")
            {
                tbResult.Text += "테이블명을 입력해 주세요.\n";

                check = false;
            }

            if ((bool)Session["DateCheck"])
            {
                if (tbSDateParameter.Text.Trim() == "")
                {
                    tbResult.Text += "날짜 시작 Parameter를 입력해 주세요.\n";

                    check = false;
                }
            }

            if ((bool)Session["PageCheck"])
            {
                if (tbPageResultName.Text.Trim() == "")
                {
                    tbResult.Text += "페이지 번호 Parameter를 입력해 주세요.\n";

                    check = false;
                }
                if (tbPageOfRows.Text.Trim() == "")
                {
                    tbResult.Text += "페이지당 건수 Parameter를 입력해 주세요.\n";

                    check = false;
                }
                if (tbTotalCountName.Text.Trim() == "")
                {
                    tbResult.Text += "전체 결과 수 Parameter를 입력해 주세요.\n";

                    check = false;
                }
            }

            #endregion 유효성 체크

            if (check)
            {
                CollectionController module = new CollectionController();

                API_Model model = new API_Model();
                model.API_NAME = tbOpenApiName.Text;
                model.API_Purpose = tbPurpose.Text;
                model.API_URL = tbUrl.Text;
                model.API_Response = HdnRadio.Value;
                model.TABLE_NAME = tbTableName.Text;
                model.API_Proc_Name = "proc_" + tbTableName.Text;
                model.API_CreateDt = DateTime.Now.ToString("yyyy-MM-dd");
                model.API_Data_Collection = tbDataCollection.Text;
                model.API_Data_Service = tbDataService.Text;
                model.API_USER_ID = Session["USER_ID"].ToString();

                if ((bool)Session["Check"])
                {
                    model.API_PageResult_YN = (bool)Session["PageCheck"] ? "Y" : "N";
                    model.API_Date_YN = (bool)Session["DateCheck"] ? "Y" : "N";
                    model.API_S_Date_Parameter = tbSDateParameter.Text;
                    model.API_E_Date_Parameter = tbEDateParameter.Text;
                    model.API_PageResult_Name = tbPageResultName.Text;
                    model.API_PageOfRows_Name = tbPageOfRows.Text;
                    model.API_TotalCount_Name = tbTotalCountName.Text;
                    model.API_Collection_Cycle = tbCycle.Text;
                }
                else
                {
                    model.API_PageResult_YN = "";
                    model.API_Date_YN = "";
                    model.API_S_Date_Parameter = "";
                    model.API_E_Date_Parameter = "";
                    model.API_PageResult_Name = "";
                    model.API_PageOfRows_Name = "";
                    model.API_TotalCount_Name = "";
                    model.API_Collection_Cycle = "";
                }

                List<TableModel> list = module.TablePanelToTableModel(grdTable);
                string sqlmessage = "";

                //테이블 생성
                string tableString = module.PanelCreateTable(list, model.TABLE_NAME);
                if (!ValidateSql(tableString, out sqlmessage))
                {
                    tbResult.Text = sqlmessage;

                    return;
                }

                //프로시저 생성
                string procString = module.PanelCreateProc(list, model.API_Proc_Name, model.TABLE_NAME);
                if (!ValidateSql(tableString, out sqlmessage))
                {
                    tbResult.Text = sqlmessage;

                    return;
                }

                model.API_CREATE_TABLE = tableString;
                model.API_CREATE_PROC = procString;

                string message = "";
                module.setCollectionModule(model, list, out message);

                if (message == "성공")
                {
                    if (gubun == "excel")
                    {
                        string fileName = model.API_NAME + "(" + model.TABLE_NAME + ")_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                        string filePath = Server.MapPath(Path.Combine("~/Uploads"));
                        module.getApiExcel(module.CollectionTable(model.TABLE_NAME,"", "", true), filePath, fileName);
                        module.FileDownload(this, filePath, fileName);
                    }
                }

                tbResult.Text = message;
            }
        }

        //배치 등록
        protected void btnbatch_Click(object sender, EventArgs e)
        {
            ApiCollection("batch");
        }

        protected void btnExcelDown_Click(object sender, EventArgs e)
        {
            ApiCollection("excel");
        }

        private bool ValidateSql(string str, out string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                message = "테이블명을 입력하세요.";

                return false;
            }
            else
            {
                message = "aa";
                return true;
            }
        }

        //입력 값 초기화
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.Set("filePath", "");
            Response.Redirect("CollectionModule.aspx");
        }

        protected void ckbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                tbSDateParameter.Enabled = true;
                tbEDateParameter.Enabled = true;
            }
            else
            {
                tbSDateParameter.Enabled = false;
                tbEDateParameter.Enabled = false;
            }
        }

        protected void ckbPageResult_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                tbPageResultName.Enabled = true;
                tbPageOfRows.Enabled = true;
                tbTotalCountName.Enabled = true;
            }
            else
            {
                tbPageResultName.Enabled = false;
                tbPageOfRows.Enabled = false;
                tbTotalCountName.Enabled = false;
            }
        }

        protected void ibdrop_Click(object sender, ImageClickEventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["filePath"];

            if (filePath == "")
            {
                tbResult.Text = "파일을 올려 주세요.";
            }
            else
            {
                tbResult.Text = "";

                dropzone.Visible = false;
                grdTable.Visible = true;

                CommonController common = new CommonController();
                DataTable dt = common.ExcelToDataSet(filePath);

                grdTable.DataSource = dt;
                grdTable.DataBind();
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if ((bool)Session["Check"])
            {
                Session["Check"] = false;
                Session["DateCheck"] = false;
                Session["PageCheck"] = false;
                tblBatch.Disabled = true;
                tbCycle.Enabled = false;
                tbSDateParameter.Enabled = false;
                tbEDateParameter.Enabled = false;
                tbPageResultName.Enabled = false;
                tbPageOfRows.Enabled = false;
                tbTotalCountName.Enabled = false;
                tblBatch.Style.Add("opacity", "0.5");
                dateCheck.Style.Add("pointer-events", "none");
                dateCheck.Style.Add("opacity", "0.5");
                pageCheck.Style.Add("pointer-events", "none");
                pageCheck.Style.Add("opacity", "0.5");
            }
            else
            {
                Session["Check"] = true;
                Session["DateCheck"] = true;
                Session["PageCheck"] = true;
                tblBatch.Disabled = false;
                tbCycle.Enabled = true;
                tbCycle.Text = "1";
                tbSDateParameter.Enabled = true;
                tbEDateParameter.Enabled = true;
                tbPageResultName.Enabled = true;
                tbPageOfRows.Enabled = true;
                tbTotalCountName.Enabled = true;
                tblBatch.Style.Add("opacity", "1");
                dateCheck.Style.Add("pointer-events", "visible");
                dateCheck.Style.Add("opacity", "1");
                pageCheck.Style.Add("pointer-events", "visible");
                pageCheck.Style.Add("opacity", "1");
                tbSDateParameter.Enabled = true;
                tbEDateParameter.Enabled = true;
                tbPageResultName.Enabled = true;
                tbPageOfRows.Enabled = true;
                tbTotalCountName.Enabled = true;
            }
        }

        protected void btnDateCheck_Click(object sender, EventArgs e)
        {
            if ((bool)Session["DateCheck"])
            {
                Session["DateCheck"] = false;
                tbSDateParameter.Enabled = false;
                tbEDateParameter.Enabled = false;
            }
            else
            {
                Session["DateCheck"] = true;
                tbSDateParameter.Enabled = true;
                tbEDateParameter.Enabled = true;
            }
        }

        protected void btnPageCheck_Click(object sender, EventArgs e)
        {
            if ((bool)Session["PageCheck"])
            {
                Session["PageCheck"] = false;
                tbPageResultName.Enabled = false;
                tbPageOfRows.Enabled = false;
                tbTotalCountName.Enabled = false;
            }
            else
            {
                Session["PageCheck"] = true;
                tbPageResultName.Enabled = true;
                tbPageOfRows.Enabled = true;
                tbTotalCountName.Enabled = true;
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdApiList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    chkRow.Checked = ((CheckBox)sender).Checked;
                }
            }
        }

        protected void btnCheckExcelDown_Click(object sender, EventArgs e)
        {
            List<API_Model> list = new List<API_Model>();

            foreach (GridViewRow row in grdApiList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string fileName = row.Cells[1].Text + "(" + row.Cells[8].Text + ")_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                        string filePath = Server.MapPath(Path.Combine("~/Uploads"));
                        list.Add(new API_Model() { TABLE_NAME = row.Cells[8].Text, FILE_NAME = fileName, FILE_PATH = filePath });
                    }
                }
            }

            if (list.Count == 0)
            {
                CommonController.MessageBox(this, "선택된 파일이 없습니다.");
            }
            else
            {
                CollectionController module = new CollectionController();
                module.ApiListToExcelDownload(this, list, Server.MapPath(Path.Combine("~/Uploads")), Session["USER_NAME"].ToString(), monthpickerS.Value, monthpickerE.Value, (bool)Session["ExcelDateCheck"]);
            }
        }

        protected void btnExcelDateCheck_Click(object sender, EventArgs e)
        {
            if ((bool)Session["ExcelDateCheck"])
            {
                Session["ExcelDateCheck"] = false;

                monthpickerS.Disabled = false;
                monthpickerE.Disabled = false;
            }
            else
            {
                Session["ExcelDateCheck"] = true;

                monthpickerS.Disabled = true;
                monthpickerE.Disabled = true;
            }
        }
    }
}