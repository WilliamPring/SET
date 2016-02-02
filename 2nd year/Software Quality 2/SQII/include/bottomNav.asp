<center><table width=700 border=0 cellpadding=0 cellspacing=0>
	<tr><td class=bgR_TL ></td><td class=bgR_T ></td><td class=bgR_TR ></td></tr>
	<tr class=bgR_Back><td class=bgR_L ></td><td style="width: 378px; padding: 0px 0px;">

		<table width=680 border=0 cellpadding=0 cellspacing=0 bgcolor=#6F82C0>
			<tr>
				<td align=center><a href="#" OnClick='document.JoinForm.submit();'><img src='../images/savebutton.gif' alt="Save the Data of this Section" border="0"></a></td>
		</tr></table>

<%
dim sectionList(26)

counter=1
alphabet=section&","
sectionList(counter)=section
vItem=section

'--- now go through all the notes and display them
errorHilightColor = "#dff1ff"
errorUnHilightColor = "#9AA9DA"
errorFlag=false	 
Response.write("<table border=0 cellspacing=0 width=680 align=center cellpadding=0>")
Response.write("<tr><td class=fieldlabels bgcolor=#ffffff><b>Section - AB&nbsp;&nbsp;&nbsp;Outstanding Errors</b></td></tr>")
errSection="A"
printAB = 0

    if ((whichState = "SUBMIT") and (numErrs >= 1)) then
'Response.Write("Sean - (" & errNotes & ")<br>")
	    ' show the individual errors
	    currError=-3
	    nextError=Instr(1,errNotes,"<br>",1)
	    do while (nextError > 0)
  			currErr = Mid(errNotes,(currError+4),(nextError-(currError+4)))
		    Response.Write("<tr><td class=""smalllabels"" style=""cursor:hand"" onMouseOver=""bgColor='" & errorHilightColor & "'; window.status='This error can be corrected in this section'; return true;"" onMouseOut=""bgColor='" & errorUnHilightColor & "'; window.status=''; return true;"" >"&currErr&"</td></tr>")
		    currError=nextError
		    nextError=Instr((currError+4),errNotes,"<br>",1)
	    loop
   				
	    'print out the last error
	    currErr = Mid(errNotes,(currError+4))
	    Response.Write("<tr name=err" & CStr(j) & "><td class=""smalllabels"" style=""cursor:hand"" onMouseOver=""bgColor='" & errorHilightColor & "'; window.status='This error can be corrected in this section'; return true;"" onMouseOut=""bgColor='" & errorUnHilightColor & "'; window.status=''; return true;"" >"&currErr&"</td></tr>")
        Response.Write("</tr>")
    else
        if ((whichState <> "SUBMIT") and (numErrs = 1)) then
            Response.Write("<tr><td class=""smalllabels"" style=""cursor:hand"" onMouseOver=""bgColor='" & errorHilightColor & "'; window.status='This error can be corrected in this section'; return true;"" onMouseOut=""bgColor='" & errorUnHilightColor & "'; window.status=''; return true;"">INCOMPLETE SECTION. None of the items contain values - most likely because this section has not been started yet.</td></tr>")
        else
            if ((whichState = "SUBMIT") and (errNotes = "") and (numErrs = 0)) then
                Response.Write("<tr><td class=""smalllabels"">No errors found</td></tr>")
            end if
        end if
    end if
Response.Write("</table>")
%>

