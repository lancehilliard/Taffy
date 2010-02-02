<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateFeed.aspx.cs" Inherits="Taffy.Web.CreateFeed" MasterPageFile="~/Taffy.Master" %>
<asp:Content ID="HeaderContent"  runat="server" ContentPlaceHolderID="headContentPlaceHolder">
    <script src="js/jquery.watermarkinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            urlInputTextBox = $("#<%= UrlInputTextBox.ClientID %>");
            urlInputTextBox.focus(function() { $(this).select(); });
            urlInputTextBox.focus();

            urlResultsTextBox = $("#<%= UrlEncodeResults.ClientID %>");
            urlResultsTextBox.focus(function() { $(this).select(); });

            $("#<%= PodcatcherTaffyAddressTextBox.ClientID %>").Watermark("<%= PodcatcherTaffyAddressTextBoxWatermark %>");
            $("#<%= UrlInputTextBox.ClientID %>").Watermark("<%= UrlInputTextBoxWatermark %>");
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" Runat="Server" ContentPlaceHolderID="bodyContentPlaceHolder" >
<h3>Create Taffy Feed</h3>
<p>On this page, you can create Taffy Feeds for your podcasts.  By giving a Taffy Feed to your podcatcher, you'll hear your podcasts at your preferred speed.  You can create feeds individually or generate an <a href="http://en.wikipedia.org/wiki/OPML" target="_blank">OPML file</a> that's had all of its RSS URLs converted to Taffy Feeds.</p>
<fieldset><legend>Point Your Podcatcher to Taffy</legend>
<p class="podcatcherTaffyAddressAdvisory">IMPORTANT: It's important that this page generate Taffy Feed URLs that successfully point your podcatcher to this Taffy instance.  In the textbox below, provide the <b>Internet address your podcatcher uses to contact this Taffy instance:</b></p>
<div class="urlContainer"><span class="urlLabel">Taffy Address:</span><asp:TextBox runat="server" ID="PodcatcherTaffyAddressTextBox" CssClass="podcatcherTaffyAddressTextBox" /></div>
</fieldset>

<fieldset><legend>Create Single Taffy Feed</legend>
        <div class="urlContainer"><span class="urlLabel">Original RSS URL:</span><asp:TextBox runat="server" ID="UrlInputTextBox" CssClass="urlTextBox" /></div>
        <div class="urlContainer"><span class="urlLabel">Taffy Feed:</span><asp:TextBox runat="server" ID="UrlEncodeResults" CssClass="urlEncodedTextBox" /></div>
        </fieldset>

<fieldset><legend>Generate OPML File of Taffy Feeds</legend>
        <div class="urlContainer"><span class="urlLabel">Original OPML File: </span><asp:FileUpload runat="server" ID="OpmlFileUpload" CssClass="urlTextBox" /></div>
        <asp:Panel runat="server" ID="OpmlDownloadPanel" CssClass="urlContainer"><span class="urlLabel">Taffy OPML File: </span><asp:LinkButton runat="server" ID="OpmlDownloadLinkButton" OnClick="OpmlDownloadLinkButton_Click" Text="Download Taffy OPML File" /></asp:Panel>
        </fieldset>
<div align="right">
<asp:Button runat="server" ID="UrlEncodeButton" CssClass="urlButton" Text="Create Taffy Feed" OnClick="UrlEncodeButton_Click" />
</div>

</asp:Content>
