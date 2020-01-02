﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.WinForms
{
  internal static class PlaceholderFactory
  {
    internal static ILogonBrowser Create()
    {
      return new PlaceholderBrowser();
    }
  }
}
