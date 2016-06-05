using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace Goofy.Presentation.Core.Controllers
{
    public class BaseRestfulController<TEntity, TViewModel, TKey>
        where TEntity : BaseEntity where TViewModel : class
    {
        private readonly IServiceMapper<TEntity, TViewModel> _serviceMapper;

        public BaseRestfulController(IServiceMapper<TEntity, TViewModel> serviceMapper)
        {
            _serviceMapper = serviceMapper;
        }

        [HttpGet]
        public virtual IEnumerable<TViewModel> Get()
        {
            return _serviceMapper.GetAll();
        }

        [HttpGet("{id}")]
        public virtual TViewModel Get(TKey key)
        {
            return _serviceMapper.Find(new object[] { key });
        }

        [HttpPost]
        public virtual void Post([FromBody] TViewModel item)
        {
            _serviceMapper.Add(item);
        }

        [HttpPut("{id}")]
        public virtual void Update(TViewModel item)
        {
            _serviceMapper.Modify(item);
        }

        [HttpDelete("{id}")]
        public virtual void Delete(TViewModel item)
        {
            _serviceMapper.Remove(item);
        }
    }
}
