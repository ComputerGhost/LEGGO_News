using System;
using System.Linq;

namespace Data.Constants
{
    public class MediaSize
    {
        public const string Original = "Original";
        public const string Thumbnail = "Thumbnail";
        public const string Small = "Small";
        public const string Medium = "Medium";
        public const string Large = "Large";

        public static int Compare(string a, string b)
        {
            if (a == b)
                return 0;

            switch (a) {
                case Original:
                    return -1;
                case Thumbnail:
                    if (new[] { Small, Medium, Large }.Contains(b))
                        return -1;
                    return 1;
                case Small:
                    if (new[] { Medium, Large }.Contains(b))
                        return -1;
                    return 1;
                case Medium:
                    if (new[] { Large }.Contains(b))
                        return -1;
                    return 1;
                case Large:
                    return 1;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
