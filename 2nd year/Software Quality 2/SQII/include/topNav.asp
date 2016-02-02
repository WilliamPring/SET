<%
DIM formIsLocked
DIM presAssessRefDate
DIM canPrint
DIM formHasBeenTransmitted
DIM nextBtn
DIM prevBtn

Session("currentpagename")="../CCRS/Forms/FORM_Section_"&UCase(section)&"_"&UCase(formType)&"_"&page&".asp?"&Request.ServerVariables("QUERY_STRING") 	
'Response.Write("currpagename="&Session("currentpagename")&"<br>")
formIsLocked = "NO"
presAssessRefDate="20110314"
canPrint = false
formHasBeenTransmitted = "NO"


if Session("currAssessment")="01" then
	printType="Admission"
elseif Session("currAssessment")="02" then
	printType="full"		
elseif Session("currAssessment")="03" then
	printType="full"			
elseif Session("currAssessment")="04" then
	printType="full"			
elseif Session("currAssessment")="05" then
	printType="quart"			
elseif Session("currAssessment")="06" then
	printType="discharge"		
elseif Session("currAssessment")="07" then
	printType="discharge"		
elseif Session("currAssessment")="08" then
	printType="discharge"
elseif Session("currAssessment")="09" then
	printtype="reentry"		
elseif Session("currAssessment")="10" then
	printType="quart"			
end if	

 nextBtn=""
 prevBtn=""
'response.Write("prev, next = "&prevBtn&", "&nextBtn&"<br>")
Session("currFormType") = formtype
Session("currSection") = section
Session("currPage") = page
Session("ccrsNextBtn")=nextBtn
Session("ccrsPrevBtn")=prevBtn

%>

<table width='100%'>
<tr><td class=navbody valign=top>
<%if userEnable="" then %> 
	<% if (canPrint = true) then %>
		<img name=print src='../images/printicon.gif' width='15' height='11' alt='Print Completed Assessment' border='0'>&nbsp;<a href=#>Print Completed Assessment</a>
	<% else %>
		<img name=print src='../images/printicon.gif' width='15' height='11' alt='Print Section' border='0'>&nbsp;<a href=#>Print Section</a>
	<%end if %>
<% else %>
	<% if (canPrint = true) then %>
		<img name=print src='../images/printicon.gif' width='15' height='11' alt='Print Completed Assessment' border='0'>&nbsp;<a href=#>Print Completed Assessment</a>
	<% end if %>
<% end if %>
&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" ><img src='../images/folder.gif' alt='View Patient History' border='0'>&nbsp;&nbsp;View Patient History</a>
<% if(formIsLocked="NO") then %>
	&nbsp;&nbsp;&nbsp;&nbsp;<a href="#"><img src='../images/go-button.gif' alt='Validate Assessment' border='0'>&nbsp;&nbsp;Validate Entire Assessment</a>
<% end if %>
&nbsp;&nbsp;&nbsp;&nbsp;<a href="#"><img src='../images/sticky-active.gif' alt='View the Audit Trail for this Assessment' border='0'>&nbsp;&nbsp;View Entire Assessment Audit Trail</a>
</td>
</td></tr><table>
<input type=hidden name="NEXT" value="NO">
<input type=hidden name="PREV" value="NO">
<input type=hidden name="UNKN" value="NO">
  