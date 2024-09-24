<%@ Control Language="c#" AutoEventWireup="false" 
    Codebehind="MyUserControl.ascx.cs" 
    Inherits="CodeProject.MyUserControl" 
    TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0"%>

<asp:table id=OuterTable BackColor=#c0c0c0c BorderWidth=0 cellPadding=0 
           cellSpacing=1 width='100%' Runat=server>
<asp:tableRow><asp:tableCell width="100%">

<asp:table id=InnerTable BackColor=#cccccc BorderWidth=0 cellPadding=0 
           cellSpacing=1 width="100%" Runat=server>
<asp:tableRow><asp:tablecell HorizontalAlign=Center>

<asp:Label ID=TitleLabel Runat=server />

</asp:tablecell></asp:tableRow>
</asp:table>

</asp:tablecell></asp:tableRow>
</asp:table>