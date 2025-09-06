// MaxIncreasingSubarraySolution.cs
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TestApp
{
    public static class MaxIncreasingSubarraySolution
    {
        public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                return "[]";

            int bestStart = 0, bestEnd = 0;
            long bestSum = numbers[0];

            int curStart = 0;
            long curSum = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    curSum += numbers[i];
                }
                else
                {
                    curStart = i;
                    curSum = numbers[i];
                }

                if (curSum > bestSum)
                {
                    bestSum = curSum;
                    bestStart = curStart;
                    bestEnd = i;
                }
            }

            // JSON'u ek kopya oluşturmadan yaz
            var buffer = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(buffer))
            {
                writer.WriteStartArray();
                for (int i = bestStart; i <= bestEnd; i++)
                    writer.WriteNumberValue(numbers[i]);
                writer.WriteEndArray();
            }
            return Encoding.UTF8.GetString(buffer.WrittenSpan);
        }
    }
}
