using LogonLabs.Client.Winforms.Example.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogonLabs.Client.Winforms.Example
{
  public partial class MainWindow : Form
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
      var appId = Properties.Settings.Default.AppId;
      var apiUrl = Properties.Settings.Default.ApiUrl;
      if (new[] { appId, apiUrl }.Any(string.IsNullOrEmpty))
      {
        MessageBox.Show("Settings incomplete, check documentation.");
        Application.Exit();
      }
      // The control needs the API URL, AppID, and AppSecret so it can call into the LogonLabs API.
      logonLabsControl1.ApiUrl = apiUrl;
      logonLabsControl1.AppId = appId;
      // LogonComplete is fired when the user logs into the provider.
      logonLabsControl1.LogonComplete += LogonLabsControl1_LogonComplete;
      // Hide the control until the user chooses a provider from the menu.
      logonLabsControl1.Hide();



      Task.Run(() =>
      {
        var client = new LogonClient(Settings.Default.AppId, Settings.Default.ApiUrl);
        // Retrieve the providers enabled for the app.
        try
        {
          var response = client.GetProviders();
          var socialProviders = response.social_identity_providers.Select(sp => sp.type).ToArray();
          var enterpriseProviders = response.enterprise_identity_providers.Select(ep => ep.type).ToArray();

          this.BeginInvoke(((Action)(() =>
          {
            var allProviders = socialProviders.Concat(enterpriseProviders).OrderBy(s => s).ToList();
            if(!allProviders.Any())
            {
              logonLabsMenu.DropDownItems.Add("No providers enabled", null);
            }
            foreach (var provider in allProviders)
            {
              // The providers are added to the menu where the user can choose which provider they would like to use.
              var it = logonLabsMenu.DropDownItems.Add(provider, null, new EventHandler(ProviderMenuItem_Click));
              it.Tag = provider;
            }
          })));
        }
        catch (Exception ex)
        {
          this.BeginInvoke((Action)(() =>
          {
            MessageBox.Show("Could not reach server. Please check your settings and start again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }));

        }

      });

    }
    /// <summary>
    /// Following login with the provider, the browser will be redirected to the callback URL. The server
    /// will be provided with the destination URI, and the token. The server is responsible for verifying
    /// the token with a call to VerifyToken (LogonLabs API), and then redirecting the browser to the
    /// destination URI.
    /// 
    /// When the browser is redirected to the destination URI the LogonLabsControl will extract the payload
    /// from the server and provide it to this event handler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void LogonLabsControl1_LogonComplete(object sender, WinForms.LogonEventArgs e)
    {
      // ErrorCode will be empty if successful.
      if (!string.IsNullOrEmpty(e.ErrorCode))
      {
        this.BeginInvoke(new Action(() =>
        {
          MessageBox.Show($"LogonLabs return error code {e.ErrorCode}. Please try again.");
        }));
        return;
      }
      var payloadTxt = e.Payload;
      var payload = JsonConvert.DeserializeObject<AppUserInfo>(payloadTxt);

      this.BeginInvoke(new Action(() =>
      {
        if (payload?.Email != null)
        {
          MessageBox.Show($"Hello {payload.FirstName} {payload.LastName}! You have sucessfully logged in using {payload.Provider}.");
        }
        else
        {
          MessageBox.Show("Logon failed, Token was invalid. Please try again.");
        }
        logonLabsControl1.Hide();
      }));
    }

    private void ProviderMenuItem_Click(object sender, EventArgs e)
    {
      var mnuItem = sender as ToolStripItem;
      var provider = mnuItem?.Tag as string;
      if (provider != null)
      {
        logonLabsControl1.Provider = provider;
        logonLabsControl1.Show();
      }
    }
  }
}
