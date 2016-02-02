
<%

Session("currAssessment") = "01"
Session("mdsOID") = "<i>Not Specified</i>"

formType="ADMISSION"
section="A"
page="1"

oid=Session("mdsOID")
uid=Request("uid")
title=Request("title")

Response.Cookies("formtype")=formType

'Response.Write("formType = " & formType & ", section = " & section & "<br>")
'Response.Write("page = " & page & ", title = " & title & "<br>")
Session("formViewMode") = request.cookies("viewmode")
response.redirect("forms/FORM_Section_"&section&"_"&formType&"_"&page&".asp?formType="&formtype&"&section="&section&"&oid="&Request("oid")&"&page="&page&"&title="&title&"")
%>
