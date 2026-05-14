
namespace ProductSimple.Infrastructure
{
    public static class BuildInfo
    {
        public static readonly DateTime builtAt = GetBuildDate();

        private static DateTime GetBuildDate()
        {
            var filePath = typeof(BuildInfo).Assembly.Location;

            if (File.Exists(filePath))
                return File.GetLastWriteTimeUtc(filePath);
            else
                return DateTime.UtcNow;

        }
    }
}
