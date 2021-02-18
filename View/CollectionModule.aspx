<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionModule.aspx.cs" Inherits="KPC_Monitoring.View.CollectionModule" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge;IE=10;IE=9;IE=8;IE=7;">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Monitoring - Dashboard</title>
    <link href="../Scripts/css/sb-admin.css" rel="stylesheet">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/googlefonts.css" rel="stylesheet">
    <link href="../Scripts/css/sb-admin-2.min.css" rel="stylesheet">
    <script src="../Scripts/vendor/jquery/jquery.min.js"></script>
    <script src="../Scripts/js/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="../Scripts/js/jquery.mtz.monthpicker.js"></script>

    <script>
        function RadioCheck(value) {
            document.getElementById('<%=HdnRadio.ClientID%>').value = value;
        }

        function OnOffCheck() {
            document.getElementById('<%=btnCheck.ClientID%>').click();
        }

        function OnOffDate() {
            document.getElementById('<%=btnDateCheck.ClientID%>').click();
        }

        function OnOffPage() {
            document.getElementById('<%=btnPageCheck.ClientID%>').click();
        }

        function OnOffExcelDate() {
            document.getElementById('<%=btnExcelDateCheck.ClientID%>').click();
        }

        var files;
        function handleDragOver(event) {
            event.stopPropagation();
            event.preventDefault();
            var dropZone = document.getElementById('dropzone');
            $(this).css('border', '2px solid #5272A0');
        }

        function excelDown() {
            alert('엑셀 다운로드시 최초 1회 API 수집 저장 됩니다.')
        }

        function handleDnDFileSelect(event) {
            event.stopPropagation();
            event.preventDefault();
            $(this).css('border', '2px dotted #8296C2');
            /* Read the list of all the selected files. */

            if (event.dataTransfer != undefined) {
                files = event.dataTransfer.files;
                /* Consolidate the output element. */
                var form = document.getElementById('form1');
                var data = new FormData(form);

                if (files.length > 1) {
                    alert('하나의 파일만 올려 주세요.');
                }
                else if (files[0].name != 'TableTemplate.xlsx') {
                    alert('다운 받으신 템플릿을 올려주세요.');
                }
                else {
                    var div = document.getElementById("dropText");
                    div.style.visibility = 'hidden';

                    var image = document.getElementById('<%= ibdrop.ClientID %>');
                    image.style.visibility = 'visible';

                    var label = document.getElementById('<%= lbdrop.ClientID %>');
                    label.style.visibility = 'visible';

                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', "https://localhost:44305/View/CollectionModule");
                    //xhr.open('POST', "http://10.200.5.22:8089/View/CollectionModule");
                    xhr.send(data);
                }
            }
        }

        function setMonthpicker() {
            /* MonthPicker 옵션 */
            options = {
                pattern: 'yyyy-mm', // Default is 'mm/yyyy' and separator char is not mandatory
                selectedYear: new Date().getFullYear(),
                startYear: new Date().getFullYear() - 10,
                finalYear: new Date().getFullYear(),
                monthNames: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월']
            };

            /* MonthPicker Set */
            $('#monthpickerS').monthpicker(options);

            /* MonthPicker 선택 이벤트 */
            $('#monthpickerS').monthpicker().bind('monthpicker-click-month', function (e, month) {
                document.getElementById('<%=HdnSDate.ClientID%>').value = $('#monthpickerS').val();
            });

            /* MonthPicker Set */
            $('#monthpickerE').monthpicker(options);

            /* MonthPicker 선택 이벤트 */
            $('#monthpickerE').monthpicker().bind('monthpicker-click-month', function (e, month) {
                if ($('#monthpickerS').val() > $('#monthpickerE').val()) {
                    $('#monthpickerE').val() = $('#monthpickerS').val();
                    alert("선택날짜가 시작날짜 보다 작습니다.");
                }
                else {
                    document.getElementById('<%=HdnEDate.ClientID%>').value = $('#monthpickerE').val();
                }
            });
        }
    </script>
</head>
<body id="page-top">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <input type="hidden" runat="server" id="HdnSDate" />
        <input type="hidden" runat="server" id="HdnEDate" />
        <input type="hidden" runat="server" id="HdnRadio" />
        <asp:Button runat="server" ID="btnCheck" OnClick="btnCheck_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnDateCheck" OnClick="btnDateCheck_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnPageCheck" OnClick="btnPageCheck_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnExcelDateCheck" OnClick="btnExcelDateCheck_Click" Style="display: none" />
        <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if (window.File && window.FileList && window.FileReader) {
                    var dropZone = document.getElementById('dropzone');

                    if (dropZone != null) {
                        dropZone.addEventListener('dragover', handleDragOver, false);
                        dropZone.addEventListener('drop', handleDnDFileSelect, false);
                    }
                }
                else {
                    alert('Sorry! this browser does not support HTML5 File APIs.');
                }
                handleDragOver(event);
                handleDnDFileSelect(event);
            });
        </script>
        <nav class="navbar navbar-expand navbar-dark bg-dark static-top">
            <a class="navbar-brand mr-1" href="Monitoring" style="font-weight:bold">KPC 빅데이터 플랫폼 상황판</a>
            <div class="d-none d-md-inline-block form-inline ml-auto mr-0 mr-md-3 my-2 my-md-0">
                <a href="Login" id="userDropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    <i class="fas fa-fw fa-user" style="color: white"></i>
                </a>
                <span style="color: white; font-size: 14px" runat="server" id="sp_userName"></span>
            </div>
        </nav>
        <div id="wrapper" style="width: 100%; word-break: break-all">
            <ul id="SidebarUl" class="sidebar navbar-nav toggled">
                <li class="nav-item active" runat="server" id="M02">
                    <a class="nav-link" href="Monitoring">
                        <i class="fas fa-fw fa-tachometer-alt"></i>
                        <span>대시보드</span>
                    </a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="Indicators" runat="server" id="M03">
                        <i class="fas fa-fw fa-copy"></i>
                        <span>성과지표</span>
                    </a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="#" runat="server" id="M04">
                        <i class="fas fa-fw fa-wallet"></i>
                        <span>API 수집 모듈</span>
                    </a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="UserAdd" runat="server" id="M05">
                        <i class="fas fa-user-plus"></i>
                        <span>사용자 등록</span>
                    </a>
                </li>
            </ul>
            <div id="content-wrapper" style="background-color: rgb(43,43,43); min-width: 1660px">
                <div class="col-md-12" style="color: white;">
                    <div class="card mb-3" style="background-color: rgb(245,247,251); color: white;">
                        <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                            <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">API 수집 모듈</h6>
                        </div>
                        <div class="card-body" style="color: black; width: 100%; font-size: 13px;">
                            <div class="row">
                                <div class="col-xl-12">
                                    <table style="width: 100%; height: 800px">
                                        <tr>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">오픈 API 명</label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" Width="190px" ID="tbOpenApiName" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">데이터 수집 처</label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" Width="190px" ID="tbDataCollection" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            </td>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">제공 기관</label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" Width="100%" ID="tbDataService" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                            </td>
                                            <td rowspan="6" style="width: 420px; padding-left: 15px; vertical-align: top; text-align: left">
                                                <link href="../Scripts/css/component-custom-switch.css" rel="stylesheet">
                                                <div runat="server" id="batchCheck" class="custom-switch custom-switch-label-onoff custom-switch-sm pl-0" style="padding-bottom: 15px" onchange="OnOffCheck();" tabindex="0">
                                                    <input class="custom-switch-input" id="onoff" type="checkbox">
                                                    <label class="custom-switch-btn" for="onoff"></label>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table runat="server" id="tblBatch" style="height: 700px; width: 100%; opacity: 0.5;">
                                                            <tr>
                                                                <td align="left">
                                                                    <label class="form-control-label" style="margin-top: 10px">데이터 수집 주기</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <div style="float: left">
                                                                        <asp:TextBox runat="server" ID="tbCycle" TextMode="Number" Width="70px" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                                                    </div>
                                                                    <div style="float: left">
                                                                        <label class="form-control-label" style="margin-top: 10px">일</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 20px">
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <style>
                                                                        div.disabled {
                                                                            pointer-events: none;
                                                                            opacity: 0.5;
                                                                        }
                                                                    </style>
                                                                    <div class="checkbox icheck-success disabled" onchange="OnOffDate();" runat="server" id="dateCheck">
                                                                        <input type="checkbox" checked id="success" />
                                                                        <label for="success">날짜 값</label>
                                                                    </div>
                                                                    <div>
                                                                        <label style="color: red; font-weight: bold" class="form-control-label">
                                                                            ※ 날짜 Parameter가 한개인 경우는 시작 Parameter에 입력
                                                                        </label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <label class="form-control-label" style="margin-top: 10px">날짜 시작 Parameter 명</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Width="100%" ID="tbSDateParameter" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnDateCheck" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <label class="form-control-label" style="margin-top: 10px">날짜 종료 Parameter 명</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Width="100%" ID="tbEDateParameter" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnDateCheck" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 20px">
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <div class="checkbox icheck-success disabled" onchange="OnOffPage();" runat="server" id="pageCheck">
                                                                        <input type="checkbox" checked id="success3" />
                                                                        <label for="success3">페이지 번호</label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <label class="form-control-label" style="margin-top: 10px">페이지 번호 Parameter 명</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Width="100%" ID="tbPageResultName" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnPageCheck" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="3">
                                                                    <label class="form-control-label" style="margin-top: 10px">페이지당 건수 Parameter 명</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Width="100%" ID="tbPageOfRows" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnPageCheck" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <label class="form-control-label" style="margin-top: 10px">전체 결과 수 Parameter 명</label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Width="100%" ID="tbTotalCountName" CssClass="form-control" TabIndex="12"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnPageCheck" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnCheck" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td rowspan="6" style="width: 330px; padding-left: 15px; vertical-align: top">
                                                <div style="margin-bottom: 8px">
                                                    <i class="fas fa-fw fa-check"></i>
                                                    <label class="form-control-label">결과 값</label>
                                                    <asp:Button runat="server" Text="초기화" Style="margin-left: 160px" ID="btnReset" OnClick="btnReset_Click" CssClass="btn btn-icon btn-fab btn-info" />
                                                </div>
                                                <div>
                                                    <asp:UpdatePanel runat="server" ID="upResult" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" TextMode="MultiLine" Width="100%" Height="700px" Enabled="false" ID="tbResult"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnbatch" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">사용 목적</label>
                                            </td>
                                            <td colspan="8">
                                                <asp:TextBox runat="server" Width="100%" ID="tbPurpose" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">호출 URL</label>
                                            </td>
                                            <td colspan="8">
                                                <asp:TextBox runat="server" Width="100%" ID="tbUrl" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">응답 형태</label>
                                            </td>
                                            <td colspan="8">
                                                <link rel="stylesheet" href="../Scripts/css/icheck-bootstrap.css" />
                                                <div style="float: left">
                                                    <div class="radio icheck-success">
                                                        <input type="radio" id="success1" name="success" onclick="RadioCheck('JSON');" />
                                                        <label for="success1" class="form-control-label">JSON</label>
                                                    </div>
                                                </div>
                                                <div style="float: left; padding-left: 15px">
                                                    <div class="radio icheck-success">
                                                        <input type="radio" checked id="success2" name="success" onclick="RadioCheck('XML');" />
                                                        <label for="success2" class="form-control-label">XML</label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label class="form-control-label" style="margin-top: 10px">테이블 명</label>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" Width="150px" ID="tbTableName" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                            </td>
                                            <td colspan="5">
                                                <asp:Button runat="server" Text="템플릿 다운로드" ID="btnTemplate" OnClick="btnTemplate_Click" CssClass="btn btn-icon btn-fab btn-success" />
                                            </td>
                                            <td align="right">
                                                <asp:Button runat="server" Text="API EXCEL 내보내기" ID="btnExcelDown" OnClick="btnExcelDown_Click" OnClientClick="excelDown()" CssClass="btn btn-icon btn-fab btn-success" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="height: 510px; vertical-align: top">
                                                <asp:UpdatePanel runat="server" ID="upUpload" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <div id="dropzone" style="width: 100%; height: 510px;" runat="server">
                                                            <div id="dropText">
                                                                Drag & Drop Files Here
                                                            </div>
                                                            <asp:ImageButton runat="server" ImageUrl="~/images/excel.jpg" ID="ibdrop" Style="visibility: hidden" OnClick="ibdrop_Click" />
                                                            <br />
                                                            <asp:Label runat="server" ID="lbdrop" Style="visibility: hidden">TableTemplate.xlsx</asp:Label>
                                                        </div>
                                                        <asp:GridView runat="server" ID="grdTable" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                            <Columns>
                                                                <asp:BoundField DataField="ColumnName" HeaderText="ColumnName" />
                                                                <asp:BoundField DataField="Size" HeaderText="Size" />
                                                                <asp:BoundField DataField="항목설명" HeaderText="항목설명" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="15" align="center">
                                                <br />
                                                <label style="color: red; font-weight: bold" class="form-control-label">※ 등록 시 최초 1회 수집이 실행되며 해당 결과 값이 출력 됩니다.&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                <asp:Button runat="server" Text="등록" ID="btnbatch" OnClick="btnbatch_Click" CssClass="btn btn-icon btn-fab btn-facebook" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mb-3" style="background-color: rgb(245,247,251); color: black;" runat="server" id="divApiList">
                        <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                            <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">사용자 API 리스트</h6>
                        </div>
                        <div class="card-body" style="color: black; width: 100%; font-size: 13px;">
                            <table style="margin-bottom: 15px">
                                <tr>
                                    <td>
                                        <div class="checkbox icheck-success" runat="server" id="ExcelDateCheck" onchange="OnOffExcelDate();" style="margin-right: 20px">
                                            <input type="checkbox" checked id="success4" />
                                            <label for="success4">전체</label>
                                        </div>
                                    </td>
                                    <td>
                                        <label class="form-control-label" style="margin-top: 8px"></label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <input id="monthpickerS" class="form-control" type="text" style="width: 80px; font-size: 13px; text-align: center; height: 25px;" runat="server" />
                                                <script>
                                                    var prm = Sys.WebForms.PageRequestManager.getInstance();

                                                    prm.add_endRequest(function () {
                                                        setMonthpicker();
                                                    });
                                                </script>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnExcelDateCheck" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>~
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <input id="monthpickerE" class="form-control" type="text" style="width: 80px; font-size: 13px; text-align: center; height: 25px" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnExcelDateCheck" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" Text="API EXCEL 내보내기" ID="btnCheckExcelDown" OnClick="btnCheckExcelDown_Click" Height="25px" Style="vertical-align: auto; font-size: 11px;" CssClass="btn btn-icon btn-fab btn-success" />
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <style type="text/css">
                                        .hiddencol {
                                            display: none;
                                        }
                                    </style>
                                    <asp:GridView ID="grdApiList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-centered mb-0" Font-Size="11px">
                                        <HeaderStyle Font-Bold="true" Font-Size="13px" ForeColor="Black" BackColor="#ECECED" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="데이터셋명" DataField="API_NAME" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="데이터수집처" DataField="API_Data_Collection" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="제공기관" DataField="API_Data_Service" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="사용목적" DataField="API_Purpose" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="수집주기" ItemStyle-Width="90px" DataField="CYCLE_NAME" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="생성자" ItemStyle-Width="80px" DataField="USER_NAME" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="생성일자" ItemStyle-Width="120px" DataField="API_CreateDt" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="테이블명" DataField="TABLE_NAME" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <!-- Sticky Footer -->
                <footer class="sticky-footer" style="background-color: black">
                    <div class="container my-auto">
                        <div class="copyright text-center my-auto">
                            <span style="color: white">Copyright © 2019 Korea Productivity Center. All Rights Reserved.</span>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        <!-- /#wrapper -->
        <style>
            #dropzone {
                border: 2px dotted #3292A2;
                width: 90%;
                height: 50px;
                color: #92AAB0;
                text-align: center;
                font-size: 24px;
                padding-top: 12px;
                margin-top: 10px;
            }
        </style>
        <!-- Scroll to Top Button-->
        <a class="scroll-to-top rounded" href="#page-top">
            <i class="fas fa-angle-up"></i>
        </a>
    </form>
</body>
<script>
    if (window.File && window.FileList && window.FileReader) {
        var dropZone = document.getElementById('dropzone');

        if (dropZone != null) {
            dropZone.addEventListener('dragover', handleDragOver, false);
            dropZone.addEventListener('drop', handleDnDFileSelect, false);
        }
    }
    else {
        alert('Sorry! this browser does not support HTML5 File APIs.');
    }
</script>
</html>