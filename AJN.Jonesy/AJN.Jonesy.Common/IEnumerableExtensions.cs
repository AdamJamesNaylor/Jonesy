
namespace AJN.Jonesy.Common {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions {
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> operand, int count = 1) {
            var rndGen = new Random((int) DateTime.Now.Ticks); // do this only once in your app/class/IoC container
            while (count-- > 0) {
                int random = rndGen.Next(0, operand.Count());
                yield return operand.ElementAt(random);
            }
        }
    }
}
