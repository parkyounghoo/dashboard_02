using KPC_Monitoring.Controller;
using KPC_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;

namespace KPC_Monitoring.View
{
    public partial class Monitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM");
            if (!IsPostBack)
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
                        Response.Redirect(string.Format("/View/CollectionModule.aspx"), false);
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

                HdnDate.Value = date;
                //보유 데이터 종합
                Domain_Data_Total(date);
                //보유 데이터 도메인 별
                Domain_Data(date);
                Domain_Chart(date, "교육", "");
                Donut_Chart(date, "교육", "postback");

                //외부 수집 데이터
                Private_Data(date);
                //연계 데이터 조회
                Contact_Data(date);

                ////컨트롤 초기화
                dvGrid.Style.Add("display", "none");
                grdDomain.Visible = false;
                grdDetail.Visible = false;
            }

            monthpicker.Value = HdnDate.Value;
        }

        private void Donut_Chart(string date, string domainName, string gubun)
        {
            MonitoringController controller = new MonitoringController();

            DataSet ds = controller.Domain_Data(date);

            if (gubun == "postback")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Donut", getDonut(domainName, ds), true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "eventDonut", getDonut(domainName, ds), true);
            }
        }

        private string getDonut(string domainName, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            #region DonutChart

            if (domainName == "교육")
            {
                EDU1.InnerHtml = "";
                sb.Append("$('#EDU1').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString()) * 100 / double.Parse(divTableCount.InnerText), 2) + ",								");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#A2B1DC', 20: '#8CA1DE', 30: '#6F8CE0', 40: '#5A7EE4', 50: '#3E6BEA', 60: '#2A5EF0', 70: '#144FF5', 80: '#0645FB', 90: '#0042FF', 100: '#0042FF' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString())) + "',													");
                sb.Append("    text: '테이블 수',																																					    ");
                sb.Append("    textY: 100,																																								");
                sb.Append("    textColor: '#4E73DF',																																					");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("																																												");
                sb.Append("});	                                                                                                                                                                        ");
                sb.Append("$('#EDU2').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[0]["TABLE_ROW"].ToString()) * 100 / double.Parse(divTableRow.InnerText), 2) + ",								    ");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#C7E0D7', 20: '#ADDBCB', 30: '#94DBC2', 40: '#7FDFBC', 50: '#61E2B4', 60: '#61E2B4', 70: '#3BEAAB', 80: '#3BEAAB', 90: '#10F5A3', 100: '#10F5A3' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_ROW"].ToString())) + "',														");
                sb.Append("    text: '데이터 건수',																																						");
                sb.Append("    textY: 100,																																								");
                sb.Append("    textColor: '#5EC89D',																																		            ");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("});																																											");
                sb.Append("$('#EDU3').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[0]["TABLE_SIZE"].ToString()) * 100 / double.Parse(divTableSize.InnerText.Replace(" KB", "")), 2) + ",			    ");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#DCD2BB', 20: '#DCD2BB', 30: '#DAC799', 40: '#DEC279', 50: '#E5BA4D', 60: '#E5BA4D', 70: '#EBB62E', 80: '#EBB62E', 90: '#F8B407', 100: '#F8B407' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_SIZE"].ToString())) + " KB" + "',											");
                sb.Append("    text: '데이터 용량',																																						");
                sb.Append("    textY: 100,																																								");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textColor: '#F6C23E',																																					");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("});																																											");
            }
            else
            {
                PROD1.InnerHtml = "";
                sb.Append("$('#PROD1').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[1]["TABLE_COUNT"].ToString()) * 100 / double.Parse(divTableCount.InnerText), 2) + ",								");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#A2B1DC', 20: '#8CA1DE', 30: '#6F8CE0', 40: '#5A7EE4', 50: '#3E6BEA', 60: '#2A5EF0', 70: '#144FF5', 80: '#0645FB', 90: '#0042FF', 100: '#0042FF' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[1]["TABLE_COUNT"].ToString())) + "',													");
                sb.Append("    text: '테이블 수',																																					    ");
                sb.Append("    textY: 100,																																								");
                sb.Append("    textColor: '#4E73DF',																																					");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("																																												");
                sb.Append("});                                                                                                                                                                          ");
                sb.Append("$('#PROD2').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[1]["TABLE_ROW"].ToString()) * 100 / double.Parse(divTableRow.InnerText), 2) + ",								    ");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#C7E0D7', 20: '#ADDBCB', 30: '#94DBC2', 40: '#7FDFBC', 50: '#61E2B4', 60: '#61E2B4', 70: '#3BEAAB', 80: '#3BEAAB', 90: '#10F5A3', 100: '#10F5A3' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[1]["TABLE_ROW"].ToString())) + "',														");
                sb.Append("    text: '데이터 건수',																																						");
                sb.Append("    textY: 100,																																								");
                sb.Append("    textColor: '#5EC89D',																																					");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("});																																											");
                sb.Append("$('#PROD3').circliful({																																						");
                sb.Append("    animation: 0,																																							");
                sb.Append("    animateInView: true,																																						");
                sb.Append("    percent: " + Math.Round(double.Parse(ds.Tables[0].Rows[1]["TABLE_SIZE"].ToString()) * 100 / double.Parse(divTableSize.InnerText.Replace(" KB", "")), 2) + ",			    ");
                sb.Append("    textSize: 30,																																							");
                sb.Append("    textStyle: 'font-size: 14px;',																																			");
                sb.Append("    textColor: '#CC9487',																																					");
                sb.Append("    progressColor: { 10: '#DCD2BB', 20: '#DCD2BB', 30: '#DAC799', 40: '#DEC279', 50: '#E5BA4D', 60: '#E5BA4D', 70: '#EBB62E', 80: '#EBB62E', 90: '#F8B407', 100: '#F8B407' },");
                sb.Append("	   replacePercentageByText: '" + string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[1]["TABLE_SIZE"].ToString())) + " KB" + "',										    ");
                sb.Append("    text: '데이터 용량',																																						");
                sb.Append("    textY: 100,																																								");
                sb.Append("    textColor: '#F6C23E',																																					");
                sb.Append("    fontColor: 'white',																																					    ");
                sb.Append("    percentageTextSize: 15,																														                            ");
                sb.Append("    percentageY: '120',																																					    ");
                sb.Append("    textStyle: 'font-weight:bold;font-size: 14px;'																															");
                sb.Append("});																																											");
            }

            #endregion DonutChart

            return sb.ToString();
        }

        private void Domain_Data_Total(string date)
        {
            MonitoringController controller = new MonitoringController();

            DataSet ds = controller.Domain_Data_Total(date);

            divTableCount.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString()));
            divTableRow.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_ROW"].ToString()));
            divTableSize.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_SIZE"].ToString())) + " KB";

            TableCountAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_COUNT_ADD"].ToString());
            TableRowAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_ROW_ADD"].ToString());
            TableSizeAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_SIZE_ADD"].ToString());
        }

        private string AddHtml(string add)
        {
            string html = "";
            if (add == "0.00")
            {
                html = "<span class='text-Secondary mr-2'>" + add + "%</span>";
            }
            else
            {
                html = "<span class='text-success mr-2'>" +
                    "<i class='fa fa-arrow-up' style='font-size: 10px'>" + add + "%</i>" +
                    "</span>";
            }

            html += "<span class='text-nowrap' style='font-size:10px'>Since last month</span>";

            return html;
        }

        private void Domain_Data(string date)
        {
            MonitoringController controller = new MonitoringController();

            DataSet ds = controller.Domain_Data(date);

            string html = "";
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                html += "<a href='#' class='dropdown-item' onclick=\"dropDownClick('" + ds.Tables[1].Rows[i]["DOMAIN_NAME"] + "')\">" + ds.Tables[1].Rows[i]["DOMAIN_NAME"] + "</a>";
            }

            dropDomainList.InnerHtml = html;
        }

        private void Domain_Chart(string date, string domainName, string gubun)
        {
            MonitoringController controller = new MonitoringController();
            DataSet ds = controller.Domain_Data_Chart(date, domainName, gubun);
            chartDomainName.InnerText = domainName;

            if (gubun == "event" || gubun == "Week")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "eventChart", getChart(ds.Tables[0], gubun), true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Chart", getChart(ds.Tables[0], gubun), true);
            }
        }

        private string getChart(DataTable dt, string gubun)
        {
            StringBuilder sb = new StringBuilder();

            string labels = "";
            if (gubun == "Week")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows.Count == i - 1)
                    {
                        labels += "'W0" + (i + 1) + "'";
                    }
                    else
                    {
                        labels += "'W0" + (i + 1) + "',";
                    }
                }
            }
            else
            {
                labels += "'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'";
            }

            string tableCount = "";
            string tableRow = "";
            string tableSize = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (11 == i)
                {
                    DataRow dr = dt.Rows[i];
                    tableCount += dr["TABLE_COUNT"];
                    tableRow += dr["TABLE_ROW"];
                    tableSize += dr["TABLE_SIZE"];
                }
                else
                {
                    DataRow dr = dt.Rows[i];
                    tableCount += dr["TABLE_COUNT"] + ",";
                    tableRow += dr["TABLE_ROW"] + ",";
                    tableSize += dr["TABLE_SIZE"] + ",";
                }
            }

            #region Chart

            sb.Append("var ctx = document.getElementById('myAreaChart');");
            sb.Append("var myLineChart = new Chart(ctx, {");
            sb.Append("    type: 'line',");
            sb.Append("    data: {");
            sb.Append("        labels: [" + labels + "],");
            sb.Append("        datasets: [{");
            sb.Append("            label: 'Number of Table',");
            sb.Append("            lineTension: 0.3,");
            sb.Append("            backgroundColor: 'rgba(78, 115, 225, 0.05)',");
            sb.Append("            borderColor: 'rgba(78, 115, 225, 1)',");
            sb.Append("            pointRadius: 3,");
            sb.Append("            pointBackgroundColor: 'rgba(78, 115, 225, 1)',");
            sb.Append("            pointBorderColor: 'rgba(78, 115, 225, 1)',");
            sb.Append("            pointHoverRadius: 3,");
            sb.Append("            pointHoverBackgroundColor: 'rgba(78, 115, 225, 1)',");
            sb.Append("            pointHoverBorderColor: 'rgba(78, 115, 225, 1)',");
            sb.Append("            pointHitRadius: 10,");
            sb.Append("            pointBorderWidth: 2,");
            sb.Append("            data: [" + tableCount + "],");
            sb.Append("        },");
            sb.Append("        {");
            sb.Append("            label: 'Table Row Count',");
            sb.Append("            lineTension: 0.3,");
            sb.Append("            backgroundColor: 'rgba(28, 200, 138, 0.05)',");
            sb.Append("            borderColor: 'rgba(28, 200, 138, 1)',");
            sb.Append("            pointRadius: 3,");
            sb.Append("            pointBackgroundColor: 'rgba(28, 200, 138, 1)',");
            sb.Append("            pointBorderColor: 'rgba(28, 200, 138, 1)',");
            sb.Append("            pointHoverRadius: 3,");
            sb.Append("            pointHoverBackgroundColor: 'rgba(28, 200, 138, 1)',");
            sb.Append("            pointHoverBorderColor: 'rgba(28, 200, 138, 1)',");
            sb.Append("            pointHitRadius: 10,");
            sb.Append("            pointBorderWidth: 2,");
            sb.Append("            data: [" + tableRow + "],");
            sb.Append("        },");
            sb.Append("        {");
            sb.Append("            label: 'Table Size',");
            sb.Append("            lineTension: 0.3,");
            sb.Append("            backgroundColor: 'rgba(246, 194, 62, 0.05)',");
            sb.Append("            borderColor: 'rgba(246, 194, 62, 1)',");
            sb.Append("            pointRadius: 3,");
            sb.Append("            pointBackgroundColor: 'rgba(246, 194, 62, 1)',");
            sb.Append("            pointBorderColor: 'rgba(246, 194, 62, 1)',");
            sb.Append("            pointHoverRadius: 3,");
            sb.Append("            pointHoverBackgroundColor: 'rgba(246, 194, 62, 1)',");
            sb.Append("            pointHoverBorderColor: 'rgba(246, 194, 62, 1)',");
            sb.Append("            pointHitRadius: 10,");
            sb.Append("            pointBorderWidth: 2,");
            sb.Append("            data: [" + tableSize + "],");
            sb.Append("        }");
            sb.Append("        ],");
            sb.Append("    },");
            sb.Append("    options: {");
            sb.Append("        maintainAspectRatio: false,");
            sb.Append("        layout: {");
            sb.Append("            padding: {");
            sb.Append("                left: 10,");
            sb.Append("                right: 25,");
            sb.Append("                top: 25,");
            sb.Append("                bottom: 0");
            sb.Append("            }");
            sb.Append("        },");
            sb.Append("        scales: {");
            sb.Append("            xAxes: [{");
            sb.Append("                ticks: {");
            sb.Append("                    fontColor: 'white'");
            sb.Append("                },");
            sb.Append("                time: {");
            sb.Append("                    unit: 'date'");
            sb.Append("                },");
            sb.Append("                gridLines: {");
            sb.Append("                    display: false,");
            sb.Append("                    drawBorder: false");
            sb.Append("                },");
            sb.Append("            }],");
            sb.Append("            yAxes: [{");
            sb.Append("                ticks: {");
            sb.Append("                    maxTicksLimit: 5,");
            sb.Append("                    fontColor: 'white',");
            sb.Append("                    padding: 10,");
            sb.Append("                },");
            sb.Append("                gridLines: {");
            sb.Append("                    color: 'rgb(234, 236, 244)',");
            sb.Append("                    zeroLineColor: 'rgb(234, 236, 244)',");
            sb.Append("                    drawBorder: false,");
            sb.Append("                    borderDash: [2],");
            sb.Append("                    zeroLineBorderDash: [2]");
            sb.Append("                }");
            sb.Append("            }],");
            sb.Append("        },");
            sb.Append("        legend: {");
            sb.Append("            display: false");
            sb.Append("        },");
            sb.Append("        tooltips: {");
            sb.Append("            backgroundColor: 'rgb(255,255,255)',");
            sb.Append("            bodyFontColor: '#858796',");
            sb.Append("            titleMarginBottom: 10,");
            sb.Append("            titleFontColor: '#6e707e',");
            sb.Append("            titleFontSize: 14,");
            sb.Append("            borderColor: '#dddfeb',");
            sb.Append("            borderWidth: 1,");
            sb.Append("            xPadding: 15,");
            sb.Append("            yPadding: 15,");
            sb.Append("            displayColors: false,");
            sb.Append("            intersect: false,");
            sb.Append("            mode: 'index',");
            sb.Append("            caretPadding: 10,");
            sb.Append("        }");
            sb.Append("    }");
            sb.Append("});");

            #endregion Chart

            return sb.ToString();
        }

        private void Private_Data(string date)
        {
            MonitoringController controller = new MonitoringController();

            DataSet ds = controller.Private_Data(date);

            ApiTableCount.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString()));
            ApiRowCount.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_ROW"].ToString()));
            ApiTableSize.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_SIZE"].ToString())) + " KB";

            ApiTableCountAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_COUNT_ADD"].ToString());
            ApiRowCountAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_ROW_ADD"].ToString());
            ApiTableSizeAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_SIZE_ADD"].ToString());
        }

        private void Contact_Data(string date)
        {
            MonitoringController controller = new MonitoringController();

            DataSet ds = controller.Contact_Data(date);

            LinkTableCount.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_COUNT"].ToString()));
            LinkRowCount.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_ROW"].ToString()));
            LinkTableSize.InnerText = string.Format("{0:n0}", int.Parse(ds.Tables[0].Rows[0]["TABLE_SIZE"].ToString())) + " KB";

            LinkTableCountAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_COUNT_ADD"].ToString());
            LinkRowCountAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_ROW_ADD"].ToString());
            LinkTableSizeAdd.InnerHtml = AddHtml(ds.Tables[0].Rows[0]["TABLE_SIZE_ADD"].ToString());
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            string date = HdnDate.Value;
            //보유 데이터 종합
            Domain_Data_Total(date);
            //보유 데이터 도메인 별
            Domain_Data(date);
            Domain_Chart(date, "교육", "");
            Donut_Chart(date, "교육", "postback");

            //외부 수집 데이터
            Private_Data(date);
            //연계 데이터 조회
            Contact_Data(date);

            ////컨트롤 초기화
            dvGrid.Style.Add("display", "none");
            grdDomain.Visible = false;
            grdDetail.Visible = false;
        }

        /// <summary>
        /// 조회 키 값
        /// EDU : 교육
        /// PROD : 생산성 연구·통계
        /// PRIV : 외부 수집
        /// PRIV_D : 외부 수집 데이터
        /// ETL_D : ETL
        /// CONT_D : 연계 데이터
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnValue_Click(object sender, EventArgs e)
        {
            this.grdDetail.PageIndex = 0;
            this.grdDomain.PageIndex = 0;
            string date = HdnDate.Value;
            dvGrid.Style.Add("display", "block");

            grdBind(date, HdnValue.Value);
        }

        private void grdBind(string date, string gubun)
        {
            MonitoringController controller = new MonitoringController();

            if (gubun == "EDU")
            {
                grdDomain.Visible = true;
                grdDetail.Visible = false;
                DataSet ds = controller.Domain_Data_EDU(date);
                grdDomain.DataSource = ds;
                grdDomain.DataBind();
            }
            else if (gubun == "PROD")
            {
                grdDomain.Visible = true;
                grdDetail.Visible = false;
                DataSet ds = controller.Domain_Data_PROD(date);
                grdDomain.DataSource = ds;
                grdDomain.DataBind();
            }
            else if (gubun == "PRIV_D")
            {
                dbName = "CENTER_RAW";
                grdDetail.Visible = true;
                grdDomain.Visible = false;
                List<MONI_DATA_MODEL> ds = controller.Private_Data_D(date);
                grdDetail.DataSource = ds;
                grdDetail.DataBind();
            }
            else if (gubun == "CONT_D")
            {
                dbName = "CENTER_MART";
                grdDetail.Visible = true;
                grdDomain.Visible = false;
                List<MONI_DATA_MODEL> ds = controller.Contact_Data_D(date);
                grdDetail.DataSource = ds;
                grdDetail.DataBind();
            }
        }

        private static string dbName;

        protected void btnScheStatus_Click(object sender, EventArgs e)
        {
            MonitoringController controller = new MonitoringController();
            List<MONI_DATA_MODEL> list = controller.Sche_Status(dbName, HdnTableName.Value, HdnDate.Value);

            if (list.Count != 0)
            {
                mpDetailLog.Show();
                grdDetailLog.DataSource = list;
                grdDetailLog.DataBind();
            }
        }

        protected void modalbtnClose_Click(object sender, EventArgs e)
        {
            mpDetailLog.Hide();
        }

        protected void btnChart_Click(object sender, EventArgs e)
        {
            string date = HdnDate.Value;

            string domainName = "";
            if (HdnChart.Value == "0")
            {
                domainName = "생산성 연구 통계";
            }
            else
            {
                domainName = "교육";
            }

            Domain_Chart(date, domainName, "event");
            Donut_Chart(date, domainName, "event");
        }

        protected void btnDropdown_Click(object sender, EventArgs e)
        {
            string date = HdnDate.Value;

            Domain_Chart(date, HdnDropdown.Value, "event");
            Donut_Chart(date, HdnDropdown.Value, "event");
        }

        protected void btnDate_Click(object sender, EventArgs e)
        {
            string date = HdnDate.Value;

            if (HdnChart.Value == "0")
            {
                Domain_Chart(date, "생산성 연구 통계", HdnDateCheck.Value == "W" ? "Week" : "event");
            }
            else
            {
                Domain_Chart(date, "교육", HdnDateCheck.Value == "W" ? "Week" : "event");
            }
        }

        protected void grdDomain_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            this.grdDomain.PageIndex = e.NewPageIndex;

            grdBind(HdnDate.Value, HdnValue.Value);
        }

        protected void grdDetail_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            this.grdDetail.PageIndex = e.NewPageIndex;

            grdBind(HdnDate.Value, HdnValue.Value);
        }
    }
}