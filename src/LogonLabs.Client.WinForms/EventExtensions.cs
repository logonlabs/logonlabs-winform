using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.WinForms
{
    static internal class EventExtensions
    {
        static internal void RaiseEvent<T>(this EventHandler<T> handler, object sender, T args)
    where T : EventArgs
            =>
            handler?.Invoke(sender, args);
    }
}
