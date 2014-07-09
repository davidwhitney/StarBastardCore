using System;
using System.Linq;
using System.Web;

namespace StarBastardCore.Website.Code.DataAccess
{
    public class Storage
    {        
        private readonly HttpContextBase _ctx;

        public Storage(HttpContextBase ctx)
        {
            _ctx = ctx;
            if (_ctx.Session == null)
            {
                throw new InvalidOperationException("No session storage, can't save data.");
            }
        }

        public TType GetOrEmpty<TType>(object id) where TType : ICanBeSaved, new()
        {
            var key = typeof(TType).Name + "_" + id;

            if (!_ctx.Application.Contents.AllKeys.Contains(key))
            {
                var newType = new TType { Id = id };
                return Save(newType);
            }

            return (TType)_ctx.Application.Contents[key];
        }

        public TType Get<TType>(object id) where TType : ICanBeSaved, new()
        {
            var key = typeof(TType).Name + "_" + id;

            if (!_ctx.Application.Contents.AllKeys.Contains(key))
            {
                return default(TType);
            }

            return (TType)_ctx.Application.Contents[key];
        }

        public TType Load<TType>(object id) where TType : ICanBeSaved
        {
            var key = typeof(TType).Name + "_" + id;
            return (TType)_ctx.Application.Contents[key];
        }

        public TType Save<TType>(TType item) where TType : ICanBeSaved
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