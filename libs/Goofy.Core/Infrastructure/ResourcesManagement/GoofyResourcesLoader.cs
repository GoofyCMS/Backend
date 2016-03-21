using System;
using System.Collections.Generic;
using System.Reflection;
using Goofy.Core.Configuration;


namespace Goofy.Core.Infrastructure
{
    public class GoofyResourcesLoader : IResourcesLoader
    {
        public IResourcesLocator Locator
        {
            get;
            private set;
        }

        public GoofyResourcesLoader(IResourcesLocator resourcesLocator)
        {
            Locator = resourcesLocator;
        }

        public IEnumerable<Type> FindClassesOfType(Type type, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                var assemblies = Locator.GetAssemblies();
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        //if (!ignoreReflectionErrors)
                        //{
                        throw;
                        //}
                    }
                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (type.IsAssignableFrom(t) || (type.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(t, type)))
                            {
                                if (!t.IsInterface)
                                {
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.IsClass && !t.IsAbstract)
                                        {
                                            result.Add(t);
                                        }
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                //Debug.WriteLine(fail.Message, fail);

                throw fail;
            }
            return result;
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClases = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClases);
        }

        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
