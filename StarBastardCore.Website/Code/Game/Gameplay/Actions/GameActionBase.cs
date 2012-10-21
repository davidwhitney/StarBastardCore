using System.Collections.Generic;

namespace StarBastardCore.Website.Code.Game.Gameplay.Actions
{
    public class GameActionBase
    {
        public string ActionName { get { return GetType().Name; } }
        public Dictionary<string, object> Parameters { get; set; }

        public GameActionBase()
        {
            Parameters = new Dictionary<string, object>();
        }

        public GameActionBase(GameActionBase action)
        {
            Parameters = action.Parameters;
        }

        public GameActionBase(Dictionary<string, object> items)
        {
            foreach(var item in items)
            {
                Parameters.Add(item.Key, item.Value);
            }
        }

        public TType Item<TType>(string key)
        {
            if(!Parameters.ContainsKey(key))
            {
                return default(TType);
            }

            return (TType)Parameters[key];
        }
    }
}