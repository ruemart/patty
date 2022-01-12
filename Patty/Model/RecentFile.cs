using System;

namespace Patty.Model
{
    internal class RecentFile
    {
        public string Name { get; set; } = string.Empty;
        
        public string Path { get; set; } = string.Empty;

        public DateTime LastUsed { get; set; }
    }
}
