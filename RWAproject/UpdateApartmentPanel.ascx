<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateApartmentPanel.ascx.cs" Inherits="RWAproject.UpdatePanel" %>

<div class="offcanvas-header" id="popup-header">
    <h4 runat="server" class="offcanvas-title" id="offcanvasTitle">Edit apartment</h4>
    <asp:Button runat="server" OnClick="BtnClose_Click" OnClientClick="$('#reservation').prop('required', false);" type="button" class="btn-close" aria-label="Close"></asp:Button>
</div>
<div id="offcanvas" class="offcanvas-body small d-flex">
    <div style="margin-left: 1em; width: 15em">
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
            <asp:Button OnClick="BtnUpdate_Click" OnClientClick="CheckInput()" Style="width: 100%" ID="BtnUpdate" runat="server" type="button" class="btn btn-primary" Text="Update"></asp:Button>
        </div>
    </div>
    <div style="width: 15em">
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
            <asp:Button OnClick="BtnDelete_Click" Style="width: 100%" ID="BtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" runat="server" type="button" class="btn btn-danger" Text="Delete"></asp:Button>
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
            <input runat="server" id="Email" type="email" class="form-control" placeholder="2" required>
            <label for="Email">Email</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="PhoneNumber" type="text" class="form-control" placeholder="2" required>
            <label for="PhoneNumber">Phone number</label>
        </div>
    </div>
    <div style="width: 15em">
        <div>
            <label style="margin: 0">Select apartment tags:</label>
            <asp:Panel Style="max-height: 8em; overflow-y: scroll; overflow-x: hidden; margin-bottom: 1.2em" ScrollBars="Horizontal" runat="server" ID="TagsPanel">
            </asp:Panel>
        </div>
        <div class="form-floating">
            <textarea runat="server" class="form-control" placeholder="Leave a comment here" id="Details" style="height: 8em; resize: none"></textarea>
            <label for="Details">Details</label>
        </div>
    </div>
    <div style="margin-right: 1em; width: 15em">
        <div class="mb-3" id="apartment-images">
            <div id="carouselExampleCaptions" class="carousel slide overflow-hidden" data-bs-ride="false">
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="https://media.istockphoto.com/photos/europe-modern-complex-of-residential-buildings-picture-id1165384568?k=20&m=1165384568&s=612x612&w=0&h=CAnAr3uJtmkr0IQ2EPgm0IBoo8oCm-FEYEtyor8X_9I=" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Apartment front side</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="https://media.istockphoto.com/photos/exterior-view-of-modern-apartment-building-offering-luxury-rental-in-picture-id1322575582?b=1&k=20&m=1322575582&s=170667a&w=0&h=bGCtLpgCEorQuVdW2lbWguNZHcOGPePSwDibgbgyh0U=" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Another side of apartment</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="https://www.aveliving.com/AVE/media/Property_Images/Florham%20Park/hero/flor-apt-living-(2)-hero.jpg?ext=.jpg" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Apartment interior</h5>
                        </div>
                    </div>
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
            <div class="mt-2 d-flex">
                <asp:FileUpload runat="server" class="form-control" ID="FileUpload" Style="height: 100%" />
                <asp:Button runat="server" class="btn btn-success" Style="margin-left: .5em" ID="BtnAddFile" Text="Add file" OnClick="BtnAddFile_Click" />
            </div>
        </div>
    </div>
</div>
