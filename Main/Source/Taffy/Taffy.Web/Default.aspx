<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taffy.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
    There are <a href="http://www.google.com/search?q=urlencode+online" target="_blank">several online URL encoders</a> that can help you URL-encode your feed's source URL.
    </div>
    <div>
    TODO: Documentation / Welcome
    </div>
    Dependencies:
    <table>
    <tr>
    <th>Dependency</th>
    <th>Found?</th>
    <th>Configured Path</th>
    </tr>
    <tr>
    <td>mpg123.exe</td>
    <td><asp:Label runat="server" ID="Mpg123ExistsLabel" /></td>
    <td><asp:Label runat="server" ID="Mpg123ConfiguredPathLabel" /></td>
    </tr>
    <tr>
    <td>soundstretch.exe</td>
    <td><asp:Label runat="server" ID="SoundstretchExistsLabel" /></td>
    <td><asp:Label runat="server" ID="SoundstretchConfiguredPathLabel" /></td>
    </tr>
    <tr>
    <td>lame.exe</td>
    <td><asp:Label runat="server" ID="LameExistsLabel" /></td>
    <td><asp:Label runat="server" ID="LameConfiguredPathLabel" /></td>
    </tr>
    </table>
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
    </form>
</body>
</html>
