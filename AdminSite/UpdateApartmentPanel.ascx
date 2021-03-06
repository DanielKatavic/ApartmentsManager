<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateApartmentPanel.ascx.cs" Inherits="RWAproject.UpdatePanel" %>

<div class="offcanvas-header" id="popup-header">
    <h4 runat="server" class="offcanvas-title" id="offcanvasTitle">Edit apartment</h4>
    <asp:Button runat="server" OnClick="BtnClose_Click" OnClientClick="removeRequiredTag()" type="button" class="btn-close" aria-label="Close"></asp:Button>
</div>
<div id="offcanvas" class="offcanvas-body small d-flex">
    <div style="margin-left: 1em; width: 15em">
        <div class="form-floating mb-3">
            <input runat="server" id="apartmentName" type="text" class="form-control" placeholder="2" required>
            <label for="apartmentName">Apartment name</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="totalRooms" type="number" min="0" class="form-control" placeholder="2">
            <label for="totalRooms">Number of rooms</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="maxAdults" type="number" min="0" class="form-control" placeholder="2">
            <label for="maxAdults">Max adults</label>
        </div>
        <div class="form-floating mb-3">
            <asp:DropDownList runat="server" ID="StatusDDL" class="form-select" OnSelectedIndexChanged="StatusDDL_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <label for="status">Options:</label>
        </div>
        <div>
            <asp:Button OnClick="BtnUpdate_Click" Style="width: 100%" ID="BtnUpdate" runat="server" type="button" class="btn btn-primary" Text="Update"></asp:Button>
        </div>
    </div>
    <div style="width: 15em">
        <div class="form-floating mb-3">
            <input runat="server" id="price" type="number" class="form-control" placeholder="2" required>
            <label for="price">Price</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="maxChildren" type="number" min="0" class="form-control" placeholder="2">
            <label for="maxChildren">Max children</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="distanceFromSea" type="number" min="0" class="form-control" placeholder="2">
            <label for="distanceFromSea">Distance from sea in m</label>
        </div>
        <label style="margin: 0">Select registered user:</label>
        <div class="input-group mb-3">
            <div class="input-group-text">
                <asp:CheckBox runat="server" ID="ChbRegisteredUser" OnCheckedChanged="ChbRegisteredUser_CheckedChanged" AutoPostBack="true" />
            </div>
            <asp:DropDownList runat="server" ID="UsersDDL" class="form-select" OnSelectedIndexChanged="UsersDDL_SelectedIndexChanged" AutoPostBack="true" />
        </div>
        <div>
            <asp:Button OnClick="BtnDelete_Click" Style="width: 100%; margin-bottom: 1em" ID="BtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" runat="server" type="button" class="btn btn-danger" Text="Delete"></asp:Button>
        </div>
    </div>
    <div style="width: 15em">
        <div class="form-floating mb-3">
            <input runat="server" id="Username" type="text" class="form-control" placeholder="2" required>
            <label for="Username">Username</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="Address" type="text" class="form-control" placeholder="2" required>
            <label for="Address">Address</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="Email" type="text" class="form-control" placeholder="2" required>
            <label for="Email">Email</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="PhoneNumber" type="text" class="form-control" placeholder="2" required>
            <label for="PhoneNumber">Phone number</label>
        </div>
    </div>
    <div style="width: 15em">
        <div>
            <label style="margin: 0">All apartment tags:</label>
            <asp:Panel Style="max-height: 8em; overflow-y: scroll; overflow-x: hidden; margin-bottom: 1.2em; display: flex; flex-direction: column" ScrollBars="Horizontal" runat="server" ID="TagsPanel">
                <asp:Repeater ID="TagsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="input-group">
                            <label class="form-control" style="display: flex; align-items: center" runat="server"><%# Eval(nameof(DataLayer.Models.Tag.Name)) %></label>
                            <asp:LinkButton ID="BtnRemoveTag" Style="padding: 0 .5em 0 .5em" runat="server" CommandArgument="<%# Eval(nameof(DataLayer.Models.Tag.Id)) %>" OnClick="BtnRemoveTag_Click" Text="Del" class="btn btn-danger" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
        <div class="input-group mb-3">
            <asp:DropDownList runat="server" ID="AllTagsDdl" class="form-select" />
            <asp:Button runat="server" ID="BtnAddTag" Text="Add" class="btn btn-success" OnClick="BtnAddTag_Click" />
        </div>
        <div class="form-floating">
            <textarea runat="server" class="form-control" placeholder="Leave a comment here" id="Details" style="height: 8em; resize: none"></textarea>
            <label for="Details">Details</label>
        </div>
    </div>
    <%--images--%>
    <div style="margin-right: 1em; width: 15em">
        <div class="mb-3" id="apartment-images">
            <div id="carouselExampleCaptions" class="carousel slide overflow-hidden" data-bs-ride="false">
                <div class="carousel-inner" id="image-wrapper">
                    <itemtemplate>
                        <div class="carousel-item active">
                            <asp:CheckBox runat="server" ID="FirstChbRepresentative" style="position: absolute; right: 1em; top: 1em; z-index: 1000;"/>
                            <img src="..." runat="server" id="FirstImage" class="d-block w-100" style="height: 17em">
                            <div class="carousel-caption d-none d-md-block">
                                <asp:TextBox class="image-desc" runat="server" ID="FirstImageDesc"></asp:TextBox>
                            </div>
                        </div>
                    </itemtemplate>
                    <asp:Repeater ID="ImagesRpt" runat="server">
                        <ItemTemplate>
                            <div class="carousel-item">
                                <asp:CheckBox runat="server" ID="ChbRepresentative" style="position: absolute; right: 1em; top: 1em; z-index: 1000" />
                                <img src="<%# Eval(nameof(DataLayer.Models.Image.Path)) %>" class="d-block w-100" style="height: 17em">
                                <div class="carousel-caption d-none d-md-block">
                                    <asp:TextBox class="image-desc" runat="server" ID="ImageDesc" Text="<%# Eval(nameof(DataLayer.Models.Image.Name)) %>"></asp:TextBox>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
            <div class="input-group mb-3">
                <asp:FileUpload runat="server" class="form-control" ID="FileUpload" Style="height: 100%" />
                <asp:Button runat="server" class="btn btn-success" ID="BtnAddImage" Text="Add file" OnClick="BtnAddImage_Click" />
            </div>
        </div>
    </div>
</div>