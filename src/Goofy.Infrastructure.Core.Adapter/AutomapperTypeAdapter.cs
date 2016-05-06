using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Infrastructure.Core.Adapters
{
    /// <summary>
    ///     Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter : ITypeAdapter
    {
        protected MapperConfiguration Config { get; }
        protected IMapper Mapper { get; }

        public AutomapperTypeAdapter(MapperConfiguration config)
        {
            Config = config;
            Mapper = config.CreateMapper();
        }

        #region Implementation of ITypeAdapter

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        ///     Adapt a source object to an instance of type <typeparamref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <param name="target">Target instance</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target) where TSource : class
            where TTarget : class
        {
            return Mapper.Map(source, target);
        }

        /// <summary>
        ///     Make a projection from a query of <typeparamref name="TSource" />'s objects to <typeparamref name="TTarget" />'s
        ///     objects
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">A query with objects to adapt</param>
        /// <param name="membersToExpand">Explicit members to expand</param>
        /// <returns></returns>
        public IQueryable<TTarget> Project<TSource, TTarget>(IQueryable<TSource> source,
            params Expression<Func<TTarget, object>>[] membersToExpand) where TSource : class where TTarget : class
        {
            return source.ProjectTo(Config, membersToExpand);
        }

        #endregion
    }
}