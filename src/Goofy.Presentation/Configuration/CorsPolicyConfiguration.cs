
namespace Goofy.Presentation.Configuration
{
    public class CorsPolicyConfiguration
    {
        public string Name { get; set; }
        public string[] Origins { get; set; } = new string[] { };
        public string[] Methods { get; set; } = new string[] { };
        public string[] Headers { get; set; } = new string[] { };
        public bool AllowCredentials { get; set; } = false;
    }
}
