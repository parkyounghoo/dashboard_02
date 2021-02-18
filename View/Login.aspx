<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KPC_Monitoring.View.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name="robots" content="noindex">

    <link href="../Scripts/css/login.css" rel="stylesheet" type="text/css">
</head>
<body>
    <div class="login">
         <h1>KPC 빅데이터플랫폼 상황판</h1>
        <form method="post" runat="server">
            <cc1:ToolkitScriptManager runat="server" />
            <asp:TextBox runat="server" ID="UserId" placeholder="Id"/>
            <asp:TextBox runat="server" ID="UserPw" placeholder="Password" TextMode="Password" />
            <asp:Button ID="btnShow" CssClass="btn btn-primary btn-block btn-large" runat="server" Text="Password Change" OnClientClick="modalTextReset()" Style="margin-bottom: 5px" />
            <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-primary btn-block btn-large" OnClientClick="return LoginValidate();" OnClick="btnLogin_Click" />

            <%--Modal Popup--%>
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
                CancelControlID="modalbtnClose">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="right" Style="display: none; background-color: ivory; width: 300px; height: 210px;">
                <div class="form-group pass_show">
                    <asp:TextBox runat="server" ID="modalId" placeholder="Id" />
                    <asp:TextBox runat="server" ID="modalCurrent" placeholder="Current Password" TextMode="Password" />
                    <asp:TextBox runat="server" ID="modalNew" placeholder="New Password" TextMode="Password" />
                </div>
                <asp:Button ID="modalbtnOk" runat="server" Text="Ok" OnClientClick="return modalChangeValidate();" OnClick="modalbtnOk_Click" CssClass="btn btn-primary btn-block btn-large" Style="margin-bottom: 5px" />
                <asp:Button ID="modalbtnClose" runat="server" Text="Close" CssClass="btn btn-primary btn-block btn-large" />
            </asp:Panel>
        </form>
    </div>
    <script type="text/javascript">
        //로그인 유효성 체크
        function LoginValidate() {
            if (document.getElementById('<%= UserId.ClientID %>').value == '') {
                alert('id를 입력해 주세요.');
                document.getElementById('<%= UserId.ClientID %>').focus();

                return false;
            }
            else if (document.getElementById('<%= UserPw.ClientID %>').value == '') {
                alert('pw를 입력해 주세요.');
                document.getElementById('<%= UserPw.ClientID %>').focus();

                return false;
            }
            else {
                return true;
            }
        }

        //모달 패스워드변경 유효성 체크
        function modalChangeValidate() {
            if (document.getElementById('<%= modalId.ClientID %>').value == '') {
                alert('id를 입력해 주세요.');
                document.getElementById('<%= modalId.ClientID %>').focus();

                return false;
            }
            else if (document.getElementById('<%= modalCurrent.ClientID %>').value == '') {
                alert('Current Password를 입력해 주세요.');
                document.getElementById('<%= modalCurrent.ClientID %>').focus();

                return false;
            }
            else if (document.getElementById('<%= modalNew.ClientID %>').value == '') {
                alert('New Password를 입력해 주세요.');
                document.getElementById('<%= modalNew.ClientID %>').focus();

                return false;
            }
            else {
                return true;
            }
        }

        //모달 팝업시에 그전에 입력한 값 초기 화
        function modalTextReset() {
            if (document.getElementById('<%= UserId.ClientID %>').value != '') {
                document.getElementById('<%= modalId.ClientID %>').value = document.getElementById('<%= UserId.ClientID %>').value;
            }
            else {
                document.getElementById('<%= modalId.ClientID %>').value = '';
            }
            document.getElementById('<%= modalCurrent.ClientID %>').value = '';
            document.getElementById('<%= modalNew.ClientID %>').value = '';
        }
</script>
</body>
</html>