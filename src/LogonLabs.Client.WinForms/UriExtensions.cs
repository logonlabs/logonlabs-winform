using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogonLabs.Client.Wpf
{
    public static class UriExtensions
    {
        private static readonly Regex _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)", RegexOptions.Compiled);

        public static IEnumerable<KeyValuePair<string, string>> QueryString(this Uri uri)
        {
            var m = _regex.Match(uri.PathAndQuery);
            while (m.Success)
            {
                yield return new KeyValuePair<string, string>(m.Groups[1].Value, m.Groups[2].Value);
                m = m.NextMatch();
            }
        }
    }
}
