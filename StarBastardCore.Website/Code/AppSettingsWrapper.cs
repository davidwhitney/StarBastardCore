using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace StarBastardCore.Website.Code
{
    public interface IAppSettings
    {
        string this[string key] { get; set; }
        T Get<T>(string key, Func<T> defaultValue = null);
    }

    public class AppSettingsWrapper : IAppSettings
    {
        private readonly NameValueCollection _settings;

        public string this[string key]
        {
            get { return _settings[key]; }
            set { _settings[key] = value; }
        }

        public AppSettingsWrapper()
        {
            _settings = ConfigurationManager.AppSettings;
        }

        public AppSettingsWrapper(NameValueCollection settings)
        {
            _settings = settings;
        }

        public T Get<T>(string key, Func<T> defaultValue = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            defaultValue = defaultValue ?? (() => default(T));

            if (!_settings.AllKeys.Contains(key))
            {
                return defaultValue();
            }

            return (T)Convert.ChangeType(this[key], typeof(T));
        }
    }
}