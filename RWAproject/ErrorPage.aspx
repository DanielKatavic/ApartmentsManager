<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="RWAproject.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <link href="css/ErrorPage.css" rel="stylesheet" />
</head>
<body style="background-color: #e3f2fd">
    <form id="form1" runat="server">
        <main>
            <div id="error-div">
                <h1>404</h1>
                <h3>Page not found</h3>
                <p>
                    An error occurred while loading this page.
                    <br />
                    Please try again.
                </p>
                <div class="button">
                    <asp:Button class="btn-hover color-9" ID="BtnTryAgain" runat="server" Text="Try again" OnClick="BtnTryAgain_Click" />
                </div>
            </div>
        </main>
    </form>
</body>
</html>
