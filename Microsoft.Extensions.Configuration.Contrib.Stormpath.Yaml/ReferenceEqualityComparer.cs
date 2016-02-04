using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration.Contrib.Stormpath.Yaml
{
    public class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
            => ReferenceEquals(x, y);

        public int GetHashCode(object obj)
            => obj.GetHashCode();
    }
}
