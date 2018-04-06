using System;
using System.Collections.Generic;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Extensions
{
    public static class StringsHelper
    {
        public static string FirstLetterToLower(this string name)
            => char.ToLowerInvariant(name[0]) + name.Substring(1);

        public static bool Empty(this string value)
            => string.IsNullOrEmpty(value);

    }
}
