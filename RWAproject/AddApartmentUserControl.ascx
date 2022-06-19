<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddApartmentUserControl.ascx.cs" Inherits="RWAproject.AddApartmentUserControl" %>

<div class="offcanvas-header" id="popup-header">
    <h4 runat="server" class="offcanvas-title" id="offcanvasTitle">Add apartment</h4>
    <asp:Button runat="server" OnClick="BtnClose_Click" OnClientClick="$('#reservation').prop('required', false);" type="button" class="btn-close" aria-label="Close"></asp:Button>
</div>
<div id="offcanvas" class="offcanvas-body small d-flex justify-content-center">
    <div>
        <div class="form-floating mb-3">
            <input runat="server" id="name" type="text" class="form-control" placeholder="2">
            <label for="name">Apartment name</label>
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
            <select name="status" id="status" class="form-select" aria-label="Floating label select example">
                <option value="<%: DataLayer.Models.Status.Occupied %>" selected><%: DataLayer.Models.Status.Occupied %></option>
                <option value="<%: DataLayer.Models.Status.Vacant %>"><%: DataLayer.Models.Status.Vacant %></option>
                <option value="<%: DataLayer.Models.Status.Reserved %>"><%: DataLayer.Models.Status.Reserved %></option>
            </select>
            <label for="status">Select availability options</label>
        </div>
        <div>
            <asp:Button OnClick="BtnAdd_Click" OnClientClick="CheckInput()" Style="width: 100%; margin-bottom: 1em" ID="BtnAdd" runat="server" type="button" class="btn btn-primary" Text="Add apartment"></asp:Button>
        </div>
    </div>
    <div>
        <div class="form-floating mb-3">
            <input runat="server" id="city" type="text" class="form-control" placeholder="2">
            <label for="city">City name</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="maxChildren" type="number" min="0" class="form-control" placeholder="2">
            <label for="maxChildren">Max children</label>
        </div>
        <div class="form-floating mb-3">
            <input runat="server" id="distanceFromSea" type="number" min="0" class="form-control" placeholder="2">
            <label for="distanceFromSea">Distance from sea in m</label>
        </div>
        <div class="form-floating mb-3">
            <input id="reservation" type="text" class="form-control" placeholder="2">
            <label for="reservation">Name of person</label>
        </div>
        <div class="form-floating mb-3">
            <input id="price" type="number" min="0" class="form-control" placeholder="2">
            <label for="price">Price in €</label>
        </div>
    </div>
    <div>
        <div>
            <asp:Panel Style="max-height: 19em; overflow-y: scroll; overflow-x: hidden" ScrollBars="Horizontal" runat="server" ID="TagsPanel">
            </asp:Panel>
        </div>
    </div>
    <div>
        <div class="mb-3" id="apartment-images">
            <div id="carouselExampleCaptions" class="carousel slide overflow-hidden" data-bs-ride="false">
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="https://media.istockphoto.com/photos/europe-modern-complex-of-residential-buildings-picture-id1165384568?k=20&m=1165384568&s=612x612&w=0&h=CAnAr3uJtmkr0IQ2EPgm0IBoo8oCm-FEYEtyor8X_9I=" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <asp:TextBox class="image-desc" runat="server" ID="ApartmentDesc" placeholder="IMAGE DESCRIPTION" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="https://media.istockphoto.com/photos/exterior-view-of-modern-apartment-building-offering-luxury-rental-in-picture-id1322575582?b=1&k=20&m=1322575582&s=170667a&w=0&h=bGCtLpgCEorQuVdW2lbWguNZHcOGPePSwDibgbgyh0U=" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <asp:TextBox class="image-desc" runat="server" ID="TextBox1" placeholder="IMAGE DESCRIPTION" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="https://www.aveliving.com/AVE/media/Property_Images/Florham%20Park/hero/flor-apt-living-(2)-hero.jpg?ext=.jpg" class="d-block w-100" style="height: 17em">
                        <div class="carousel-caption d-none d-md-block">
                            <asp:TextBox class="image-desc" runat="server" ID="TextBox2" placeholder="IMAGE DESCRIPTION" ></asp:TextBox>
                        </div>
                    </div>
                    <asp:Literal runat="server" ID="ImagesLiteral">
                        
                    </asp:Literal>
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
            <div class="mt-3 d-flex">
                <asp:FileUpload runat="server" class="form-control" ID="FileUpload" Style="height: 100%" />
                <asp:Button runat="server" class="btn btn-success" Style="margin-left: .5em" ID="BtnAddFile" Text="Add file" OnClick="BtnAddFile_Click" />
            </div>
        </div>
    </div>
</div>
