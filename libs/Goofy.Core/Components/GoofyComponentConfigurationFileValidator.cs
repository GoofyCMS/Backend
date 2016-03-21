using System.IO;
using Newtonsoft.Json.Linq;
using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    public class GoofyComponentConfigurationFileValidator : IComponentsConfigurationFileValidator
    {
        public bool IsValid(string componentJsonConfigFilePath, string componentFolderPath)
        {
            //Extraer el nombre de la componente
            var fileInfo = new FileInfo(componentFolderPath);
            var componentName = fileInfo.Name;
            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(File.ReadAllText(componentJsonConfigFilePath));
            }
            catch
            {
                return false;
            }
            JToken token;
            if (jsonObject.Count != 1 || !jsonObject.TryGetValue(componentName, out token))
                return false;
            return true;
        }
    }
}
