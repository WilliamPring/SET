
<!-- this include files checks the mode we are in.
if it is open then we hide the openleft image
if we are closed, we show (set visible) the open left image
-->
<% if Session("formViewMode")="close" then %>
	<div id="expandDIV" STYLE="position:absolute;left:2;top:2;CURSOR:hand;z-index:100;visibility:visible;">
<%else%>
	<div id="expandDIV" STYLE="position:absolute;left:2;top:2;CURSOR:hand;z-index:100;visibility:hidden;">
<%end if%>
	<img src="../images/right_content_open.gif" width="19" height="19" alt="Reopen the Assessment Navigation" border="0" onclick="javascript:autosave('<%=Session("ccrsAutoSave")%>');javascript:openLeftSession();openNavHighlight();">
</div>
