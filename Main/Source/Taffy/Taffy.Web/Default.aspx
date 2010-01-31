<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taffy.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/styles.css" />
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            urlInputTextBox = $("#<%= UrlInputTextBox.ClientID %>");
            urlInputTextBox.focus(function() { $(this).select(); });
            urlInputTextBox.focus();

            urlResultsTextBox = $("#<%= UrlEncodeResults.ClientID %>");
            urlResultsTextBox.focus(function() { $(this).select(); });
        });

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
    <h1>Welcome to Taffy!</h1>
    <asp:Panel runat="server" ID="LameNotInstalledContainer" CssClass="warning">
    <h3>Before you begin!</h3>
    <p>Taffy needs the LAME MP3 Encoder to operate, and it can't find it. <asp:HyperLink runat="server" ID="LameDownloadHyperLink" Text="Download LAME" Target="_blank" />.  It comes as a zip file, and the only file you need from that zip file is lame.exe, which you should copy to the following location:</p>
    <p><asp:Label runat="server" ID="LameConfiguredPathLabel" /></p>
    <p>Put lame.exe in the above location and <asp:LinkButton runat="server" ID="ReloadLinkButton" Text="reload this page" /> to make this message go away.</p>
    </asp:Panel>
<div class="tipBox">Visit <a href="http://taffy.codeplex.com">the Taffy homepage</a> to check for updates and learn more about how it works.</div>
        
        <div>
        
        <asp:Panel runat="server" ID="TransformInput">
        
        </asp:Panel>
        
        </div>
        <p>Turn your podcast's original RSS feed into a Taffy Feed:</p>
        <table>
        <tr>
        <td><nobr>Original RSS URL:</nobr></td>
        <td>
        <asp:TextBox runat="server" ID="UrlInputTextBox" CssClass="urlTextBox" />
    <asp:Button runat="server" ID="UrlEncodeButton" Text="Create Taffy Feed" OnClick="UrlEncodeButton_Click" />
        </td>
        </tr>
        <tr>
        <td>Taffy Feed:
        </td>
        <td>
        <asp:TextBox runat="server" ID="UrlEncodeResults" CssClass="urlEncodedTextBox" />
        </td>
        </tr>
        </table>
        <p>By giving your podcatcher* the Taffy Feed, you'll start hearing the audio of your podcasts at the new speed.</p>
        <p>* Note: When you give a Taffy Feed to your podcatcher, make sure you use the hostname/port at which it can access Taffy.</p>
        <asp:Panel runat="server" ID="ElmahDisabledPanel">
        <p>Taffy uses ELMAH to report on errors, but it has to be enabled before it will capture information about errors.  If you think Taffy is experiencing errors, you can <a href="http://taffy.codeplex.com/wikipage?title=EnablingElmah">read about enabling ELMAH</a>.</p>
        </asp:Panel>
        <asp:Panel runat="server" ID="ElmahEnabledPanel">
        Taffy's ELMAH component is enabled.  You may <asp:HyperLink runat="server" ID="ElmahHyperLink" NavigateUrl="~/elmah.axd" Text="view recent errors" /> at anytime.
        </asp:Panel>
        
    </div>
    
<%--
    Configuration:
    <table>
    <tr>
    <th>Setting</th>
    <th>Value</th>
    </tr>
    <tr>
    <td>Hours to cache stretched podcasts:</td>
    <td><asp:Label runat="server" ID="NumberOfHoursToCacheStretchedPodcastsLabel" /></td>
    </tr>
    <tr>
    <td>Transformer type used to stretch podcasts:</td>
    <td><asp:Label runat="server" ID="TransformerTypeLabel" /></td>
    </tr>
    </table>
    <table>
    <tr>
    <th>Dependency</th>
    <th>Found?</th>
    <th>Configured Path</th>
    <th>&nbsp;</th>
    </tr>
    <tr>
    <td>mpg123.exe</td>
    <td><asp:Label runat="server" ID="Mpg123ExistsLabel" /></td>
    <td><asp:Label runat="server" ID="Mpg123ConfiguredPathLabel" /></td>
    <td><asp:HyperLink runat="server" ID="Mpg123DownloadHyperLink" Text="Download" Target="_blank" /></td>
    </tr>
    <tr>
    <td>soundstretch.exe</td>
    <td><asp:Label runat="server" ID="SoundstretchExistsLabel" /></td>
    <td><asp:Label runat="server" ID="SoundstretchConfiguredPathLabel" /></td>
    <td><asp:HyperLink runat="server" ID="SoundstretchDownloadHyperLink" Text="Download" Target="_blank" /></td>
    </tr>
    <tr>
    <td>lame.exe Installed?</td>
    <td><asp:Label runat="server" ID="LameExistsLabel" /></td>
    <td>Place here: </td>
    <td></td>
    </tr>
    </table>
    --%>
    
    <!--
        <div>
        <asp:Label runat="server" ID="FeedPageUrlLabel" />
    <br />
    <asp:Label runat="server" ID="SourceUrlHelpLabel" />
    <br />
    Example feed source URL:
    <br />
    Old: <asp:Label runat="server" ID="SourceUrlExampleLabel" />
    <br />
    New: <asp:Label runat="server" ID="SourceUrlEncodedExampleLabel" />
    <br />

    </div>
    <div>
    TODO: Documentation / Welcome
    </div>

    -->
    </form>
</body>
</html>
