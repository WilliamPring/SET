<!--
    Filename: Default.aspx
    Assignment: Assignment 4 WDD
    By: Naween Mehanmal and William Pring 
    Date: November 16, 2015
    Description: This program focuses on building a Hi-Low game using the ASP.Net Framwork, program is event driven. The game logic is all coded in the 'code behind'
    and the UI relies on the ASP.Net's designer toolbox
-->
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

        <!-- The control validators will ease the client side validation check, so that no garbage values are sent to the server -->
        <asp:RequiredFieldValidator ID="validateEmptyTextbox" ValidationGroup="valForm" runat="server" ControlToValidate="gameTextbox" Text="* Required!" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validateRegex" ValidationGroup="valForm" runat="server" ControlToValidate="gameTextbox" ValidationExpression="^[a-zA-Z''-'\s]{1,40}$" ErrorMessage="* Must only contain alphabets" Display="Dynamic"></asp:RegularExpressionValidator>
        
    
       <br /><br /><asp:Button ID="buttonID" onClick="submit" Text="Submit" runat="server" ValidationGroup="valForm" CausesValidation="true"/>
    </div>
    </form>
</body>
</html>
