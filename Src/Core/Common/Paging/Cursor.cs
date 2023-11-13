using Newtonsoft.Json;
using System.Text;

namespace Core.Common.Paging;

public struct Cursor<SortKeyType>
{
    public static Cursor<SortKeyType> FromEncodedString(string encoded)
    {
        try
        {
            var bytes = Convert.FromBase64String(encoded);
            var serialized = Encoding.UTF8.GetString(bytes);
            var value = JsonConvert.DeserializeObject<SortKeyType>(serialized);
            return new Cursor<SortKeyType>(value!);
        }
        catch
        {
            throw new InvalidCursorException();
        }
    }

    public static Cursor<SortKeyType> FromUnencodedValue(SortKeyType value)
    {
        return new Cursor<SortKeyType>(value);
    }

    private Cursor(SortKeyType value)
    {
        Value = value;
    }

    public SortKeyType Value { get; }

    /// <summary>
    /// Returns the encoded value of this cursor.
    /// </summary>
    public override string ToString()
    {
        var serialized = JsonConvert.SerializeObject(Value);
        var bytes = Encoding.UTF8.GetBytes(serialized);
        return Convert.ToBase64String(bytes);
    }
}
