<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitoring.aspx.cs" Inherits="KPC_Monitoring.View.Monitoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge;IE=10;IE=9;IE=8;IE=7;">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Monitoring - Dashboard</title>

    <link href="../Scripts/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">
    <link href="../Scripts/css/sb-admin.css" rel="stylesheet">
    <script src="../Scripts/vendor/jquery/jquery.min.js"></script>
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css">
    <script src="../Scripts/js/jquery-1.12.4.js"></script>
    <script src="../Scripts/js/jquery-ui.js"></script>
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/googlefonts.css" rel="stylesheet">
    <link href="../Scripts/css/sb-admin-2.min.css" rel="stylesheet">

    <script type="text/javascript">
        function dropDownClick(gubun) {
            $('.carousel').carousel('pause');

            if (gubun == "교육") {
                document.getElementById('<%=HdnChart.ClientID%>').value = "1";
            }
            else {
                document.getElementById('<%=HdnChart.ClientID%>').value = "0";
            }

            document.getElementById('<%=HdnDropdown.ClientID%>').value = gubun;
            document.getElementById('<%=btnDropdown.ClientID%>').click();
        }

        function dateClick(gubun) {
            $('.carousel').carousel('pause');

            document.getElementById('<%=HdnDateCheck.ClientID%>').value = gubun;
            document.getElementById('<%=btnDate.ClientID%>').click();
        }

        function div_Click(message) {
            var element = document.getElementById("dvGrid");
            element.style.display = "block";

            var offset = $('#dvGrid').offset();
            $('html').animate({ scrollTop: offset.top }, 400);
            document.getElementById('<%=HdnValue.ClientID%>').value = message;
            document.getElementById('<%=btnValue.ClientID%>').click();
        }

        function Status_Click(message) {
            document.getElementById('<%=HdnTableName.ClientID%>').value = message;
            document.getElementById('<%=btnScheStatus.ClientID%>').click();
        }

        $(document).ready(function () {
            $('#carouselDomainFake').on('slide.bs.carousel', function () {
                var slideFrom = $(this).find('.active').index();
                document.getElementById('<%=HdnChart.ClientID%>').value = slideFrom;
                document.getElementById('<%=btnChart.ClientID%>').click();
            });

            $(".nav .nav-link").on("click", function () {
                $(".nav").find(".active").removeClass("active");
                $(this).addClass("active");
            });
        });
    </script>

    <style>
        #monthpicker {
            width: 80px;
        }

        #btn_monthpicker {
            background: url('../images/datepicker.png');
            border: 0;
            height: 24px;
            width: 24px;
        }

        .circleDetail {
            background-color: white;
            width: 10px;
            height: 10px;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            cursor: pointer;
            margin-top: 5px;
        }
    </style>
</head>
<body id="page-top">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="HdnDate" />
        <input type="hidden" runat="server" id="HdnValue" />
        <input type="hidden" runat="server" id="HdnTableName" />
        <input type="hidden" runat="server" id="HdnChart" />
        <input type="hidden" runat="server" id="HdnDropdown" />
        <input type="hidden" runat="server" id="HdnDateCheck" />
        <asp:Button runat="server" ID="btnValue" OnClick="btnValue_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnScheStatus" OnClick="btnScheStatus_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnfake" Style="display: none" />
        <asp:Button runat="server" ID="btnChart" OnClick="btnChart_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnDropdown" OnClick="btnDropdown_Click" Style="display: none" />
        <asp:Button runat="server" ID="btnDate" OnClick="btnDate_Click" Style="display: none" />
        <cc1:ToolkitScriptManager runat="server" ScriptMode="Release" />
        <div id="carouselDomainFake" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                </div>
                <div class="carousel-item">
                </div>
            </div>
        </div>
        <nav class="navbar navbar-expand navbar-dark bg-dark static-top">
            <a class="navbar-brand mr-1" href="Monitoring" style="font-weight:bold">KPC 빅데이터 플랫폼 상황판</a>

            <!-- Navbar Search -->
            <div class="d-none d-md-inline-block form-inline ml-auto mr-0 mr-md-3 my-2 my-md-0">
                <a href="Login" id="userDropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    <i class="fas fa-fw fa-user" style="color: white"></i>
                </a>
                <span style="color: white; font-size: 14px" runat="server" id="sp_userName"></span>
            </div>
        </nav>

        <div id="wrapper">
            <!-- Sidebar -->
            <ul id="SidebarUl" class="sidebar navbar-nav toggled">
                <li class="nav-item active" runat="server" id="M02">
                    <a class="nav-link" href="#">
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
                    <a class="nav-link" href="CollectionModule" runat="server" id="M04">
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
            <div id="content-wrapper" style="background-color: rgb(43,43,43);min-width:1660px">
                <div class="container-fluid" style="font-size: 12px">
                    <ol class="breadcrumb" style="background-color: rgb(62,62,62);">
                        <li class="breadcrumb-item active" style="font-weight: bold; color: white; font-size: 18px; width: 100%; height: 30px">
                            <div style="float: left;">
                                <label style="margin: 0px">대시보드</label>
                            </div>
                            <div style="float: right; width: 15%;">
                                <table>
                                    <tr>
                                        <td>
                                            <input id="monthpicker" type="text" style="width: 80px; font-size: 13px; text-align: center; height: 24px" runat="server" />
                                        </td>

                                        <td>
                                            <asp:Button runat="server" ID="btnSerch" Text="검색" CssClass="btn btn-secondary btn-sm" Height="24px"
                                                Style="vertical-align: auto; font-size: 11px;" OnClick="btnSerch_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <script type="text/javascript" src="../Scripts/js/jquery-1.11.1.min.js"></script>
                                <script type="text/javascript" src="../Scripts/js/jquery-ui.min.js"></script>
                                <script type="text/javascript" src="../Scripts/js/jquery.mtz.monthpicker.js"></script>
                                <script>
                                    /* MonthPicker 옵션 */
                                    options = {
                                        pattern: 'yyyy-mm', // Default is 'mm/yyyy' and separator char is not mandatory
                                        selectedYear: new Date().getFullYear(),
                                        startYear: new Date().getFullYear() - 5,
                                        finalYear: new Date().getFullYear() + 5,
                                        monthNames: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월']
                                    };

                                    /* MonthPicker Set */
                                    $('#monthpicker').monthpicker(options);

                                    /* MonthPicker 선택 이벤트 */
                                    $('#monthpicker').monthpicker().bind('monthpicker-click-month', function (e, month) {
                                        document.getElementById('<%=HdnDate.ClientID%>').value = $('#monthpicker').val();
                                    });
                                </script>
                            </div>
                        </li>
                    </ol>
                    <div class="row">
                        <div style="float: left; width: 19%; padding-left: 12px;">
                            <style>
                                .header-line {
                                    height: 5px;
                                    width: 100%;
                                    content: '';
                                    display: block;
                                }

                                .gradient-color-1 {
                                    background: linear-gradient(to top, #1e3c72 0%, #1e3c72 1%, #2a5298 100%);
                                } 
                                .gradient-color-2 {
                                    background: linear-gradient(to top, #1e3c72 0%, #1e3c72 1%, #2a5298 100%);
                                } 
                                .gradient-color-3 {
                                    background: linear-gradient(to top, #09203f 0%, #537895 100%);
                                } 
                                .gradient-color-4 {
                                    background: linear-gradient(to top, #09203f 0%, #537895 100%);
                                }
                            </style>
                            <div class="card border-0 mb-3" style="background-color: rgb(245,247,251); color: black;">
                                <div class="card-header flex-row align-items-center justify-content-between gradient-color-1" style="color: white; background-color: gray;">
                                    <h6 class="m-0 font-weight-bold text" style="font-size: 13px;"><i class="fas fa-align-justify"></i> 보유데이터 종합</h6>
                                </div>
                                <div class="card-body">
                                    <div class="row" style="height: 480px; color: black; width: 100%;">
                                        <div class="col-xl-12 col-md-6 mb-3">
                                            <div class="card border-left-primary shadow h-100 py-2" style="background-color: rgb(23,43,77); color: white">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-primary text-uppercase mb-1" style="font-size: 13px">
                                                                테이블 수
                                                            </div>
                                                            <div runat="server" id="divTableCount" class="h5 mb-0 font-weight-bold text-gray-300">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-primary" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-database" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="TableCountAdd" class="mt-3 mb-0 text-sm">
                                                        <span class="text-nowrap" style="font-size: 10px">Since last month
                                                        </span>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-12 col-md-6 mb-3">
                                            <div class="card border-left-success shadow h-100 py-2" style="background-color: rgb(23,43,77); color: white">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-success text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 건수
                                                            </div>
                                                            <div runat="server" id="divTableRow" class="h5 mb-0 font-weight-bold text-gray-300">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-success" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-clipboard-list" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="TableRowAdd" class="mt-3 mb-0 text-sm">
                                                        <span class="text-nowrap" style="font-size: 10px">Since last month
                                                        </span>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-12 col-md-6 mb-3">
                                            <div class="card border-left-warning shadow h-100 py-2" style="background-color: rgb(23,43,77); color: white">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-warning text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 용량
                                                            </div>
                                                            <div runat="server" id="divTableSize" class="h5 mb-0 font-weight-bold text-gray-300">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-warning" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="far fa-hdd" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="TableSizeAdd" class="mt-3 mb-0 text-sm">
                                                        <span class="text-nowrap" style="font-size: 10px">Since last month
                                                        </span>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 80%; padding-left: 24px;">
                            <div class="card border-0 mb-3" style="background-color: rgb(245,247,251); color: white">
                                <div class="card-header flex-row align-items-center justify-content-between gradient-color-2" style="color: white; background-color: #4E73DF;">
                                    <h6 class="m-0 font-weight-bold text" style="font-size: 13px;"><i class="fas fa-chart-line"></i> 보유데이터 ( 도메인 별 )</h6>
                                </div>
                                <div class="card-body">
                                    <div class="row" style="height: 480px; color: black;">
                                        <div class="col-md-10" style="padding: 0px;">
                                            <div class="card shadow" style="background-color: rgb(23,43,77);">
                                                <div class="card-header card-header bg-transparent border-0" style="background-color: rgb(23,43,77); color: white">
                                                    <div class="row align-items-center">
                                                        <div class="col">
                                                            <h6 class="text-light text-uppercase ls-1 mb-1 pt-2 text-gray-500" style="font-size: 9px">Overview</h6>
                                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <h5 class="h3 text-white mb-0 pt-1" style="font-size: 17px" runat="server" id="chartDomainName"></h5>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnChart" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnDropdown" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnDate" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col mr-3">
                                                            <style type="text/css">
                                                                .domain {
                                                                    background-color: white
                                                                }
                                                            </style>
                                                            <ul class="nav nav-pills justify-content-end">
                                                                <li class="nav-item mr-2 mr-md-0">
                                                                    <a href="#" class="domain nav-link py-2 px-3 mr-3 active" onclick="dateClick('M')">
                                                                        <span class="d-none d-md-block">Month</span>
                                                                        <span class="d-md-none">M</span>
                                                                    </a>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a href="#" class="domain nav-link py-2 px-3" onclick="dateClick('W')">
                                                                        <span class="d-none d-md-block">Week</span>
                                                                        <span class="d-md-none">W</span>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-body" style="height: 409px">
                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="chart-area">
                                                                <canvas id="myAreaChart"></canvas>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnChart" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnDropdown" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnDate" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="height: 480px; padding: 0px">
                                            <div class="col-xl-12">
                                                <div class="card border-0 shadow" style="background-color: rgb(23,43,77);">
                                                    <div align="center">
                                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div runat="server" id="EDU1" style="width: 140px; cursor: pointer;" onclick="div_Click('EDU')"></div>
                                                                <div runat="server" id="EDU2" style="width: 140px; cursor: pointer;" onclick="div_Click('EDU')"></div>
                                                                <div runat="server" id="EDU3" style="width: 140px; cursor: pointer;" onclick="div_Click('EDU')"></div>
                                                                <div runat="server" id="PROD1" style="width: 140px; cursor: pointer" onclick="div_Click('PROD')"></div>
                                                                <div runat="server" id="PROD2" style="width: 140px; cursor: pointer" onclick="div_Click('PROD')"></div>
                                                                <div runat="server" id="PROD3" style="width: 140px; cursor: pointer" onclick="div_Click('PROD')"></div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnChart" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnDropdown" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-xl-12">
                                                        <div class="dropdown mb-2 mt-1">
                                                            <button class="btn dropdown-toggle btn-outline-primary" style="width: 100%; height: 35px; background-color: rgb(23,43,77); color: white" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <div style="float: left;"><i class="far fa-list-alt" style="font-size: 15px;"></i></div>
                                                                <div style="float: left; padding-left: 10px; padding-top: 3px">
                                                                    <h6 style="font-size: 15px;">도메인 목록</h6>
                                                                </div>
                                                            </button>
                                                            <div runat="server" id="dropDomainList" class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card border-0 mb-3" style="background-color: rgb(245,247,251); color: white;">
                                <div class="card-header flex-row align-items-center justify-content-between gradient-color-3" style="color: white; background-color: #18AE78">
                                    <h6 class="m-0 font-weight-bold text" style="font-size: 13px;"><i class="fas fa-cogs"></i> 외부 수집 데이터</h6>
                                </div>
                                <div class="card-body" style="color: black; width: 100%">
                                    <div class="row">
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-primary shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('PRIV_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-primary text-uppercase mb-1" style="font-size: 13px">
                                                                도메인 종류
                                                            </div>
                                                            <div runat="server" id="ApiTableCount" class="h5 mb-0 font-weight-bold text-gray-800">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-primary" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-database" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="ApiTableCountAdd" class="mt-3 mb-0 text-sm">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-success shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('PRIV_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-success text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 건수
                                                            </div>
                                                            <div runat="server" id="ApiRowCount" class="h5 mb-0 font-weight-bold text-gray-800">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-success" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-list-ol" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="ApiRowCountAdd" class="mt-3 mb-0 text-sm">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-warning shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('PRIV_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-warning text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 용량
                                                            </div>
                                                            <div runat="server" id="ApiTableSize" class="h5 mb-0 font-weight-bold text-gray-800"></div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-warning" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="far fa-hdd" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="ApiTableSizeAdd" class="mt-3 mb-0 text-sm"></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="card border-0 mb-3" style="background-color: rgb(245,247,251); color: white">
                                <div class="card-header flex-row align-items-center justify-content-between gradient-color-4" style="color: white; background-color: #1294AB">
                                    <h6 class="m-0 font-weight-bold text" style="font-size: 13px;"><i class="fas fa-sitemap"></i> 연계 데이터</h6>
                                </div>
                                <div class="card-body" style="color: black; width: 100%">
                                    <div class="row">
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-primary shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('CONT_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-primary text-uppercase mb-1" style="font-size: 13px">
                                                                도메인 종류
                                                            </div>
                                                            <div runat="server" id="LinkTableCount" class="h5 mb-0 font-weight-bold text-gray-800">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-primary" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-database" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="LinkTableCountAdd" class="mt-3 mb-0 text-sm">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-success shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('CONT_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-success text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 건수
                                                            </div>
                                                            <div runat="server" id="LinkRowCount" class="h5 mb-0 font-weight-bold text-gray-800">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-success" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="fas fa-list-ol" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="LinkRowCountAdd" class="mt-3 mb-0 text-sm">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 mb-3">
                                            <div class="card border-left-warning shadow h-100 py-2">
                                                <div class="card-body" style="cursor: pointer" onclick="div_Click('CONT_D')">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="font-weight-bold text-warning text-uppercase mb-1" style="font-size: 13px">
                                                                데이터 용량
                                                            </div>
                                                            <div runat="server" id="LinkTableSize" class="h5 mb-0 font-weight-bold text-gray-800">
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <div class="icon icon-shape text-white rounded-circle shadow bg-gradient-warning" style="display: inline-block; background-color: black; border-radius: 50%">
                                                                <i class="far fa-hdd" style="width: 40px; height: 40px; padding-top: 10px; font-size: 20px; color: white; vertical-align: middle; text-align: center;"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p runat="server" id="LinkTableSizeAdd" class="mt-3 mb-0 text-sm">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card border-0 mb-3" style="background-color: rgb(245,247,251); color: black;" runat="server" id="dvGrid">
                                <div class="card-header border-0 flex-row align-items-center justify-content-between gradient-color-1" style="color: white; background-color: rgb(22,87,217)">
                                    <h6 class="m-0 font-weight-bold text" style="font-size: 13px;"><i class="fas fa-book-open"></i> 데이터 상세</h6>
                                </div>
                                <div class="card-body">
                                    <asp:GridView ID="grdDomain" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-centered mb-0" PagerStyle-HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="grdDomain_PageIndexChanging">
                                        <HeaderStyle Font-Bold="true" Font-Size="13px" ForeColor="Black" BackColor="#ECECED" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField HeaderText="도메인 명" DataField="DATA_SE_NAME" ItemStyle-Width="200px" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="테이블 명" DataField="TABLE_NAME_KOR" ItemStyle-Width="250px" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="데이터 건수" DataField="ROWS" ItemStyle-Width="150px" DataFormatString="{0:n0}" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="데이터 용량(KB)" DataField="TABLE_SIZE" ItemStyle-Width="150px" DataFormatString="{0:#,###}" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="설명" DataField="TABLE_DESCRIPTION" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="수집 주기" DataField="CYCLE_NAME" ItemStyle-Width="150px" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-centered mb-0" PagerStyle-HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="grdDetail_PageIndexChanging">
                                        <HeaderStyle Font-Bold="true" Font-Size="13px" ForeColor="Black" BackColor="#ECECED" HorizontalAlign="Center" />
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="상태">
                                                <ItemStyle Width="110px" />
                                                <ItemTemplate>
                                                    <div id="dvStatus" class="circleDetail" style='<%#Eval("STATUS")%>; float: left' onclick="Status_Click('<%#Eval("TABLE_NAME")%>')"></div>
                                                    <div style="float: left; cursor: pointer; padding-left: 5px" onclick="Status_Click('<%#Eval("TABLE_NAME")%>')"><%#Eval("STATUS_NAME")%></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="데이터셋 명" DataField="TABLE_NAME_KOR" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                                            <asp:BoundField HeaderText="데이터 건수" DataField="ROWS" ItemStyle-Width="150px" DataFormatString="{0:#,###}" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="데이터 용량(KB)" DataField="TABLE_SIZE" ItemStyle-Width="150px" DataFormatString="{0:#,###}" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="설명" DataField="TABLE_DESCRIPTION" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField HeaderText="수집 주기" DataField="CYCLE_NAME" ItemStyle-Width="150px" ItemStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnValue" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <footer class="sticky-footer" style="background-color: black">
            <div class="container my-auto">
                <div class="copyright text-center my-auto">
                    <span style="color: white">Copyright © 2019 Korea Productivity Center. All Rights Reserved.</span>
                </div>
            </div>
        </footer>

        <a class="scroll-to-top rounded" href="#page-top">
            <i class="fas fa-angle-up"></i>
        </a>

        <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Ready to Leave?</h5>
                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">Select "Logout" below if you are ready to end your current session.</div>
                    <div class="modal-footer">
                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancel</button>
                        <a class="btn btn-primary" href="login.html">Logout</a>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/vendor/jquery/jquery.min.js"></script>
        <script src="../Scripts/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="../Scripts/vendor/jquery-easing/jquery.easing.min.js"></script>
        <script src="../Scripts/vendor/chart.js/Chart.min.js"></script>
        <script src="js/jquery.circliful.js"></script>
        <cc1:ModalPopupExtender ID="mpDetailLog" runat="server" PopupControlID="upDetailLog" TargetControlID="btnfake" CancelControlID="modalbtnClose" />

        <asp:UpdatePanel ID="upDetailLog" runat="server" Style="display: none; background-color: rgb(255, 242, 171); width: 500px; height: 300px; font-size: 12px" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group pass_show" style="height: 295px; color: black; overflow: auto; margin-bottom: 0px">
                    <asp:GridView runat="server" ID="grdDetailLog" AutoGenerateColumns="False" CssClass="table table-striped table-yellow">
                        <Columns>
                            <asp:TemplateField HeaderText="상태" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <div id="dvStatus" class="circleDetail" style='<%#Eval("STATUS")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="실행 일자" DataField="CREATE_DATE" ItemStyle-Width="200px" />
                            <asp:BoundField HeaderText="로그" DataField="ERROR_LOG" ItemStyle-Width="250px" />
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="modalbtnClose" runat="server" Text="닫기" CssClass="btn btn-block btn-large" OnClick="modalbtnClose_Click" Style="background-color: rgb(255, 235, 129); color: black;" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnScheStatus" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>