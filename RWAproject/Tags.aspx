<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPage.Master" CodeBehind="Tags.aspx.cs" Inherits="RWAproject.Tags" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container d-flex">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                        <table id="myTable" class="table table-bordered">
                            <thead>
                                <tr style="text-align: center">
                                    <th scope="col">Name</th>
                                    <th scope="col">Count</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="text-align: center">
                            <td scope="row"><%# Eval(nameof(DataLayer.Models.Tag.Name)) %></td>
                            <td><%# Eval(nameof(DataLayer.Models.Tag.Count)) %></td>
                            <td>
                                <asp:LinkButton CommandArgument="<%# Eval(nameof(DataLayer.Models.Tag.Name)) %>" ID="LinkButton1" runat="server">Select</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
