<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hi_Low_Game._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hi-Low Game in Asp.Net</title>
</head>
<body id="body" runat="server">
    <form id="serverForm" runat="server">

    <div>
        <asp:Label ID="mainLabel" Text="Enter in your name: "  runat="server"/>
        <asp:TextBox ID="gameTextbox" runat="server"/>
        <asp:RequiredFieldValidator ID="validateEmptyTextbox" ValidationGroup="valForm" runat="server" ControlToValidate="gameTextbox" Text="* Required!" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validateRegex" ValidationGroup="valForm" runat="server" ControlToValidate="gameTextbox" ValidationExpression="^[a-zA-Z''-'\s]{1,40}$" ErrorMessage="* Must only contain alphabets" Display="Dynamic"></asp:RegularExpressionValidator>
        
        <!-- 
        <asp:Label ID="askForRandomNumber" Text="Enter in a number: " Visible="false" runat="server" />
        <asp:TextBox CausesValidation="false" ID="hashNumberKey" Visible="false" runat="server" />
        <asp:RequiredFieldValidator ID="requiredNumber" runat="server" ControlToValidate="hashNumberKey" ErrorMessage="Second Error Missing Message" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="numberValidatorTwo"  runat="server" ControlToValidate="hashNumberKey" ValidationExpression="\d+" ErrorMessage="Must enter a positive number." Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:RangeValidator ID="numberRangeValidator" runat="server" MinimumValue="2" MaximumValue="1000" Type="Integer" ControlToValidate="hashNumberKey" ErrorMessage="Number cannot be less than or equal to 1." Display="Dynamic"></asp:RangeValidator>
        -->

       <br /><br /><asp:Button ID="buttonID" onClick="submit" Text="Submit" runat="server" ValidationGroup="valForm" CausesValidation="true"/>
    </div>
    </form>
</body>
</html>
