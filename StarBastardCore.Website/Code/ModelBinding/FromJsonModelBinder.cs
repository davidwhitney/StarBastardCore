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
                var stringified = controllerContext.HttpContext.Request.Form.ToString();
                stringified = HttpUtility.UrlDecode(stringified);
                
                return string.IsNullOrEmpty(stringified)
                           ? null
                           : JsonConvert.DeserializeObject(stringified, bindingContext.ModelType);
            }
        }
    }
}