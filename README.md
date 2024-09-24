# User controls in ASP .NET

An introduction to writing User Controls in ASP .NET



## Introduction

In traditional ASP code reuse and encapulation was traditionally done 
using a combination of include files and web classes. While this worked 
reasonably well for business logic, it was always a little fiddly for visual 
components. For example, if you wanted to display a grid of data in many 
different places and have the grid have the same general look and feel, but be 
customisable for a particular page, then you either cut and pasted the HTML, 
used style sheets, used an include file, wrote some VBScript to generate the 
HTML on the fly, or used a combination of all these methods.

It was messy. It could also be difficult to move these components between 
projects because there was the ever present problem of ensuring that variable 
names didn't conflict, that you had the include file included only once 
(and in the correct order). Then there was the whole issue of tying the new 
control into existing code.

ASP .NET solves many of these issues with the introduction of User 
Controls. These are self contained visual elements that can be placed on a web 
page in the same way as a tradition intrinsic HTML control, and can have their 
attributes set in a similar fashion.

In this article I'll present a Title bar that has 6 properties (border width 
and colour, Title text and colour, background and padding. The control is 
embedded in a page using the following simple syntax:

```cpp
<CP:TitleBar Title="User Control Test" TextColor="green" Padding=10 runat=server />
```

This produces the following output: 

![Sample Image - MyUserControl.gif](https://raw.githubusercontent.com/ChrisMaunder/myusercontrol/master/docs/assets/myusercontrol.gif)

## So how do we write a user control?

The easiest way is using Visual Studio .NET. Start a new 
web project and right click on the project's name in the Server Explorer. Select 
'Add...' then 'Add Web User Control...' Rename the control and hit Open. The 
wizard produces two files: a .ascx file containing the visual layout, and a 
.ascx.cs (assuming you are working in C#) that has the business logic. 

### The Visual Layout

First we design the visual layout of the Title bar. We'll use the new asp:XXX 
controls so that we get simple access to their attributes at runtime.

The entire .ascx file looks like:

```cpp
<%@ Control Language="c#" AutoEventWireup="false" 
    Codebehind="MyUserControl.ascx.cs" 
    Inherits="CodeProject.MyUserControl" 
    TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0"%>

<asp:table id=OuterTable BackColor=#c0c0c0c BorderWidth=0 cellPadding=0 
           cellSpacing=1 width='100%' Runat=server>
<asp:tableRow><asp:tableCell width="100%">

<asp:table id=InnerTable BackColor=#cccccc BorderWidth=0 cellPadding=0 
           cellSpacing=1 width="100%" Runat=server>
<asp:tableRow<asp:tablecell HorizontalAlign=Center>

<asp:Label ID=TitleLabel Runat=server />

</asp:tablecell></asp:tableRow>
</asp:table>

</asp:tablecell></asp:tableRow>
</asp:table>
```

A fairly traditional way of creating a table with a thin 
border - except that we are using ASP .NET controls instead of traditional HTML.

Note that if you were restricting your viewers to using IE then you could use 
the border-width style to set the border style and stick with one table. 
However, if you have Netscape 4.X viewers then this won't work. ASP .NET 
controls will output HTML 3.2 for downspec browsers like Netscape, so while ASP 
.NET's promise of 'write-once, view anywhere' sounds good, it doesn't actually 
happen in practice.

I'm not going to go into the in's and out's of ASP .NET 
controls. They are fairly self explanatory. Just make sure you include the 
'runat=server' bit. They help.

### The backend logic

Note the bit up the top of the file: `Codebehind="MyUserControl.ascx.cs"`. 
This is where the visual layout (.ascx file) is hooked up with the backend logic 
(.ascx.cs). We will barely scrape the surface of what this separation of logic 
and display can do.

Our C# code looks like the following:

```cpp
namespace CodeProject
{
    using System;
    ...

    public abstract class MyUserControl : System.Web.UI.UserControl
    {
        public string Title       = null;
        public string TextColor   = Color.Black.Name;
        public string BackColor   = Color.Wheat.Name;
        public int    Padding     = 2;
        public string BorderColor = Color.Gray.Name;
        public int    BorderWidth = 1;
        
        protected Table OuterTable;
        protected Table InnerTable;
        protected Label TitleLabel;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Title==null || Title=="") 
                Visible = false;
            else
            {

                OuterTable.BackColor   = Color.FromName(BorderColor);
                OuterTable.CellSpacing = BorderWidth;
                InnerTable.CellPadding = Padding;
                InnerTable.BackColor   = Color.FromName(BackColor);
                
                TitleLabel.Text        = Title;
                TitleLabel.ForeColor   = Color.FromName(TextColor);
                TitleLabel.Font.Name   = "Verdana";
                TitleLabel.Font.Bold   = true;
                TitleLabel.Font.Size   = FontUnit.Parse("13");
            }
        }

        #region Web Form Designer generated code
        ...
        #endregion
    }
}
```

The entire purpose of this backend is to define a set of properties
that the user can access when instantiating the control, and to then
set the attributes of the ASP .NET controls in our .ascx file based on
these properites.

We could just as easily open a connection to a database and bind
a dataset to a control, or we could access the page that this control
resides in (using the `Page` property) and adjust the look
and feel of the control from what we find there. We could also provide
user input mechanisms in our display and provide handlers for click
events in the control here as well. These controls really are quite powerful.

Unfortunately we'll be staying a lot closer to Earth and 
will only provide the mediocrity of colour and border width changes. 

### Properties

Note the block of variable declarations at the top:

```cpp
        public string Title       = null;
        public string TextColor   = Color.Black.Name;
        public string BackColor   = Color.Wheat.Name;
        public int    Padding     = 2;
        public string BorderColor = Color.Gray.Name;
        public int    BorderWidth = 1;
```

These publicly accessible variables form the attributes that are
available to the user when he or she instantiates the control in a
web page:

```cpp
<CP:TitleBar Title="User Control Test" TextColor="green" Padding=10 runat=server />
```

The user can set all, some or none of these properties.

### The link between the visual elements and the code

   The other half of the story is the link between the ASP .NET controls
and the C# code. Note the block of variables underneath these:

```cpp
        protected Table OuterTable;
        protected Table InnerTable;
        protected Label TitleLabel;
```

The names of these variables are the same as the ID's of the ASP .NET controls
in our .ascx page. This causes the variable in our C# class to be linked up to the 
ASP .NET control in the .ascx page. We can now manipulate the controls on the
page however we like:

```cpp
            if (Title==null || Title=="") 
                Visible = false;
            else
            {

                OuterTable.BackColor   = Color.FromName(BorderColor);
                OuterTable.CellSpacing = BorderWidth;
                InnerTable.CellPadding = Padding;
                InnerTable.BackColor   = Color.FromName(BackColor);
                
                TitleLabel.Text        = Title;
                TitleLabel.ForeColor   = Color.FromName(TextColor);
                TitleLabel.Font.Name   = "Verdana";
                TitleLabel.Font.Bold   = true;
                TitleLabel.Font.Size   = FontUnit.Parse("13");
            }
```

Here we simply check if there is any title text, and if not then we set the
control as invisible (meaning the HTML for the control simply won't be rendered
and will not be sent down to the client). If there is text then we go ahead and
set the properties of the controls.

And we're done!

## Using the control

To use the control you will first need to compile the project with your files, 
and then create a page to host the control. The code for the control will be compiled
into an assembly and placed in the /bin directory. If you make any changes to the
C# code you'll need to recompile. You do not have to recompile if you make changes 
to the visual layout (.ascx) file.

To use the control you must make a page aware of the control. We do this
by using a Register directive that specified the tag prefix we'll use, the
tag name and the location of the user control's page:

```cpp
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
```

The tag prefix in this case is `CP`, and the tag `TitleBar` They can be anything you want as long as there are no conflicts. Open the page in your browser and be amazed at your cleverness.
