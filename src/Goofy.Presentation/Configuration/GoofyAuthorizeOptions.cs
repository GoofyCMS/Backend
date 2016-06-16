
namespace Goofy.Presentation.Configuration
{
    public class GoofyAuthorizeOptions
    {
        public string Path { get; set; } = "/permissions";

        public string ResourceField { get; set; } = "viewmodel";
        public string MethodField { get; set; } = "operation";
    }
}
