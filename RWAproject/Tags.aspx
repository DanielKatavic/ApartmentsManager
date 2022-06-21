<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPage.Master" CodeBehind="Tags.aspx.cs" Inherits="RWAproject.Tags" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container d-flex pt-4">
        <div class="row justify-content-center" style="height: 60vh; width: 30em; overflow-y: scroll">
            <div class="col-md-6" style="width: 100%; height:100%;">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                        <table class="table" style="height: 50%">
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
                                <asp:Button class="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete?')" OnClick="BtnDeleteTag_Click" CommandArgument="<%# Eval(nameof(DataLayer.Models.Tag.Guid)) %>" runat="server" ID="BtnDeleteTag" Visible="false" Text="Delete"></asp:Button>
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
        <div style="text-align: right; width: 30em; margin-top: 2em">
            <asp:Button class="btn btn-success" runat="server" Style="margin-right: 1em" Text="Add new tag" OnClick="BtnShowPanel_Click"></asp:Button>
        </div>
    </div>
    <asp:Panel runat="server" ID="TagAddPanel" Visible="false">
        <div class="container animate__animated animate__slideInDown" id="popup" style="width: 20vw">
            <div class="offcanvas-header" id="popup-header">
                <h4 class="offcanvas-title">Add tag</h4>
                <asp:Button runat="server" OnClick="BtnClose_Click" OnClientClick="$('#ContentPlaceHolder_TagName').prop('required', false);" type="button" class="btn-close" aria-label="Close"></asp:Button>
            </div>
            <div style="padding: 1em">
                <div class="form-floating mb-3">
                    <input runat="server" id="TagName" type="text" class="form-control" placeholder="2" required>
                    <label for="TagName">Tag name</label>
                </div>
                <div>
                    <asp:DropDownList class="form-select" runat="server" ID="ddlTypeName" Style="margin: .5em 0 1em 0"></asp:DropDownList>
                </div>
                <div>
                    <asp:Button Style="width: 100%" ID="BtnAddTag" runat="server" type="button" class="btn btn-primary" OnClick="BtnAddTag_Click" Text="Add tag"></asp:Button>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
