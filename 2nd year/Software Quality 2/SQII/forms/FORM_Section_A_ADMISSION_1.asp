<%
dim userEnable
dim facName
dim numErrs
dim errNotes

whichState = request.QueryString("state")
section=Request("section")
oid=Request("oid")
'oid=Session("mdsOID")
uid=Request("uid")
formType=Request("formType")
page=Request("page")
comastate=""
userEnable=""

title=Request("title")

MDSAB2A=Request("MDSAB2A")
MDSAB2B=UCase(Request("MDSAB2B"))
MDSAB3=Request("MDSAB3")
MDSAB4=UCase(Request("MDSAB4"))
MDSAB5a=Request("MDSAB5a")
MDSAB5b=Request("MDSAB5b")
MDSAB5c=Request("MDSAB5c")
MDSAB5d=Request("MDSAB5d")
MDSAB5e=Request("MDSAB5e")
MDSAB5f=Request("MDSAB5f")
MDSAB7=Request("MDSAB7")
MDSAB8=Request("MDSAB8")
MDSAB9=Request("MDSAB9")
MDSAB10a=Request("MDSAB10a")
MDSAB10b=Request("MDSAB10b")
MDSAB10c=Request("MDSAB10c")
MDSAB10d=Request("MDSAB10d")
MDSAB10e=Request("MDSAB10e")
MDSAB10f=Request("MDSAB10f")

' translate the checkbox ON to 1
if(MDSAB5a = "on") then MDSAB5a = "1"
if(MDSAB5b = "on") then MDSAB5b = "1"
if(MDSAB5c = "on") then MDSAB5c = "1"
if(MDSAB5d = "on") then MDSAB5d = "1"
if(MDSAB5e = "on") then MDSAB5e = "1"
if(MDSAB5f = "on") then MDSAB5f = "1"
if(MDSAB10a = "on") then MDSAB10a = "1"
if(MDSAB10b = "on") then MDSAB10b = "1"
if(MDSAB10c = "on") then MDSAB10c = "1"
if(MDSAB10d = "on") then MDSAB10d = "1"
if(MDSAB10e = "on") then MDSAB10e = "1"
if(MDSAB10f = "on") then MDSAB10f = "1"
errNotes = ""
numErrs = 0

' cheesy way of keeping track of what is valid - not good coding !!
ab2a = "0"
ab2b = "0"
ab3 = "0"
ab4 = "0"
ab5 = "0"
ab7 = "0"
ab8 = "0"
ab9 = "0"
ab10 = "0"
overall = "0"

if(whichState = "SUBMIT") then
    ab2a = "1"
    ab2b = "1"
    ab3 = "1"
    ab4 = "1"
    ab5 = "1"
    ab7 = "1"
    ab8 = "1"
    ab9 = "1"
    ab10 = "1"

'    response.Write("submitted data<br>")
    ' do validation and create errors
    if(trim(MDSAB2A) = "") then
        ab2a = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB2a : Please specify the <i>Level of Care Admitted From</i> on Admission.<br>"
    end if
    if((trim(MDSAB2A) <> "") and (trim(MDSAB2B) = "")) then
        ab2aV = CInt(trim(MDSAB2A))
        if((ab2aV >= 1) and (ab2aV <= 7)) then
            ab2b = "0"
            numErrs = numErrs + 1
            errNotes = errNotes & "Item AB2b : Given the value for <b>AB2a</b> you <u>must specify</u> a facility in AB2b.<br>"
        end if
    end if
    if((trim(MDSAB2A) <> "") and (trim(MDSAB2B) <> "")) then
        ab2aV = CInt(trim(MDSAB2A))
        if((ab2aV < 1) or (ab2aV > 7)) then
            ab2b = "0"
            numErrs = numErrs + 1
            errNotes = errNotes & "Item AB2b : Given the value for <b>AB2a</b> you <u>must not specify</u> a facility in AB2b.<br>"
        end if
    end if
    if((len(trim(MDSAB2B)) > 0) and (ab2b = "1")) then
        if(len(trim(MDSAB2B)) = 5) then
    	    leftChar = left(trim(MDSAB2B),1)
	    	if((leftChar<>"U") and (leftChar<>"Z") and (leftChar<>"0") and (leftChar<>"1") and (leftChar<>"2") and (leftChar<>"3") and (leftChar<>"4") and (leftChar<>"5") and (leftChar<>"6") and (leftChar<>"7") and (leftChar<>"8") and (leftChar<>"9") and (leftChar<>"N") and (leftChar<>"Y") and (leftChar<>"V")) then
                ab2b = "0"
                numErrs = numErrs + 1
                errNotes = errNotes & "Item AB2b : A <i>valid</i> Facility Code is made up of a <b>Province Identifier</b> and <b>4 digits</b>  (eg. 50002).<br>"
	    	end if
        else
            ab2b = "0"
            numErrs = numErrs + 1
            errNotes = errNotes & "Item AB2b : A <i>valid</i> Facility Code is made up of a <b>Province Identifier</b> and <b>4 digits</b>  (eg. 50002).<br>"
        end if
    end if
    if(trim(MDSAB3) = "") then
        ab3 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB3 : Please indicate if the patient <i>lived alone</i> prior to Admission.<br>"
    end if
    if(trim(MDSAB4) = "") then
        ab4 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB4 : Please specify a <i>valid</i> Postal Code.<br>"
    end if
    if((len(trim(MDSAB4)) <> 6) and (len(trim(MDSAB4)) <> 3) and (ab4 = "1")) then
        ab4 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB4 : Please specify a <i>valid</i> Postal Code.<br>"
    end if
    if(((len(trim(MDSAB4)) = 6) or (len(trim(MDSAB4)) = 3)) and (ab4 = "1")) then
        correct = 0
        if((asc(mid(MDSAB4,1,1)) >= 65) and (asc(mid(MDSAB4,1,1)) <= 91)) then correct = correct + 1
        if((asc(mid(MDSAB4,2,1)) >= 48) and (asc(mid(MDSAB4,2,1)) <= 57)) then correct = correct + 1
        if((asc(mid(MDSAB4,3,1)) >= 65) and (asc(mid(MDSAB4,3,1)) <= 91)) then correct = correct + 1
		if(len(trim(MDSAB4)) = 6) then
        	if((asc(mid(MDSAB4,4,1)) >= 48) and (asc(mid(MDSAB4,4,1)) <= 57)) then correct = correct + 1
	        if((asc(mid(MDSAB4,5,1)) >= 65) and (asc(mid(MDSAB4,5,1)) <= 91)) then correct = correct + 1
        	if((asc(mid(MDSAB4,6,1)) >= 48) and (asc(mid(MDSAB4,6,1)) <= 57)) then correct = correct + 1

			if(correct <> 6) then
            	ab4 = "0"
	            numErrs = numErrs + 1
            	errNotes = errNotes & "Item AB4 : Please specify a <i>valid</i> Postal Code.<br>"
        	end if
        else
			if(correct <> 3) then
            	ab4 = "0"
	            numErrs = numErrs + 1
            	errNotes = errNotes & "Item AB4 : Please specify a <i>valid</i> Postal Code.<br>"
        	end if
		end if

		' if still correct - then make sure leftmost character is valid
	    leftChar = left(trim(MDSAB4),1)
		if((leftChar="D") or (leftChar="F") or (leftChar="I") or (leftChar="O") or (leftChar="Q") or (leftChar="U") or (leftChar="W") or (leftChar="Z")) then
           	ab4 = "0"
            numErrs = numErrs + 1
           	errNotes = errNotes & "Item AB4 : Postal Code is <i>invalid</i> according to Canada Post.<br>"
		end if

    end if
    if((trim(MDSAB3) <> "9") and (trim(MDSAB3) <> "")) then
        count = 0
        if(trim(MDSAB5a) <> "") then count = count + 1
        if(trim(MDSAB5b) <> "") then count = count + 1
        if(trim(MDSAB5c) <> "") then count = count + 1
        if(trim(MDSAB5d) <> "") then count = count + 1
        if(trim(MDSAB5e) <> "") then count = count + 1
        if(trim(MDSAB5f) <> "") then count = count + 1
        if(count = 0) then
            ab5 = "0"
            numErrs = numErrs + 1
            errNotes = errNotes & "Item AB5 : Given the value for <b>AB3</b> you <u>must specify</u> the Patient's Residential History.<br>"
        end if
    end if
    if(trim(MDSAB9) = "1") then
        if(trim(MDSAB7) = "") then
            ab7 = "0"
            numErrs = numErrs + 1
            errNotes = errNotes & "Item AB7 : Given the value for <b>AB9</b> you <u>must specify</u> the Patient's Education Level.<br>"
        end if
    else
        count = 0
        if(trim(MDSAB10b) <> "") then count = count + 1
        if(trim(MDSAB10c) <> "") then count = count + 1
        if(trim(MDSAB10d) <> "") then count = count + 1
        if(trim(MDSAB10e) <> "") then count = count + 1
        if(trim(MDSAB10f) <> "") then count = count + 1
        if(count > 0) then
            if((trim(MDSAB7) = "") and (ab7 = "1")) then
                ab7 = "0"
                numErrs = numErrs + 1
                errNotes = errNotes & "Item AB7 : Given the value for <b>AB10</b> you <u>must specify</u> the Patient's Education Level.<br>"
            end if
        end if
    end if
    if(trim(MDSAB8) = "") then
        ab8 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB8 : Please specify a <i>valid</i> ISO Language Code.<br>"
    end if
    if(trim(MDSAB9) = "") then
        ab9 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB9 : Please indicate if this patient has an <i>History of Mental Illness</i>.<br>"
    end if
    count = 0
    if(trim(MDSAB10a) <> "") then count = count + 1
    if(trim(MDSAB10b) <> "") then count = count + 1
    if(trim(MDSAB10c) <> "") then count = count + 1
    if(trim(MDSAB10d) <> "") then count = count + 1
    if(trim(MDSAB10e) <> "") then count = count + 1
    if(trim(MDSAB10f) <> "") then count = count + 1
    if(count = 0) then
        ab10 = "0"
        numErrs = numErrs + 1
        errNotes = errNotes & "Item AB10 : Please indicate if this patient has any <i>Developmental Disabilities</i>.<br>"
    end if

    ' check for overall validity - again cheesy ...
	overall = "0"
    currVal = CInt(ab2a) + CInt(ab2b) + CInt(ab3) + CInt(ab4) + CInt(ab5) + CInt(ab7) + CInt(ab8) + CInt(ab9) + CInt(ab10)
	if(currVal = 9) then 	' all fields are valid
		overall = "1"
	end if
else
    numErrs = 1
    errNotes = "INCOMPLETE SECTION. None of the items contain values - most likely because this section has not been started yet."
end if
'response.Write("MDSTena="&MDSAB10a&"<br>")
'response.Write("numErrs = "&CStr(numErrs)&"<br>")
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML><HEAD>
<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
<LINK href="../css/styles.css" rel=styleSheet type=text/css>
<style type="text/css">
.LINKSOFF    {display: none;}
.LINKSON     {display: inline;}
</style>
<script type="text/javascript" src="../js/utility.js"></script>
<script type="text/vbscript" src="../js/utility.vbs"></script>
<title>SQ-II Sample ASP Form</title>
</HEAD>
<BODY bgColor="#9aa9da" leftMargin="15" topMargin="0" marginwidth="0" marginheight="0"><form name=JoinForm <%=userEnable%> action="./FORM_Section_A_ADMISSION_1.asp?oid=<%=oid%>&section=A&formtype=ADMISSION&page=1&state=SUBMIT" method=post >
<!--#include virtual="include/incOpenDivSession.asp" -->
<!--#INCLUDE virtual="include/topNav.asp" -->
<script language="VBScript">
	Function getPostal(whichPostal)
		dim ret
		ret = false


		tmpStr = document.JoinForm.item(whichPostal).value
		tmpLen = len(tmpStr) + 1
		whichKey = window.event.keyCode
		if((len(tmpStr)>=6) or (len(tmpStr)=0)) then
			tmpLen = 1
		end if
		if((Mid(tmpStr,1,1) = "-") and (len(tmpStr)>=3))then
			tmpLen = 1
		end if

		if((tmpLen = 1) or (tmpLen=3) or (tmpLen=5)) then
			if((whichKey > 64) and (whichKey < 91)) then ret = true
			if((whichKey > 96) and (whichKey < 123)) then ret = true
			if(whichKey =45) then ret = true
			if((Mid(tmpStr,1,1) = "-") and (tmpLen = 3))then
				if((whichKey > 47) and (whichKey < 58)) then ret = true
			end if
		end if
		if((tmpLen = 2) or (tmpLen=4) or (tmpLen=6)) then
			if((whichKey > 47) and (whichKey < 58)) then ret = true
		end if

		tmpStr = document.JoinForm.item(whichPostal).value
		if((len(tmpStr)>=6) or (len(tmpStr)=0)) then
			document.JoinForm.item(whichPostal).value = ""
		end if
		if((Mid(tmpStr,1,1) = "-") and (len(tmpStr)>=3))then
			document.JoinForm.item(whichPostal).value = ""
		end if

		window.event.returnValue = ret
		getPostal = ret
	End Function
</script>
<DIV id="masterDIV" STYLE="position:absolute;left:0;top:0;width:100%;"><br><br><br><br><br>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB2A&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB2a. Admitted From</span><br>
</TD>
<TD align=left valign=bottom>
<span class=smalllabels>Level of care admitted from <i>on admission</i></span>
<br><a name='MDSAB2A'><select class=inputbox name='MDSAB2A' tabindex=1  <%=userEnable%> <%=comastate%>></a>
<option value=''></option>
<option value='00' <%if (MDSAB2A="00") then%> selected <%end if%> >0. Ambulatory Health</option>
<option value='01' <%if (MDSAB2A="01") then%> selected <%end if%> >1. Inpatient Acute Care</option>
<option value='02' <%if (MDSAB2A="02") then%> selected <%end if%> >2. Inpatient General Rehab</option>
<option value='03' <%if (MDSAB2A="03") then%> selected <%end if%> >3. Inpatient Continuing Care</option>
<option value='04' <%if (MDSAB2A="04") then%> selected <%end if%> >4. Residential 24hr Nursing</option>
<option value='05' <%if (MDSAB2A="05") then%> selected <%end if%> >5. Inpatient Psychiatric</option>
<option value='06' <%if (MDSAB2A="06") then%> selected <%end if%> >6. Other/Unclassified</option>
<option value='07' <%if (MDSAB2A="07") then%> selected <%end if%> >7. Inpatient Special Rehab</option>
<option value='08' <%if (MDSAB2A="08") then%> selected <%end if%> >8. Home Care</option>
<option value='09' <%if (MDSAB2A="09") then%> selected <%end if%> >9. Residential Board & Care</option>
<option value='10' <%if (MDSAB2A="10") then%> selected <%end if%> >10. Private Home</option>
</select></TD><td><input type="hidden" name="ab2a" value=<%=ab2a%>><input type="hidden" name="ValidPatient" value=<%=overall%>><input type="hidden" name="NumberOfErrors" value=<%=numErrs%>></td></TR></TABLE><br>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB2B&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB2b. <i>Admitted From</i> Facility Number</span><br>
</TD>
<TD valign=bottom>
<span class=smalllabels>This element must be coded if <i>AB2a</i> is in the range 1 to 7.  Otherwise leave blank.</span>
<br>
<input type='text'  <%=userEnable%> <%=comastate%> name=MDSAB2B value='<%=MDSAB2B%>' maxlength='5' size='5' tabindex=2 >&nbsp;
<%
	facName = "( Not Specified )"
	if(len(trim(MDSAB2B)) = 5) then
	    leftChar = left(trim(MDSAB2B),1)
		if(leftChar="U") then facName = "( Facility in United States )"
		if(leftChar="Z") then facName = "( Facility in Other Country )"
		if(leftChar="0") then facName = "( Facility in Newfoundland )"
		if(leftChar="1") then facName = "( Facility in Prince Edward Island)"
		if(leftChar="2") then facName = "( Facility in Nova Scotia )"
		if(leftChar="3") then facName = "( Facility in New Brunswick )"
		if(leftChar="4") then facName = "( Facility in Quebec )"
		if(leftChar="5") then facName = "( Facility in Ontario )"
		if(leftChar="6") then facName = "( Facility in Manitoba )"
		if(leftChar="7") then facName = "( Facility in Saskatchewan )"
		if(leftChar="8") then facName = "( Facility in Alberta )"
		if(leftChar="9") then facName = "( Facility in British Columbia )"
		if(leftChar="N") then facName = "( Facility in Northwest Territories )"
		if(leftChar="Y") then facName = "( Facility in Yukon Territory )"
		if(leftChar="V") then facName = "( Facility in Nunavut )"
	else
		if(len(trim(MDSAB2B)) > 0) then
			facName = "( Unknown )"
		else
			facName = "( Not Specified )"
		end if
	end if
%>
<span class=smalllabels>&nbsp;&nbsp;&nbsp;<i><%=facName%></i></span>
</TD><td><input type="hidden" name="ab2b" value=<%=ab2b%>></td></TR></TABLE>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB3&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB3. Lived Alone (Before Entry)</span><br>
</TD>
<TD align=left valign=bottom>
<br>
<input type=radio  <%=userEnable%> <%if (MDSAB3="0") then%> checked <%end if%> name='MDSAB3' id='MDSAB3-0' value='0' tabindex=3 <%=comastate%>><span class=smalllabels>0. No</span>
<input type=radio  <%=userEnable%> <%if (MDSAB3="1") then%> checked <%end if%> name='MDSAB3' id='MDSAB3-1' value='1' tabindex=3 <%=comastate%>><span class=smalllabels>1. Yes</span>
<input type=radio  <%=userEnable%> <%if (MDSAB3="9") then%> checked <%end if%> name='MDSAB3' id='MDSAB3-9' value='9' tabindex=3 <%=comastate%>><span class=smalllabels>9. Unknown</span>
</TD><td><input type="hidden" name="ab3" value=<%=ab3%>></td></TR></TABLE><br>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB4&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB4. Postal Code Prior Residence.</span><br>
</TD>
<TD valign=bottom>
<span class=smalllabels></span>
<br>
<input type='text'  <%=userEnable%> <%=comastate%> name='MDSAB4' value='<%=MDSAB4%>' maxlength='6' size='6' tabindex=4 >
<!--  
  ' when using Watir, we cannot invoke any VBScripts - so "onkeypress" needed to be removed
    <input type='text'  <%=userEnable%> <%=comastate%> name='MDSAB4' value='<%=MDSAB4%>' onkeypress="return getPostal('MDSAB4');" maxlength='6' size='6' tabindex=4 >
-->
<%
	postCode = "( Not Specified )"
	if((len(trim(MDSAB4)) = 6) or (len(trim(MDSAB4)) = 3)) then
	    leftChar = left(trim(MDSAB4),1)
	    postCode = "( Invalid )"
		if(leftChar="A") then postCode = "( Residence in Newfoundland )"
		if(leftChar="B") then postCode = "( Residence in Nova Scotia )"
		if(leftChar="C") then postCode = "( Residence in Prince Edward Island )"
		if(leftChar="E") then postCode = "( Residence in New Brunswick )"
		if(leftChar="G") then postCode = "( Residence in Quebec )"
		if(leftChar="H") then postCode = "( Residence in Quebec )"
		if(leftChar="J") then postCode = "( Residence in Quebec )"
		if(leftChar="K") then postCode = "( Residence in Ontario )"
		if(leftChar="L") then postCode = "( Residence in Ontario )"
		if(leftChar="M") then postCode = "( Residence in Ontario )"
		if(leftChar="N") then postCode = "( Residence in Ontario )"
		if(leftChar="P") then postCode = "( Residence in Ontario )"
		if(leftChar="R") then postCode = "( Residence in Manitoba )"
		if(leftChar="S") then postCode = "( Residence in Saskatchewan )"
		if(leftChar="T") then postCode = "( Residence in Alberta )"
		if(leftChar="V") then postCode = "( Residence in British Columbia )"
		if(leftChar="X") then postCode = "( Residence in Nunavut / Northwest Territories )"
		if(leftChar="Y") then postCode = "( Residence in Yukon Territory )"
	else
		if(len(trim(MDSAB4)) > 0) then
			postCode= "( Invalid )"
		else
			postCode = "( Not Specified )"
		end if
	end if
%>
<span class=smalllabels>&nbsp;&nbsp;&nbsp;<i><%=postCode%></i></span>
</TD><td><input type="hidden" name="ab4" value=<%=ab4%>></td></TR></TABLE>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB5&family=YES','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB5. Residential History 5 Yrs</span><br>
</TD>
<TD valign=bottom>
<span class=smalllabels>5. Residential History 5 Years Prior to Entry (Check all settings resident lived in during 5 years prior to the <i>Assessment Reference Date</i>)</span>
<%if userEnable="" then %>
<br><a href=javascript:ShowHide('LinkMDSAB5')><img src='../images/expandBox.gif' width='15' height='15' alt='' border='0'>&nbsp;&nbsp;View</a>
<%end if %>
<br>
<DIV ID='LinkMDSAB5' CLASS='LINKSON'>
<table>
<input type=hidden fldtype='checkbox'>
<tr>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>  <%if (MDSAB5a="1") then%> checked <%end if%> name='MDSAB5a' tabindex=5 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB5","f");' >a. Prior stay at this facility</span></td>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>  <%if (MDSAB5b="1") then%> checked <%end if%> name='MDSAB5b' tabindex=5 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB5","f");' >b. Stay in other nursing home</span></td>
</tr>
<tr>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>  <%if (MDSAB5c="1") then%> checked <%end if%> name='MDSAB5c' tabindex=5 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB5","f");' >c. Other residential facility...</span></td>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>  <%if (MDSAB5d="1") then%> checked <%end if%> name='MDSAB5d' tabindex=5 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB5","f");' >d. MH/psychiatric setting</span></td>
</tr>
<tr>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>  <%if (MDSAB5e="1") then%> checked <%end if%> name='MDSAB5e' tabindex=5 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB5","f");' >e. MR/DD setting</span></td>
<td><span class=checkboxclass><input type=checkbox <%=userEnable%>   <%if (MDSAB5f="1") then%> checked <%end if%> name='MDSAB5f' tabindex=5 <%=comastate%> onclick='javascript:checkBoxes(false,"MDSAB5","6","6");' >f. NONE OF ABOVE</span></td>
</tr></table>
</TD><td><input type="hidden" name="ab5" value=<%=ab5%>></td></TR></TABLE>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB7&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB7. Education (Highest Level)</span><br>
</TD>
<TD align=left valign=bottom>
<span class=smalllabels>7. Education (Highest Level Completed)</span>
<br><a name='MDSAB7'><select class=inputbox name='MDSAB7' tabindex=6 <%=userEnable%> <%=comastate%>></a>
<option value=''></option>
<option value='1' <%if (MDSAB7="1") then%> selected <%end if%> >1. No schooling</option>
<option value='2' <%if (MDSAB7="2") then%> selected <%end if%> >2. 8th grade/less</option>
<option value='3' <%if (MDSAB7="3") then%> selected <%end if%> >3. 9-11 grades</option>
<option value='4' <%if (MDSAB7="4") then%> selected <%end if%> >4. High school</option>
<option value='5' <%if (MDSAB7="5") then%> selected <%end if%> >5. Tech or trade school</option>
<option value='6' <%if (MDSAB7="6") then%> selected <%end if%> >6. Some college</option>
<option value='7' <%if (MDSAB7="7") then%> selected <%end if%> >7. Bachelor's degree</option>
<option value='8' <%if (MDSAB7="8") then%> selected <%end if%> >8. Graduate degree</option>
<option value='9' <%if (MDSAB7="9") then%> selected <%end if%> >9. Unknown</option>
</select></TD><td><input type="hidden" name="ab7" value=<%=ab7%>></td></TR></TABLE><br>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB8&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB8. Primary Language</span><br>
</TD>
<TD valign=bottom>
<span class=smalllabels></span>
<br>
<input type='text'  <%=userEnable%> <%=comastate%> name=MDSAB8 value='<%=MDSAB8%>' maxlength='3' size='3' tabindex=7 >&nbsp;
<%if userEnable="" then %>
<a href="javascript:LookUp('MDSAB8','<%=presAssessRefDate%>');"><img src='../images/bluearrow.gif' width='16' height='15' alt='Look Up' border='0'></a>
<%end if%>
</TD><td><input type="hidden" name="ab8" value=<%=ab8%>></td></TR></TABLE>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB9&family=NO','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB9. Mental Health History</span><br>
</TD>
<TD align=left valign=bottom>
<span class=smalllabels>Does resident's RECORD indicate any history of mental illness ?</span>
<br>
<input type=radio  <%=userEnable%> <%if (MDSAB9="0") then%> checked <%end if%> name='MDSAB9' id='MDSAB9-0' value='0' tabindex=8 <%=comastate%>><span class=smalllabels>0. No</span>
<input type=radio  <%=userEnable%> <%if (MDSAB9="1") then%> checked <%end if%> name='MDSAB9' id='MDSAB9-1' value='1' tabindex=8 <%=comastate%>><span class=smalllabels>1. Yes</span>
</TD><td><input type="hidden" name="ab9" value=<%=ab9%>></td></TR></TABLE><br>
<TABLE width=100% border=0 ><TR>
<TD valign=top width=250><span class=fieldlabels>
<a href=#><img src=../images/help_icon.gif width=16 height=16 alt='Get Help with this Question' border=0></a>&nbsp;&nbsp;
<a href=javascript:openWindow('../ViewItemAuditTrail.asp?item=MDSAB10&family=YES','400','800')><img src="../images/sticky-active.gif" alt="View the Audit Trail for this Question" border="0"></a><br>
AB10. Developmental Disability conditions</span><br>
</TD>
<TD valign=bottom>
<span class=smalllabels>(Check all conditions that are related to developmental disability).</span>
<%if userEnable="" then %>
<br><a href=javascript:ShowHide('LinkMDSAB10')><img src='../images/expandBox.gif' width='15' height='15' alt='' border='0'>&nbsp;&nbsp;View</a>
<%end if %>
<br>
<DIV ID='LinkMDSAB10' CLASS='LINKSON'>
<table>
<input type=hidden fldtype='checkbox'>
<tr>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10a="1") then%> checked <%end if%> name='MDSAB10a' tabindex=9 <%=comastate%> onclick='javascript:checkBoxes(false,"MDSAB10","6","1");'>a. No developmental disability</span></td>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10b="1") then%> checked <%end if%> name='MDSAB10b' tabindex=9 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB10","a");'>b. Down's syndrome</span></td>
</tr>
<tr>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10c="1") then%> checked <%end if%> name='MDSAB10c' tabindex=9 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB10","a");'>c. Autism</span></td>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10d="1") then%> checked <%end if%> name='MDSAB10d' tabindex=9 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB10","a");'>d. Epilepsy</span></td>
</tr>
<tr>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10e="1") then%> checked <%end if%> name='MDSAB10e' tabindex=9 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB10","a");'>e. Other related organic condition</span></td>
<td><span class=checkboxclass><input  type=checkbox <%=userEnable%>  <%if (MDSAB10f="1") then%> checked <%end if%> name='MDSAB10f' tabindex=9 <%=comastate%> onclick='javascript:uncheckNOA("MDSAB10","a");'>f. Developmental disability with no organic condition</span></td>
</tr></table>
</TD><td><input type="hidden" name="ab10" value=<%=ab10%>></td></TR></TABLE>
</form>
<!--#INCLUDE virtual="include/bottomNav.asp" -->
<%
'response.Write(document.JoinForm.item(err1).value&"<br>")
%>
</div></body></html>