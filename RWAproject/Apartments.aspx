<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPage.Master" CodeBehind="Apartments.aspx.cs" Inherits="RWAproject.Apartments" %>

<%@ Register TagName="UpdatePanel" TagPrefix="ascx" Src="~/UpdatePanel.ascx" %>

<asp:Content ID="ApartmentsContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container d-flex ">
        <div class="container d-flex" id="sorting-div">
            <div class="d-flex me-auto">
                <div class="d-flex align-items-center" id="SortByStatus">
                    <label for="status">Status:</label>
                    <asp:DropDownList class="form-select" runat="server" ID="ddlStatus" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="d-flex align-items-center" id="SortByCity">
                    <label for="city">City:</label>
                    <asp:DropDownList class="form-select" runat="server" ID="ddlCity" OnSelectedIndexChanged="DdlCity_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="ms-auto">
                <div class="d-flex align-items-center" id="SortBy">
                    <label for="sortBy">Sort by:</label>
                    <asp:DropDownList class="form-select" runat="server" ID="ddlSortBy" OnSelectedIndexChanged="DdlSortBy_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="numberOfRooms">Number of rooms</asp:ListItem>
                        <asp:ListItem Value="numberOfSpace">Number of space</asp:ListItem>
                        <asp:ListItem Value="price">Price</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-6" id="table-div">
            <asp:Repeater ID="rptApartments" runat="server">
                <HeaderTemplate>
                    <table class="table" id="apartmentsTable">
                        <thead>
                            <tr style="text-align: center">
                                <th scope="col">Name</th>
                                <th scope="col">City</th>
                                <th scope="col">Adults</th>
                                <th scope="col">Children</th>
                                <th scope="col">Rooms</th>
                                <th scope="col">Pictures</th>
                                <th scope="col">Price</th>
                                <th scope="col">Status</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="text-align: center">
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.Name)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.CityName)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.MaxAdults)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.MaxChildren)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.TotalRooms)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.PicturesCount)) %></td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.Price)) %> €</td>
                        <td scope="row"><%# Eval(nameof(DataLayer.Models.Apartment.Status)) %></td>
                        <td>
                            <asp:LinkButton OnClick="LinkButton_Click" CommandArgument="<%# Eval(nameof(DataLayer.Models.Apartment.Guid)) %>" class="btn btn-primary" runat="server">Open</asp:LinkButton>
                            <asp:LinkButton OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument="<%# Eval(nameof(DataLayer.Models.Apartment.Guid)) %>" class="btn btn-danger" runat="server"><svg id="delete-icon"></svg></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div style="text-align: right; margin-right: 2.5em">
                <asp:Button OnClick="BtnAdd_Click" runat="server" type="button" class="btn btn-success" Text="Add apartment"></asp:Button>
            </div>
        </div>
        <asp:Panel runat="server" ID="ApartmentsPanel" Visible="false">
            <div class="container animate__animated animate__slideInDown" id="popup">
                <ascx:UpdatePanel ID="updatePanel" runat="server" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
