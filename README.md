# LogonLabs Desktop
---
The official LogonLabs repository for Winforms components.

---

## LogonLabs API


- Prior to coding, some configuration is required at https://app.logonlabs.com/app/#/app-settings.

- For the full Developer Documentation please visit: https://app.logonlabs.com/api/

---

### Install Nuget Package

Open the Package Manager Console by opening Tools in the Visual Studio menu, then Nuget Package Manager, and clicking on Package Manager Console. Enter the following command to add the package to your project:

```shell
PM> Install-Package LogonLabs.Client.Winforms -Version <version retrieved from the web>

```

### Getting Started with Winforms
---

The package includes two controls, named LogonLabsProvidersControl and LogonLabsControl which can be used independently if desired. Both controls require an AppId and an AppSecret which can be found on the LogonLabs Portal. Logon or signup for a free account.

#### LogonLabsProvidersControl
---

This control lists the identity providers enabled for your app and allows the user to choose one. Drag the control from the Toolbar inside the Winforms designer onto your form. Assuming the name of the control is providersControl you assign the AppId and AppSecret to the control in the constructor of the form:

```csharp
            providersControl.AppId = appId;
            providersControl.AppSecret = appSecret;
```

The user will be presented with a list of providers that are enabled for the app. Clicking on one of the providers will trigger the ProviderClick event on the control. Add an event handler for ProviderClick that takes the provider selected and assigns the provider of the LogonLabsControl:

```csharp
public MainWindow()
{
    InitializeComponent();

	providersControl.ProviderClick += ProvidersControl_ProviderClick;
}

private void ProvidersControl_ProviderClick(object sender, WinForms.ProviderClickedEventArgs e)
{
    logonControl.Provider = e.ProviderType;
			
	// show logonControl / hide providersControl, or display logonControl in a dialog, etc.
}
```

#### LogonLabsControl

This control handles the api calls and takes the user to the appropriate login page. The Session property will contain the session token if the user logs in successfully.

To start the control needs the a few properties set ApiUrl, AppId, AppSecret and finally Provider. When all are valid the control will show the login page. Following a sucessful login the LogonComplete event will be triggered. Handle the LogonComplete event:

```csharp
public MainWindow()
{
	InitializeComponent();
	logonControl.LogonComplete += LogonControl_LogonComplete;
}
private void Logon_LogonComplete(object sender, WinForms.LogonEventArgs e)
{
	// replace backendClient with the client to your own api.
    if(string.IsNullOrEmpty(e.Token) || !backendClient.Logon(e.Token, cts.Token))
    {
        // logon failed, show error message, go back to the providers list so that the user can try again.
        return;
    }
    ShowForm();
}
```

Typically the session token would be sent to the backend server, which should call ValidateLogin to validate the token and retrieve the details. If the token is validated then the backend server would allow the Windows application access to its API. Either the backend uses the LogonLabs token or creates its own and passes it back.
