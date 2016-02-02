
function openWindow (url)
{
	if(url.substring(3,10) == "MDSshow")
	{
		newwin=window.open(url,"Help","width=1024,height=768,resizable=yes,scrollbars=yes,menubar=no,personalbar=no");
	}
	else
	{
		newwin=window.open(url,"Help","width=800,height=400,resizable=yes,scrollbars=yes,menubar=no,personalbar=no");
	}
}
function checkBoxes(to_be_checked,myfield,total_boxes,whichToCheck)
{ 
	var counter=0;
	var tracker=0;
	alpha = Array(100);
 	var alphabet="a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,aa,bb,cc,dd,ee,ff,gg,hh,ii,jj,kk,ll,mm,nn,oo,pp,qq,rr,ss,tt,uu,vv,ww,xx,yy,zz,";
	var index = 0

	index = alphabet.indexOf(",", index + 1); // start search after last match found
	while (index != -1) 
	{
		alpha[tracker]=alphabet.substring(counter, index);
//msg="adding ("+alphabet.substring(counter, index)+")";
//alert(msg);
	  	counter=index+1;
		tracker=tracker+1
		index = alphabet.indexOf(",", index + 1); // start search after last match found
	}
//msg="unckecking "+total_boxes+" boxes"
//alert(msg)
	for ( i=0 ; i < total_boxes ; i++ )
	{ 
		if (to_be_checked)
		{  
	  		document.JoinForm.elements[myfield+alpha[i]].checked=true; 
		}
		else
		{ 
//msg="unchecking("+myfield+alpha[i]+")";
//alert(msg);
			document.JoinForm.elements[myfield+alpha[i]].checked=false; 
		} 
	} 
	//--check myself
	document.JoinForm.elements[myfield+alpha[whichToCheck-1]].checked=true   
} 
	
function uncheckNOA(myfield,myend)
{
	document.JoinForm.elements[myfield+myend].checked=false // make sure the NOA is unchecked
}

function LookUp(myfield,assessRefDate)
{
	if (myfield=="MDSAB8")
	{
//msg="lookup for ("+myfield+") - Language";
//alert(msg);
		window.open('../Common/Lookup/lookupLanguage.asp?fld='+myfield+'','Language','width=450,height=450,menubar=0,scrollbars=1,resizable=1,toolbar=0');	
	}
}

function uncheckAll(myfield,myend)
{
	var counter=0;
	var tracker=0;
	alpha = Array(100);
 	var alphabet="a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,aa,bb,cc,dd,ee,ff,gg,hh,ii,jj,kk,ll,mm,nn,oo,pp,qq,rr,ss,tt,uu,vv,ww,xx,yy,zz,aaa,bbb,ccc,ddd,eee,fff,ggg";
	var index = 0

	while (index != -1) 
	{
		index = alphabet.indexOf(",", index + 1); // start search after last match found
		alpha[tracker]=alphabet.substring(counter, index);
	  	counter=index+1;
		tracker=tracker+1
	}

	for (var i=0; i < myend-1; i++) 
	{
		document.JoinForm.elements[myfield+alpha[i]].checked=false
	}
	document.JoinForm.elements[myfield+alpha[myend-1]].checked=true // make sure the NOA is checked
}
	
function uncheckNOA(myfield,myend)
{
	document.JoinForm.elements[myfield+myend].checked=false // make sure the NOA is unchecked
}

