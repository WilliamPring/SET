<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
	<title>Clarity</title>		
		<link rel="STYLESHEET" type="text/css" href="css/styles.css">

</head>

<%   

dim value1(26)

element=request("item")
elementToDisplay = element
if(element = "MDSABTen") then
	elementToDisplay = "MDSAB10"
end if
family=request("family")

%>

<BODY  bgColor=#9aa9da  leftMargin="25" topMargin="0" marginwidth="0" marginheight="0"><br>
<form name="JoinForm" bgcolor="#dff1ff">
</form>
<input class=regularbuttons type=button value="  Close  " onclick="javascript:self.close()"><br>
<span class="fieldlabels"><center>AUDIT TRAIL</center></span><br>
<span class="navbody"><b>Record ID:</b> <%=session("mdsOID")%></span> <br>
<%      if(family="YES") then %>
			<span class="navbody"><b>Element Family:</b> <%=mid(elementToDisplay,4)%></span> <br><p>
<%		else  %>
			<span class="navbody"><b>Element:</b> <%=mid(elementToDisplay,4)%></span> <br><p>
<%		end if  %>
	<table border="1" cellpadding="2" cellspacing="0" width="100%" bordercolor="#AABBCC">

	
<img name=print src='images/printicon.gif' width='15' height='11' alt='' border='0'>&nbsp;<a href=javascript:window.print()>Print</a>
	<tr><td bordercolor="#CCDDEE" bgcolor="#f4f5e1" valign="top">
	<CENTER>
	<TABLE cellSpacing=1 cellPadding=1 width=100% border=0 bgcolor=#f3f3f3>
	
	  <TR class=fieldlabels bgcolor=#ffdf7b>
	    
		<td align=middle>User</td>
<%      if(family="YES") then %>
			<td align=middle>Item</td>
<%		end if  %>
		<td align=middle>Type</td>
		<TD align=middle>Old value</td>	
		<TD align=middle>New Value</td>	
		<td align=middle>Date Changed</td>
		<TD align=middle>Time Changed</td>	
		</TD>
	  </TR>

  
<%

	
response.write("<td align=middle colspan=7><i>This is where the Audit Trail for this item would be found ...</td>")
response.write("</table></center>")

'--- translate patient rec_id into patient name --

%>
</table></table>
</body>
</html>
