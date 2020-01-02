using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace LogonLabs.Client.WinForms
{
  /// <summary>
  /// Based on: https://github.com/cefsharp/CefSharp/issues/1714
  /// </summary>
  internal class AnyCpuInitializer
  {
    internal static bool IsInitialized { get; set; }
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Initialize()
    {
      if (IsInitialized)
        return;
      if (!AnyCPUSupport.IsActivated)
        return;
      var settings = new CefSettings();

      // Set BrowserSubProcessPath based on app bitness at runtime
      settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                             Environment.Is64BitProcess ? "x64" : "x86",
                                             "CefSharp.BrowserSubprocess.exe");
      settings.CefCommandLineArgs.Add("disable-features", "NetworkService");
      settings.LogSeverity = LogSeverity.Verbose;
     
      // Make sure you set performDependencyCheck false
      Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
      IsInitialized = true;
    }

  }
}
