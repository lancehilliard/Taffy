<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorReporting.aspx.cs" Inherits="Taffy.Web.ErrorReporting" MasterPageFile="~/Taffy.Master" %>
<asp:Content ID="Content1" Runat="Server" ContentPlaceHolderID="bodyContentPlaceHolder" >
<h3>Error Reporting</h3>
        <asp:Panel runat="server" ID="ElmahDisabledPanel">
        <p>Taffy uses <a href="http://code.google.com/p/elmah/" target="_blank">ELMAH</a> to report on errors, but it has to be enabled before it will capture information about errors.  If you think Taffy is experiencing errors, you can <a href="http://taffy.codeplex.com/wikipage?title=EnablingElmah" target="_blank">read about enabling ELMAH</a>.</p>
        </asp:Panel>
        <asp:Panel runat="server" ID="ElmahEnabledPanel">
        Taffy's ELMAH component is enabled.  You may <asp:HyperLink runat="server" ID="ElmahHyperLink" NavigateUrl="~/elmah.axd" Text="view recent errors" Target="_blank" /> at anytime.
        </asp:Panel>
</asp:Content>
