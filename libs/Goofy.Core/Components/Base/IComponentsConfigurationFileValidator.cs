
namespace Goofy.Core.Components.Base
{
    public interface IComponentsConfigurationFileValidator
    {
        bool IsValid(string componentJsonConfigFilePath, string componentFolderPath);
    }
}
