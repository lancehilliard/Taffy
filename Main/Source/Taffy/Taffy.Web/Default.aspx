<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taffy.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/styles.css" />
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            urlInputTextBox = $("#<%= UrlInputTextBox.ClientID %>");
            urlInputTextBox.focus();
            urlInputTextBox.select();
        });
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>Taffy stretches the audio of your podcasts.  It's a "podcast proxy" that 
        downloads and stretches your podcast audio before giving it to your podcatcher.</p>
        <h3>Using Taffy</h3>
    <p>Your podcatcher has an RSS feed address for each of your podcasts.  To use Taffy for a podcast, give your podcatcher a Taffy URL for that podcast.</p>
        Turn your podcast's original RSS feed URL into a Taffy RSS feed URL:<br />
        <table>
        <tr>
        <td>Original RSS feed URL:
        </td>
        <td>
        <asp:TextBox runat="server" ID="UrlInputTextBox" Columns="60" />
    <asp:Button runat="server" ID="UrlEncodeButton" Text="Get Taffy URL" OnClick="UrlEncodeButton_Click" />
        </td>
        </tr>
        <tr>
        <td>Taffy RSS feed URL*:
        </td>
        <td>
        <asp:Label runat="server" ID="UrlEncodeResults" Text="Press the button to see your Taffy RSS feed URL here." />
        </td>
        </tr>
        <tr>
        <td>&nbsp;
        </td>
        <td>
        * Note: Your podcatcher may use a different hostname and port to access Taffy.
        </td>
        </tr>
        </table>
    </div>
    <!--
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
    -->
    
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
