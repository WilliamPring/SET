<!--
// this code processes the closing and expanding of the Nav items. Also used in 
// formbuilder for hiding/showing checkboxes items

Option Explicit

Dim previousID
DIM menuID,menuObj
DIM numItems, i
DIM ret
DIM CkNameLen, CkValLen
DIM CkValStart, CkVal, CkValEnd
	
SUB ShowHide(menuID)
	menuObj = menuID
	// CHECK IF OPEN
	if document.all.item(menuObj).className = "LINKSON" then    'IF MENU OPEN THEN CLOSE
		document.all.item(menuObj).className = "LINKSOFF"
	ELSE
		document.all.item(menuObj).className = "LINKSON"               ' OPEN MENU   
	END IF
END SUB

SUB ShowHideAll()
	numItems = document.all.length
//msgbox "numitems = "+CStr(numItems)
	for i=1 to numItems-1
		if document.all(i).className = "LINKSON" then    'IF MENU OPEN THEN CLOSE
			document.all(i).className = "LINKSOFF"
		else
			if document.all(i).className = "LINKSOFF" then    'IF MENU OPEN THEN CLOSE
				document.all(i).className = "LINKSON"
			end if
		end if
	next
END SUB

-->