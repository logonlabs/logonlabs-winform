using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.WinForms
{
  internal interface ILogonBrowser
  {
    event EventHandler<LogonEventArgs> LogonComplete;

    bool UseDestinationMode { get; set; }
    string AppId { get; set; }

    string ApiUrl { get; set; }

    string Provider { get; set; }

    void Navigate(string url);
    
    void Initialize();
  }
}
