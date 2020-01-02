using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogonLabs.Client.WinForms
{
  internal class PlaceholderBrowser : Panel, ILogonBrowser
  {
    public bool UseDestinationMode { get; set; }
    public string AppId { get; set; }
    public string ApiUrl { get; set; }
    public string Provider { get; set; }

    public event EventHandler<LogonEventArgs> LogonComplete;

    public void Initialize()
    {      
    }

    public void Navigate(string url)
    {     
    }
  }
}
