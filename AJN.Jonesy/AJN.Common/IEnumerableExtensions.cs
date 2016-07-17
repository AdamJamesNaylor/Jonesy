
namespace AJN.Jonesy.Common {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class IEnumerableExtensions {
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> operand, int count = 1) {
            var rndGen = new Random();
            while (count-- > 0) {
                int random = rndGen.Next(0, operand.Count());
                yield return operand.ElementAt(random);
            }
        }

        public static IEnumerable<T> TakeRandomExclusive<T>(this IEnumerable<T> operand, int count = 1) {
            var rndGen = new Random();
            var enumerable = operand as T[] ?? operand.ToArray();
            if (count > enumerable.Length)
                count = enumerable.Length;

            var result = new int[count];
            for (int i = 0; i < count; i++) {

                var random = rndGen.Next(0, enumerable.Length);

                while (result.Contains(random)) {
                    random = rndGen.Next(0, enumerable.Length);
                }
                result[i] = random;
            }

            return result.Select(enumerable.ElementAt);
        }

        public static Collection<T> ToCollection<T>(this IEnumerable<T> operand) {
            return new Collection<T>(operand.ToList());
        }
    }
}
