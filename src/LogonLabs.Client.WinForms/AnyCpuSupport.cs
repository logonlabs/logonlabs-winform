using CefSharp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace LogonLabs.Client.WinForms
{
  /// <summary>
  /// If the application is built with "Any CPU" as the target then this will choose which of the CefSharp dlls to load. If
  /// the CPU at runtime is x64 it will load the 64 bit dll or if it's x86 it will load the 32 bit dll.
  /// </summary>
  public class AnyCPUSupport
  {
    internal static bool IsActivated { get; set; }

    /// <summary>
    /// Add support for "Any CPU". Call this method early before any UI is loaded containing the LogonLabsControl.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Initialize()
    {
      if (IsActivated)
        return;
      AppDomain.CurrentDomain.AssemblyResolve += Resolver;
      IsActivated = true;
    }
    private static Assembly Resolver(object sender, ResolveEventArgs args)
    {
      if (args.Name.StartsWith("CefSharp"))
      {
        string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
        string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                               Environment.Is64BitProcess ? "x64" : "x86",
                                               assemblyName);

        return File.Exists(archSpecificPath)
                   ? Assembly.LoadFile(archSpecificPath)
                   : null;
      }
      return null;
    }
  }
}
