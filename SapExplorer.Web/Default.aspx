<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SapExplorer.Web._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-inline">
        <div class="form-group">
            <asp:DropDownList ID="ddlDestination" CssClass="form-control" runat="server" />
            <asp:TextBox ID="txtFunction" CssClass="form-control" runat="server" />
            <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-default" runat="server" />
        </div>
    </div>
    <asp:GridView ID="grid" runat="server">
        <Columns>
        </Columns>
    </asp:GridView>
</asp:Content>
