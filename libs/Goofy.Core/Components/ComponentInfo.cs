using System;

namespace Goofy.Core.Components
{
    public class ComponentInfo
    {
        //private readonly List<ComponentAttributes> _referencedAssemblies;

        public ComponentInfo(string fullName, Version version, string path)
        {
            FullName = fullName;
            Version = version;
            Path = path;
            //_referencedAssemblies = new List<ComponentAttributes>();
        }
        public string FullName { get; private set; }
        public Version Version { get; private set; }
        public string Path { get; private set; }
        //public string ConfigFilePath { get; private set; }
        //public string Name
        //{
        //    get
        //    {
        //        return FullName.Substring(0, FullName.IndexOf(','));
        //    }
        //}

        //public IEnumerable<ComponentAttributes> ReferencedAssemblies { get { return _referencedAssemblies; } }

        //public void AddReference(ComponentAttributes componentAttributes)
        //{
        //    _referencedAssemblies.Add(componentAttributes);
        //}
        //public void AddMultipleReferences(IEnumerable<ComponentAttributes> componentAttributes)
        //{
        //    _referencedAssemblies.AddRange(componentAttributes);
        //}
    }
}
