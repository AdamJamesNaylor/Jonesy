namespace AJN.Jonesy.Common {
    using System.Xml.Linq;

    public static class XElementExtensions {
        public static bool IsCanonical(this XElement operand) {
            return operand.Attribute("canonicalQuestion") == null;
        }
    }
}
