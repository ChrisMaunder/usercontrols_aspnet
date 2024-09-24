<%@ Page language="c#" AutoEventWireup="false" Inherits="System.Web.UI.Page" %>
         
<%@ Register TagPrefix="CP" TagName="TitleBar" Src="MyUserControl.ascx" %>
         
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
    <title>MyPage</title>
  </head>
  <body>
  <basefont face=verdana>
  
  <CP:TitleBar Title="User Control Test" TextColor="green" Padding=10 runat=server />
	
  </body>
</html>
