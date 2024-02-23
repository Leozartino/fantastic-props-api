namespace Core.Models
{
    public class ApplicationConfig
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
        public JwtSettings? JwtSettings { get; set; }
        public SwaggerUris? SwaggerUris { get; set; }
        public string[]? Cors { get; set; }
        public string? ApiUrl { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }

    public class SwaggerUris
    {
        public string? ContactUri { get; set; }
        public string? LicenseUri { get; set; }
    }


}
