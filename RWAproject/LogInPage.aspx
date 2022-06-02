<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPage.Master" CodeBehind="LogInPage.aspx.cs" Inherits="RWAproject.LogInPage" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <main class="form-signin w-100 m-auto text-center" style="width: 15vw !important">
        <div>
            <div runat="server" id="ShowAlert" visible="false">
                <div class="animate__animated animate__headShake d-flex" id="div-alert">
                    <img id="alert" src="https://cdn-icons-png.flaticon.com/512/1828/1828843.png" />
                    <h5>Incorrect username or password</h5>
                </div>
            </div>
            <img class="mb-4" src="/icons/users.png" style="width: 10em;">
            <h1 class="h3 mb-3 font-weight-bold">Sign in</h1>
            <div class="form-floating mb-3">
                <input runat="server" type="email" class="form-control" id="txtEmail" placeholder="name@example.com" required>
                <label for="txtEmail">Email address</label>
            </div>
            <div class="form-floating mb-3">
                <input runat="server" type="password" class="form-control" id="txtPassword" placeholder="Password" required>
                <label for="txtPassword">Password</label>
            </div>
            <asp:Button runat="server" OnClick="BtnSignIn_Click" class="w-100 btn btn-lg btn-primary" type="submit" Text="Sign in"></asp:Button>
        </div>
    </main>
</asp:Content>
