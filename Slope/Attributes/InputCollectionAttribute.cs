using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InputCollectionAttribute : Attribute
    {
        public string Title { get; }

        public int Order { get; }

        public InputCollectionAttribute(string title, int order)
        {
            Title = title;
            Order = order;
        }
    }
}
