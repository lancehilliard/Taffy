<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="Taffy.Web.Preferences" MasterPageFile="~/Taffy.Master" %>
<asp:Content ID="Content1" Runat="Server" ContentPlaceHolderID="bodyContentPlaceHolder">
<h3>Preferences</h3>
<p>This page enables you to change certain aspects about how Taffy functions.</p>
<table>
<tr>
<td class="preferencesCheckBoxInput"><asp:CheckBox runat="server" ID="ErrorFeedbackEnabledCheckBox" /></td>
<td><b>Error Reporting</b><br />When enabled, this feature sends anonymous error information to the developers automatically.</td>
</tr>
<tr>
<td class="preferencesCheckBoxInput"><asp:CheckBox runat="server" ID="AppendOriginalAudioEnabledCheckBox" /></td>
<td><b>Append Original Audio</b><br />When enabled, this feature appends the podcast's original audio to the end of the stretched audio -- for every podcast Taffy serves.  This is useful when you come across an episode that just doesn't listen well at your faster speed (note: this will increase download times and disk usage on your podcatcher).</td>
</tr>
</table>
<hr />
<div class="preferencesSaveContainer">
    <asp:Button runat="server" ID="SaveButton" OnClick="SaveButton_Click" Text="Save Preferences" />
</div>

</asp:Content>