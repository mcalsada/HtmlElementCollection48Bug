using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			webBrowser1.Navigate("http://google.com/");
			webBrowser1.StatusTextChanged += WebBrowser1_StatusTextChanged;
		}

		private void WebBrowser1_StatusTextChanged(object sender, EventArgs e)
		{
			textBox1.Text = webBrowser1.ReadyState.ToString();
		}

		private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{			
			var bodyElement = webBrowser1.Document.GetElementsByTagName("body");
			var methods = bodyElement.GetType().GetMethods();
			var found = false;
			foreach (var item in methods)
			{
				Console.WriteLine(item.Name);
				if (item.Name == "get_Item")
				{
					MessageBox.Show("Found GetItem");
					found = true;

					try
					{
						object[] args = new object[1];
						args[0] = 0;

						var result = item.Invoke(bodyElement, args);						
						MessageBox.Show("Found it..." + (result as HtmlElement).InnerHtml.ToString());
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error..." + ex.Message + Environment.NewLine + ex.StackTrace);
					}
					
					
					break;
				}
			}

			if (!found)
			{
				MessageBox.Show("Missing");
			}
		}
	}
}
