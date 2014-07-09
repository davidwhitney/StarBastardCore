using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public class GameActionBase
    {
        public string ActionName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public GameActionBase()
        {
            ActionName = GetType().Name;
            Parameters = new Dictionary<string, object>();
        }

        public GameActionBase(GameActionBase action)
        {
            ActionName = action.ActionName;
            Parameters = action.Parameters;
        }

        public GameActionBase(Dictionary<string, object> items)
        {
            ActionName = GetType().Name;
            foreach(var item in items)
            {
                Parameters.Add(item.Key, item.Value);
            }
        }

        public virtual void Commit(GameContext entireContext)
        {
            throw new InvalidOperationException("Implemente me in derived type.");
        }

        public TType Item<TType>(string key)
        {
            if(!Parameters.ContainsKey(key))
            {
                return default(TType);
            }

            var value = Parameters[key];

            try
            {
                return (TType)value;
            }
            catch
            {
                try
                {
                    return (TType) Convert.ChangeType(value, typeof (TType));
                }
                catch
                {
                    return (TType)TypeDescriptor.GetConverter(typeof(TType)).ConvertFromInvariantString(value.ToString());
                }
            }

        }
    }
}