using System;
using System.Collections.Generic;

namespace Goofy.Domain.Core.Service.Adapter
{
    /// <summary>
    ///     Base contract for adapter factory
    /// </summary>
    public interface ITypeAdapterFactory
    {
        /// <summary>
        ///     Create a type adater
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetSourceTypes(Type targetType);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetTargetTypes(Type sourceType);
    }
}