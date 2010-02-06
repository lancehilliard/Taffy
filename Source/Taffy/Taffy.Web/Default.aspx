<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taffy.Web._Default" MasterPageFile="~/Taffy.Master" %>
<%@ Import Namespace="Taffy.Configuration"%>
<asp:Content ID="Content1" Runat="Server"
             ContentPlaceHolderID="bodyContentPlaceHolder">
<p>This is the homepage of your Taffy server.</p>
<p>Do you have an opinion about what should be displayed on this (or any other) page?</p>
<p><a href="http://taffy.codeplex.com/Thread/List.aspx" target="_blank">Come tell us</a>, and we'll consider it for future versions of Taffy.</p>
<p>Regardless, do be sure to familiarize yourself with all of the links at the top of the page!</p>
<asp:Panel runat="server" ID="LameMissingContainer">
<p>It looks like you haven't setup the LAME MP3 Encoder yet.  This is a <b>very important step</b>, and you must complete it before your Taffy server will operate properly.  Click on the "<%= Messages.LameAdvisoryPanelTitle %>" section on the right before you use Taffy!</p>
</asp:Panel>
</asp:Content>
