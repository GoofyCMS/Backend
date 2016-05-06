using System;
using System.Linq;
using System.Linq.Expressions;

namespace Goofy.Domain.Core.Service.Adapter
{
    /// <summary>
    ///     Base contract for map dto to aggregate or aggregate to dto.
    ///     <remarks>
    ///         This is a  contract for work with "auto" mappers ( automapper,emitmapper,valueinjecter...)
    ///         or adhoc mappers
    ///     </remarks>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        TTarget Adapt<TSource, TTarget>(TSource source) where TTarget : class where TSource : class;

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        TTarget Adapt<TTarget>(object source) where TTarget : class;

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <param name="target">Target instance</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        TTarget Adapt<TSource, TTarget>(TSource source, TTarget target) where TTarget : class where TSource : class;

        /// <summary>
        ///     Make a projection from a query of <typeparamref name="TSource" />'s objects to <typeparamref name="TTarget" />'s
        ///     objects
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">A query with objects to adapt</param>
        /// <param name="membersToExpand">Explicit members to expand</param>
        /// <returns></returns>
        IQueryable<TTarget> Project<TSource, TTarget>(IQueryable<TSource> source,
            params Expression<Func<TTarget, object>>[] membersToExpand) where TTarget : class
            where TSource : class;
    }
}