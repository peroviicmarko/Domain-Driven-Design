namespace DDD.Application.Config
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string ConnectionString { get; set; }
        public string[] AllowedCorsDomains { get; set; }
        public string[] AllowedMethods { get; set; }
    }
}
