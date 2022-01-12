using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Patty.DBModels
{
    internal class Acronym
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Meaning { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Link { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Changed { get; set; }

        public ICollection<AcronymTag> AcronymTags { get; set; } = new ObservableCollection<AcronymTag>();
    }
}
