namespace TheBedstand.Common.Helpers
{
    public static class NameHelper
    {
        public static string GetFullName(string personalName, string surname)
            => $"{personalName} {surname}";
    }
}
