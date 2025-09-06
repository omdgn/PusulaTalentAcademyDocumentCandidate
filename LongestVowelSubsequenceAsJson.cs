using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

public static class LongestVowelSubsequence
{
    private static readonly HashSet<char> Vowels = new HashSet<char>{'a','e','i','o','u'};

    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
            return "[]";

        var buffer = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(buffer))
        {
            writer.WriteStartArray();

            foreach (var word in words)
            {
                string longest = "";
                string current = "";

                foreach (char c in word)
                {
                    if (Vowels.Contains(char.ToLower(c)))
                    {
                        current += c;
                        if (current.Length > longest.Length)
                            longest = current;
                    }
                    else
                    {
                        current = "";
                    }
                }

                writer.WriteStartObject();
                writer.WriteString("word", word);
                writer.WriteString("sequence", longest);
                writer.WriteNumber("length", longest.Length);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }
}
