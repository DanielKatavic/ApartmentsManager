<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdatePanel.ascx.cs" Inherits="RWAproject.UpdatePanel" %>

<div class="offcanvas-header" id="popup-header">
    <h4 runat="server" class="offcanvas-title" id="offcanvasTitle">Edit apartment</h4>
    <asp:Button runat="server" OnClick="BtnClose_Click" type="button" class="btn-close" aria-label="Close"></asp:Button>
</div>
<div id="offcanvas" class="offcanvas-body small d-flex justify-content-center">
    <div class="row">
        <div class="form-floating mb-3">
            <input runat="server" id="totalRooms" type="number" min="0" class="form-control" placeholder="2">
            <label for="totalRooms">Number of rooms</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="maxAdults" type="number" min="0" class="form-control" placeholder="2">
            <label for="maxAdults">Max adults</label>
        </div>
        <div class="form-floating mb-3">
            <asp:DropDownList OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" runat="server" class="form-select" aria-label="Floating label select example" ID="DdlStatus">
            </asp:DropDownList>
            <label for="options">Select availability options</label>
        </div>
        <div>
            <asp:Button OnClick="BtnUpdate_Click" Style="width: 100%" ID="BtnUpdate" runat="server" type="button" class="btn btn-primary" Text="Update"></asp:Button>
        </div>
        <div>
            <%--<asp:Panel class="form-select" runat="server" ID="TagsPanel"></asp:Panel>--%>
            <div class="input-group">
                <asp:CheckBox class="input-group-text align-items-baseline" runat="server"/>
                <label class="form-control">Ime apartmana</label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="form-floating mb-3">
            <input runat="server" id="maxChildren" type="number" min="0" class="form-control" placeholder="2">
            <label for="maxChildren">Max children</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="distanceFromSea" type="number" min="0" class="form-control" placeholder="2">
            <label for="distanceFromSea">Distance from sea in m</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="reservation" type="text" min="0" class="form-control" placeholder="2">
            <label for="reservation">Name of person</label>
        </div>
        <div>
            <asp:Button OnClick="BtnDelete_Click" Style="width: 100%" id="BtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" runat="server" type="button" class="btn btn-danger" Text="Delete"></asp:Button>
        </div>
    </div>
    <div class="row">
        <div class="mb-3" id="apartment-images">
            <asp:Image ID="ApartmentImage" runat="server" ImageUrl="https://www.apartmanija.hr/slike/apartments/Apartman_Tanja_a5a97d0bb805.jpg" />
            <input class="form-control" type="file" id="formFileMultiple" multiple>
        </div>
    </div>
</div>
