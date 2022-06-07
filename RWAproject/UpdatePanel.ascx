﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdatePanel.ascx.cs" Inherits="RWAproject.UpdatePanel" %>

<div class="offcanvas-header" id="popup-header">
    <h4 runat="server" class="offcanvas-title" id="offcanvasTitle">Edit apartment</h4>
    <asp:Button runat="server" OnClick="BtnClose_Click" OnClientClick="$('#reservation').prop('required', false);" type="button" class="btn-close" aria-label="Close"></asp:Button>
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
            <select name="status" id="status" class="form-select" aria-label="Floating label select example">
                <option value="<%: DataLayer.Models.Status.Occupied %>" selected><%: DataLayer.Models.Status.Occupied %></option>
                <option value="<%: DataLayer.Models.Status.Vacant %>"><%: DataLayer.Models.Status.Vacant %></option>
                <option value="<%: DataLayer.Models.Status.Reserved %>"><%: DataLayer.Models.Status.Reserved %></option>
            </select>
            <label for="status">Select availability options</label>
        </div>
        <div>
            <asp:Button OnClick="BtnUpdate_Click" OnClientClick="CheckInput()" Style="width: 100%" ID="BtnUpdate" runat="server" type="button" class="btn btn-primary" Text="Update"></asp:Button>
        </div>
        <div>
            <asp:Panel Style="max-height: 10em; margin: 2em 0 2em 0; padding: 1em 0 1em 0" ScrollBars="Horizontal" runat="server" ID="TagsPanel">
            </asp:Panel>
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
            <input id="reservation" type="text" min="0" class="form-control" placeholder="2">
            <label for="reservation">Name of person</label>
        </div>
        <div>
            <asp:Button OnClick="BtnDelete_Click" Style="width: 100%" ID="BtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" runat="server" type="button" class="btn btn-danger" Text="Delete"></asp:Button>
        </div>
    </div>
    <div class="row">
        <div class="mb-3" id="apartment-images">
            <asp:Image ID="ApartmentImage" runat="server" ImageUrl="https://www.apartmanija.hr/slike/apartments/Apartman_Tanja_a5a97d0bb805.jpg" />
            <input class="form-control" type="file" id="formFileMultiple" multiple>
        </div>
    </div>
</div>
