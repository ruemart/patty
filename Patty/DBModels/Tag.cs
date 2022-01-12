using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Patty.DBModels
{
    internal class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<AcronymTag> AcronymTags { get; set; } = new ObservableCollection<AcronymTag>();
    }
}
