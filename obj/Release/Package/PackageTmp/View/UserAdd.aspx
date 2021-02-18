<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="KPC_Monitoring.View.UserAdd" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
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

    <script>
        function RadioCheck(value) {
            document.getElementById('<%=HdnRadio.ClientID%>').value = value;
        }
    </script>
</head>
<body id="page-top">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="HdnRadio" />
        <nav class="navbar navbar-expand navbar-dark bg-dark static-top">
            <a class="navbar-brand mr-1" href="Monitoring">Monitoring</a>
            <i class="fas fa-bars text-white"></i>
            <!-- Navbar Search -->
            <div class="d-none d-md-inline-block form-inline ml-auto mr-0 mr-md-3 my-2 my-md-0">
                <a href="Login" id="userDropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    <i class="fas fa-fw fa-user" style="color: white"></i>
                </a>
                <span style="color: white; font-size: 14px" runat="server" id="sp_userName"></span>
            </div>
        </nav>
        <div id="wrapper" style="width: 100%; word-break: break-all">
            <!-- Sidebar -->
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
            <div id="content-wrapper" style="background-color: rgb(43,43,43); min-width: 1660px">
                <div class="row" style="color: white; text-align: center">
                    <div class="col-md-3" style="margin-left: 15px">
                        <div class="card mb-3" style="background-color: rgb(245,247,251); color: white;">
                            <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                                <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">사용자 등록</h6>
                            </div>
                            <div class="card-body" style="color: black; width: 100%; font-size: 13px;">
                                <table style="height: 200px" align="center">
                                    <tr>
                                        <td>
                                            <label class="form-control-label" style="margin-top: 10px">사용자 ID</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="USER_ID" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="form-control-label" style="margin-top: 10px">사용자 이름</label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="USER_NAME" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="form-control-label" style="margin-top: 10px">권한</label>
                                        </td>
                                        <td>
                                            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/icheck-bootstrap@3.0.1/icheck-bootstrap.min.css" />
                                            <div style="float: left">
                                                <div class="radio icheck-success">
                                                    <input type="radio" id="success1" name="success" onclick="RadioCheck('A01');" />
                                                    <label for="success1" class="form-control-label">관리자</label>
                                                </div>
                                            </div>
                                            <div style="float: left; padding-left: 15px">
                                                <div class="radio icheck-success">
                                                    <input type="radio" checked id="success2" name="success" onclick="RadioCheck('A02');" />
                                                    <label for="success2" class="form-control-label">연구원</label>
                                                </div>
                                            </div>
                                            <div style="float: left; padding-left: 15px">
                                                <div class="radio icheck-success">
                                                    <input type="radio" checked id="success3" name="success" onclick="RadioCheck('A03');" />
                                                    <label for="success3" class="form-control-label">일반사용자</label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <label for="success1" class="form-control-label">※초기 비밀번호는 ID와 같습니다.&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                <asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_Click" Text="등록" CssClass="btn btn-icon btn-fab btn-success" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="card mb-3" style="background-color: rgb(245,247,251); color: white;">
                                    <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                                        <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">관리자</h6>
                                    </div>
                                    <div id="container1" class="card-body box-container" style="color: black; width: 100%; font-size: 13px;">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="card mb-3" style="background-color: rgb(245,247,251); color: white;">
                                    <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                                        <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">연구원</h6>
                                    </div>
                                    <div id="container2" class="card-body box-container" style="color: black; width: 100%; font-size: 13px;">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="card mb-3" style="background-color: rgb(245,247,251); color: white;">
                                    <div class="card-header flex-row align-items-center justify-content-between bg-gradient-primary" style="color: white; background-color: rgb(22,87,217)">
                                        <h6 class="m-0 font-weight-bold text" style="font-size: 13px;">일반사용자</h6>
                                    </div>
                                    <div id="container3" class="card-body box-container" style="color: black; width: 100%; font-size: 13px;">
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.3.min.js"></script>
                            <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>
                            <style>
                                /* Styles go here */
                                .box-item {
                                    width: 100%;
                                    z-index: 1000;
                                }
                            </style>
                            <script>
                                $(document).ready(function () {
                                    $('.box-item').draggable({
                                        cursor: 'move',
                                        helper: "clone"
                                    });

                                    $("#container1").droppable({
                                        drop: function (event, ui) {
                                            var itemid = $(event.originalEvent.toElement).attr("itemid");
                                            $('.box-item').each(function () {
                                                if ($(this).attr("itemid") === itemid) {
                                                    $(this).appendTo("#container1");
                                                }
                                            });
                                        }
                                    });

                                    $("#container2").droppable({
                                        drop: function (event, ui) {
                                            var itemid = $(event.originalEvent.toElement).attr("itemid");
                                            $('.box-item').each(function () {
                                                if ($(this).attr("itemid") === itemid) {
                                                    $(this).appendTo("#container2");
                                                }
                                            });
                                        }
                                    });

                                    $("#container3").droppable({
                                        drop: function (event, ui) {
                                            var itemid = $(event.originalEvent.toElement).attr("itemid");
                                            $('.box-item').each(function () {
                                                if ($(this).attr("itemid") === itemid) {
                                                    $(this).appendTo("#container3");
                                                }
                                            });
                                        }
                                    });
                                });
                            </script>
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
            </div>
        </div>
        <a class="scroll-to-top rounded" href="#page-top">
            <i class="fas fa-angle-up"></i>
        </a>
    </form>
</body>
</html>