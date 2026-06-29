using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Models
{
    public class MetadataFile
    {
        public Dictionary<string, CollectionMetadata> Collections { get; init; } = [];
    }

    public class CollectionMetadata
    {
        public string Title { get; init; } = "";

        public string Kind => "collection";

        public string ItemType { get; init; } = "";

        public List<FieldMetadata> Columns { get; init; } = [];
    }

    public class FieldMetadata
    {
        public string Property { get; init; } = "";

        public string Label { get; init; } = "";

        public string Type { get; init; } = "";
    }
}
