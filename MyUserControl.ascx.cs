namespace CodeProject
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for MyUserControl.
	/// </summary>
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
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
