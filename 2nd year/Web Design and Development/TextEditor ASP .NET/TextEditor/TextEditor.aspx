<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextEditor.aspx.cs" Inherits="TextEditor.TextEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="myForm" enctype="multipart/form-data" runat="server">

        <asp:ScriptManager ID="MainScriptManager" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>

            <h1>SET Text Editor</h1> 
            
            <h4>Available Files</h4> 
            <asp:ListBox ID="dirListBox" runat="server" Height="104px" Width="274px"></asp:ListBox><br /><br />
            <asp:Button ID="openButton" runat="server" Text="Open" OnClick="openButton_Click" />
            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" />

            <span id="openErrorMsg" runat="server"></span>
                <asp:Button ID="UpdateBox" runat="server" OnClick="UpdateBox_Click" Text="Update" />
            <span id="saveErrorMsg" runat="server"></span>

            <br /><br />
            <asp:Button ID="saveAsButton" runat="server" Text="SaveAs" OnClick="saveAsButton_Click" />
            <asp:TextBox ID="saveAsTextBox" runat="server"></asp:TextBox>
            <span id="sugggestions" runat="server"></span>

            <br /><br />
            <textarea id="textPad" cols="70" rows="50" runat="server"></textarea>

            </ContentTemplate>        
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="updateTimer" runat="server">
            <ContentTemplate>
                <asp:Timer ID="myTimer" Interval="2000" runat="server" OnTick="myTimer_Tick" Enabled="False"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>              
    </form>
</body>
</html>
