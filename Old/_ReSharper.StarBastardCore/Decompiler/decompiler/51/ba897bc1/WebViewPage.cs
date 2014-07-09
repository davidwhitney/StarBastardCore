// Type: System.Web.Mvc.WebViewPage`1
// Assembly: System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 4\Assemblies\System.Web.Mvc.dll

namespace System.Web.Mvc
{
  public abstract class WebViewPage<TModel> : WebViewPage
  {
    private ViewDataDictionary<TModel> _viewData;

    public AjaxHelper<TModel> Ajax { get; set; }

    public HtmlHelper<TModel> Html { get; set; }

    public TModel Model
    {
      get
      {
        return this.ViewData.Model;
      }
    }

    public ViewDataDictionary<TModel> ViewData
    {
      get
      {
        if (this._viewData == null)
          this.SetViewData((ViewDataDictionary) new ViewDataDictionary<TModel>());
        return this._viewData;
      }
      set
      {
        this.SetViewData((ViewDataDictionary) value);
      }
    }

    public override void InitHelpers()
    {
      base.InitHelpers();
      this.Ajax = new AjaxHelper<TModel>(this.ViewContext, (IViewDataContainer) this);
      this.Html = new HtmlHelper<TModel>(this.ViewContext, (IViewDataContainer) this);
    }

    protected override void SetViewData(ViewDataDictionary viewData)
    {
      this._viewData = new ViewDataDictionary<TModel>(viewData);
      base.SetViewData((ViewDataDictionary) this._viewData);
    }
  }
}
