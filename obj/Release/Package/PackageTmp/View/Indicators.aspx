<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Indicators.aspx.cs" Inherits="KPC_Monitoring.View.Indicators" %>

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
    <link href="../Scripts/css/sb-admin.css" rel="stylesheet">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link href="../Scripts/css/jquery-ui.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="../Scripts/css/googlefonts.css" rel="stylesheet">
    <link href="../Scripts/css/sb-admin-2.min.css" rel="stylesheet">
</head>
<body id="page-top">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager runat="server" ScriptMode="Release" />
        <nav class="navbar navbar-expand navbar-dark bg-dark static-top">
            <a class="navbar-brand mr-1" href="Monitoring">Monitoring</a>
            <i class="fas fa-bars text-white"></i>
            <div class="d-none d-md-inline-block form-inline ml-auto mr-0 mr-md-3 my-2 my-md-0">
                <a href="Login" id="userDropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    <i class="fas fa-fw fa-user" style="color: white"></i>
                </a>
                <span style="color: white; font-size: 14px" runat="server" id="sp_userName"></span>
            </div>
        </nav>
        <div id="wrapper">
            <ul id="SidebarUl" class="sidebar navbar-nav toggled">
                <li class="nav-item active" runat="server" id="M02">
                    <a class="nav-link" href="Monitoring">
                        <i class="fas fa-fw fa-tachometer-alt"></i>
                        <span>대시보드</span>
                    </a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="#" runat="server" id="M03">
                        <i class="fas fa-fw fa-copy"></i>
                        <span>성과지표</span>
                    </a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="CollectionModule" runat="server" id="M04">
                        <i class="fas fa-fw fa-wallet"></i>
                        <span>오픈 API 수집 모듈</span>
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
                                <label style="margin: 0px">성과지표</label>
                            </div>
                        </li>
                    </ol>
                    <div class="row"> 
                        <div class="col-md-12">
                            <div class="card border-0 mb-3" style="background-color:white">
                                <div class="card-header flex-row align-items-center">
                                    <h6 class="font-weight-bold text"><i class="fas fa-desktop"></i> 지표별 성과</h6>
                                </div>
                                <div class="card-body" style="background-color:green">
                                    <table>
                                        <tr>
                                            <td>
                                                신규 데이터
                                            </td>
                                            <td>
                                                <i class="far fa-edit"></i>
                                            </td>
                                            <td>
                                                프로그래스바
                                            </td>
                                            <td>
                                                <i class="fas fa-file-export"></i>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                데이터 생산
                                            </td>
                                            <td>
                                                <i class="far fa-edit"></i>
                                            </td>
                                            <td>
                                                프로그래스바
                                            </td>
                                            <td>
                                                <i class="fas fa-file-export"></i>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                데이터 개방
                                            </td>
                                            <td>
                                                <i class="far fa-edit"></i>
                                            </td>
                                            <td>
                                                프로그래스바
                                            </td>
                                            <td>
                                                <i class="fas fa-file-export"></i>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card border-0 mb-3" style="background-color:white">
                                <div class="card-header flex-row align-items-center">
                                    <h6 class="font-weight-bold text"><i class="fas fa-chart-line"></i> 지표명 : 신규 데이터 종수 (누적)</h6>
                                </div>
                                <div class="card-body" style="background-color:green">
                                    선그래프 / 센터단독, 센터융합 범례표시 / 연도별, 월별 버튼
                                </div>
                            </div>
                        </div>
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
        </div>
    </form>
</body>
</html>