using CefSharp;
using CefSharp.WinForms;
using LogonLabs.Client.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.WinForms
{
  internal class LogonChromiumBrowser : ChromiumWebBrowser, ILogonBrowser
  {

    private string apiUrl = "https://api.logonlabs.com";
    private string appId = null;
    private string provider = null;


    public string AppId
    {
      get => appId;
      set
      {
        appId = value;
        StartLoginAndNavigate();
      }
    }

    public string ApiUrl
    {
      get => apiUrl;
      set
      {
        apiUrl = value;
        StartLoginAndNavigate();
      }
    }
    
    public string Provider
    {
      get => provider;
      set
      {
        provider = value;
        StartLoginAndNavigate();
      }
    }

    public LogonChromiumBrowser() :base("", new RequestContext(new RequestContextSettings { CachePath = "" }))
    {
      
    }

    public event EventHandler<LogonEventArgs> LogonComplete;

    public bool UseDestinationMode
    {
      get; set;
    }

    public void Navigate(string url)
    {
      throw new NotImplementedException();
    }

    public void Initialize(string url)
    {
      var r = BeginInvoke((Action)(() =>
      {
        LoadingStateChanged += Browser_LoadingStateChanged;
        LoadError += Browser_LoadError;
        Load(url);
      }));
      EndInvoke(r);
    }
    public void Initialize()
    {
      var url = StartLogin();
      Initialize(url);
    }



    private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
    {
      if (e.IsLoading)
      {
        this.Invoke(new Action(() =>
        {
          if (UseDestinationMode)
            // in destination mode we're waiting for a redirect to "logonlabs://app-id" which isn't a scheme supported by WebView.
            return;

          var uri = new Uri(Address);
          var qargs = uri.QueryString().Where(item =>
              item.Key == "app_id" || item.Key == "token").ToDictionary(t => t.Key, t => t.Value);
          var app_id = "";
          var token = "";
          if (qargs.TryGetValue("app_id", out app_id) && qargs.TryGetValue("token", out token) && app_id == this.AppId)
          {
            LogonComplete.RaiseEvent(this, new LogonEventArgs()
            {
              Payload = token
            });
          }
        }));
      }
    }
    private void Browser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
    {
      if (e.ErrorCode == CefSharp.CefErrorCode.UnknownUrlScheme)
      {
        var uri = new Uri(e.FailedUrl);
        if (IsDestinationUri(uri))
        {
          var payload = ExtractPayload(uri);
          Invoke(new Action(() => LogonComplete.RaiseEvent(this, new LogonEventArgs()
          {
            Payload = payload
          })));
        }
      }
      else
      {
        Invoke(new Action(() => LogonComplete.RaiseEvent(this, new LogonEventArgs()
        {
          ErrorCode = e.ErrorCode.ToString(),
          ErrorText = e.ErrorText
        })));
      }
    }
    /// <summary>
    /// Extracts the payload from the redirect to the destination URI.
    /// </summary>
    /// <param name="uri">The destination URI. Expected to look like https://host?payload=base64-json</param>
    /// <returns>Decoded text or null if the payload is missing/malformed.</returns>
    private string ExtractPayload(System.Uri uri)
    {
      var b64Payload = Uri.UnescapeDataString(uri.QueryString().FirstOrDefault(kv => kv.Key.Equals("payload", StringComparison.InvariantCultureIgnoreCase)).Value);

      if (string.IsNullOrEmpty(b64Payload))
        return null;
      return Encoding.UTF8.GetString(Convert.FromBase64String(b64Payload));
    }

    private bool IsDestinationUri(Uri uri) =>
        uri.Scheme.Equals(LogonLabsScheme, StringComparison.InvariantCultureIgnoreCase);

    private const string LogonLabsScheme =
        "logonlabs";
    private string DestinationUri =>
        $"{LogonLabsScheme}://{AppId}";

    private void StartLoginAndNavigate()
    {
      var url = StartLogin();
      if (!string.IsNullOrEmpty(url))
      {
        Load(url);
      }
    }
    /// <summary>
    /// Calls Logon Labs StartLogin method that will give us a URL for logon.
    /// </summary>
    /// <returns>The URL that the browser should navigate to.</returns>
    private string StartLogin()
    {


      // Check that all the settings are valid and that the Provider has been set.
      if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(ApiUrl) || Provider == null)
        return "";
      var client = new LogonClient(AppId, ApiUrl);
      if (UseDestinationMode)
        return client.StartLogin(Provider, destinationUrl: DestinationUri);
      else
        return client.StartLogin(Provider);
    }

  }
}
