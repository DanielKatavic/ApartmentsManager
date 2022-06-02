<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPage.Master" CodeBehind="RegisteredUsers.aspx.cs" Inherits="RWAproject.RegisteredUsers" %>

<asp:Content ID="UsersContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container d-flex">
        <div class="col-md-6" id="table-div">
            <asp:Repeater ID="rptUsers" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr style="text-align: center">
                                <th scope="col">Username</th>
                                <th scope="col">Email</th>
                                <th scope="col">Phone number</th>
                                <th scope="col">Address</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="text-align: center">
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.User.UserName)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.User.Email)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.User.PhoneNumber)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.User.Address)) %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
