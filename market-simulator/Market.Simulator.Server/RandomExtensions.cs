using System;

namespace Market.Simulator.Server
{
    public static class RandomExtensions
    {
        public static int NextInt32(this Random random)
        {
            var firstBits = random.Next(0, 1 << 4) << 28;
            var lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimal(this Random random)
        {
            var scale = (byte) random.Next(29);
            var sign = random.Next(2) == 1;
            return new decimal(random.NextInt32(),
                random.NextInt32(),
                random.NextInt32(),
                sign,
                scale);
        }
    }
}