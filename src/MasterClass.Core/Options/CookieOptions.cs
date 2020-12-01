using System;

namespace MasterClass.Core.Options
{
    public class CookieOptions
    {
        public bool Enabled { get; set; }
        public string Issuer { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
    }
}