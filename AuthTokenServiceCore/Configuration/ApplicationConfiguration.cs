using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthTokenServiceCore.Configuration
{
    public enum ApplicationEnvironment
    {
        Localhost,
        Development,
        Evaluation,
        Production
    }
    public static class ApplicationSettings
    {
        public static ApplicationOptions ApplicationOptions { get; set; } = new ApplicationOptions();
    }
    public class ApplicationOptions
    {
        public ApplicationEnvironment Environment { get; set; }
        public string EnvironmentName =>
            Environment == ApplicationEnvironment.Localhost ? "Localhost" :
            Environment == ApplicationEnvironment.Development ? "Development" :
            Environment == ApplicationEnvironment.Evaluation ? "Evaluation" :
            Environment == ApplicationEnvironment.Production ? "Production" :
            "Unknown";
        public string EncryptionKey { get; set; } = string.Empty;

        public Uri AudienceUri { get; set; }
        public string ConfirmationMethod { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Namespace { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int ValidFor { get; set; }

    }
}
