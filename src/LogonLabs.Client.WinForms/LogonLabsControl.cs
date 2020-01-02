
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogonLabs.Client.WinForms
{
  public class LogonLabsControl : Panel
  {

    [Description("LogonLabs AppId"), Category("Data")]
    public string AppId
    {
      get => browser?.AppId;
      set
      {
        if (browser == null)
          return;
        browser.AppId = value;
      }
    }

    [Description("LogonLabs API URL."), Category("Data")]
    public string ApiUrl
    {
      get => browser?.ApiUrl; set
      {
        if (browser == null)
          return;
        browser.ApiUrl = value;
      }
    }

    [Description("SSO Provider"), Category("Data")]
    public string Provider
    {
      get => browser?.Provider; set
      {
        if (browser == null)
          return;
        browser.Provider = value;
      }
    }
    /// <summary>
    /// In destination mode after logging into the provider, the browser will navigate to the RedirectionUrl which will
    /// validate the token and redirect to the DestinationUrl. When the browser is directed to the RedirectionUrl the control
    /// will extract the payload from the URL and trigger the LogonComplete event with the payload attached. Defaults to true.
    /// </summary>
    [Description("Destination Mode"), Category("Data")]
    public bool UseDestinationMode
    {
      get => browser?.UseDestinationMode ?? false; set {
        if (browser == null)
          return;
        browser.UseDestinationMode = value; 
      }
    }



    public event EventHandler<LogonEventArgs> LogonComplete;
    private ILogonBrowser browser = new PlaceholderBrowser();
    public LogonLabsControl()
    {
      if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
      {        
        AnyCPUSupport.Initialize();
      }
    }


    protected override void OnHandleCreated(EventArgs e)
    {
      try
      {
        // this ensures that the browser does not load when the control is loaded in the designer.
        if (!DesignMode) //  && LicenseManager.UsageMode == LicenseUsageMode.Runtime
        {
          
          AnyCpuInitializer.Initialize();
          //browser = (ILogonBrowser)Assembly.GetAssembly(typeof(LogonLabsControl)).CreateInstance("LogonLabs.Client.WinForms.LogonChromiumBrowser");
          browser = ChromiumFactory.Create();
          UseDestinationMode = true;
        }
        else
        {
          browser = PlaceholderFactory.Create();
          //browser = (ILogonBrowser)Assembly.GetAssembly(typeof(LogonLabsControl)).CreateInstance("LogonLabs.Client.WinForms.PlaceholderBrowser");
        }
        System.Diagnostics.Debug.Assert(browser != null, "Browser == null.");
        ((Control)this.browser).Dock = DockStyle.Fill;
        this.Controls.Add((Control)this.browser);

        base.OnHandleCreated(e);
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Error ({ex.GetType().Name}): {ex.Message}\nStackTrace: {ex.StackTrace}");
      }
    }




  }
}
