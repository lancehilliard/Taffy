<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LameSetup.aspx.cs" Inherits="Taffy.Web.LameSetup" MasterPageFile="~/Taffy.Master" %>
<asp:Content ID="Content1" Runat="Server" ContentPlaceHolderID="bodyContentPlaceHolder" >
    <asp:Panel runat="server" ID="LameInstalledContainer">
        The LAME MP3 Encoder is properly installed.
    </asp:Panel>
    <asp:Panel runat="server" ID="LameNotInstalledContainer">
        <p>Taffy needs the LAME MP3 Encoder to operate, and it can't find it. <asp:HyperLink runat="server" ID="LameDownloadHyperLink" Text="Download LAME" Target="_blank" />.  It comes as a zip file, and the only file you need from that zip file is lame.exe, which you should copy to the following location:</p>
        <p><asp:Label runat="server" ID="LameConfiguredPathLabel" /></p>
        <p>Put lame.exe in the above location and <asp:LinkButton runat="server" ID="ReloadLinkButton" Text="reload this page" /> to make this message go away.</p>
    </asp:Panel>
</asp:Content>
