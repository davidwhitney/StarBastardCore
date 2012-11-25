using System;
using System.Linq;
using System.Web;

namespace StarBastardCore.Website.Code.DataAccess
{
    public class Repository<TType> where TType : ICanBeSaved
    {        
        private readonly HttpContextBase _ctx;

        public Repository(HttpContextBase ctx)
        {
            _ctx = ctx;
            if (_ctx.Session == null)
            {
                throw new InvalidOperationException("No session storage, can't save data.");
            }
        }

        public TType Load(Guid id)
        {
            var key = typeof(TType).Name + "_" + id;
            return (TType)_ctx.Application.Contents[key];
        }

        public TType Save(TType item)
        {
            var key = typeof(TType).Name + "_" + item.Id;

            if (_ctx.Application.Contents.AllKeys.Contains(key))
            {
                _ctx.Application.Contents.Remove(key);
            }

            _ctx.Application.Contents.Add(key, item);

            return item;
        }
    }
}