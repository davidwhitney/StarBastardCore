using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace StarBastardCore.Website.Code.ModelBinding
{
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                controllerContext.HttpContext.Request.InputStream.Position = 0;
                var stringified = new StreamReader(controllerContext.HttpContext.Request.InputStream).ReadToEnd();

                return string.IsNullOrEmpty(stringified)
                           ? null
                           : JsonConvert.DeserializeObject(stringified, bindingContext.ModelType);
            }
        }
    }
}