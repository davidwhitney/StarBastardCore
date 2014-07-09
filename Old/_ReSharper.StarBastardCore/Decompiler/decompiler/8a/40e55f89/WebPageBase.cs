// Type: System.Web.WebPages.WebPageBase
// Assembly: System.Web.WebPages, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v2.0\Assemblies\System.Web.WebPages.dll

using Microsoft.Internal.Web.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
  public abstract class WebPageBase : WebPageRenderingBase
  {
    private HashSet<string> _renderedSections = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private bool _renderedBody;
    private Action<TextWriter> _body;
    private TextWriter _tempWriter;
    private TextWriter _currentWriter;
    private DynamicPageDataDictionary<object> _dynamicPageData;

    public override string Layout { get; set; }

    public TextWriter Output
    {
      get
      {
        return this.OutputStack.Peek();
      }
    }

    public Stack<TextWriter> OutputStack
    {
      get
      {
        return this.PageContext.OutputStack;
      }
    }

    public override IDictionary<object, object> PageData
    {
      get
      {
        return this.PageContext.PageData;
      }
    }

    public override object Page
    {
      get
      {
        if (this._dynamicPageData == null)
          this._dynamicPageData = new DynamicPageDataDictionary<object>((PageDataDictionary<object>) this.PageData);
        return (object) this._dynamicPageData;
      }
    }

    Dictionary<string, SectionWriter> PreviousSectionWriters
    {
      private get
      {
        Dictionary<string, SectionWriter> dictionary1 = this.SectionWritersStack.Pop();
        Dictionary<string, SectionWriter> dictionary2 = this.SectionWritersStack.Count > 0 ? this.SectionWritersStack.Peek() : (Dictionary<string, SectionWriter>) null;
        this.SectionWritersStack.Push(dictionary1);
        return dictionary2;
      }
    }

    Dictionary<string, SectionWriter> SectionWriters
    {
      private get
      {
        return this.SectionWritersStack.Peek();
      }
    }

    Stack<Dictionary<string, SectionWriter>> SectionWritersStack
    {
      private get
      {
        return this.PageContext.SectionWritersStack;
      }
    }

    protected virtual void ConfigurePage(WebPageBase parentPage)
    {
    }

    public static WebPageBase CreateInstanceFromVirtualPath(string virtualPath)
    {
      return WebPageBase.CreateInstanceFromVirtualPath(virtualPath, (IVirtualPathFactory) VirtualPathFactoryManager.Instance);
    }

    internal static WebPageBase CreateInstanceFromVirtualPath(string virtualPath, IVirtualPathFactory virtualPathFactory)
    {
      try
      {
        WebPageBase instance = VirtualPathFactoryExtensions.CreateInstance<WebPageBase>(virtualPathFactory, virtualPath);
        instance.VirtualPath = virtualPath;
        instance.VirtualPathFactory = virtualPathFactory;
        return instance;
      }
      catch (HttpException ex)
      {
        BuildManagerExceptionUtil.ThrowIfUnsupportedExtension(virtualPath, ex);
        throw;
      }
    }

    private WebPageBase CreatePageFromVirtualPath(string virtualPath, HttpContextBase httpContext, Func<string, bool> virtualPathExists, DisplayModeProvider displayModeProvider, IDisplayMode displayMode)
    {
      try
      {
        DisplayInfo infoForVirtualPath = displayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, httpContext, virtualPathExists, displayMode);
        if (infoForVirtualPath != null)
        {
          WebPageBase instance = VirtualPathFactoryExtensions.CreateInstance<WebPageBase>(this.VirtualPathFactory, infoForVirtualPath.FilePath);
          if (instance != null)
          {
            instance.VirtualPath = virtualPath;
            instance.VirtualPathFactory = this.VirtualPathFactory;
            instance.DisplayModeProvider = this.DisplayModeProvider;
            return instance;
          }
        }
      }
      catch (HttpException ex)
      {
        BuildManagerExceptionUtil.ThrowIfUnsupportedExtension(virtualPath, ex);
        BuildManagerExceptionUtil.ThrowIfCodeDomDefinedExtension(virtualPath, ex);
        throw;
      }
      throw new HttpException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, WebPageResources.WebPage_InvalidPageType, new object[1]
      {
        (object) virtualPath
      }));
    }

    private WebPageContext CreatePageContextFromParameters(bool isLayoutPage, params object[] data)
    {
      object model = (object) null;
      if (data != null && data.Length > 0)
        model = data[0];
      return WebPageContext.CreateNestedPageContext<object>(this.PageContext, PageDataDictionary<object>.CreatePageDataFromParameters(this.PageData, data), model, isLayoutPage);
    }

    public void DefineSection(string name, SectionWriter action)
    {
      if (this.SectionWriters.ContainsKey(name))
        throw new HttpException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionAleadyDefined, new object[1]
        {
          (object) name
        }));
      else
        this.SectionWriters[name] = action;
    }

    internal void EnsurePageCanBeRequestedDirectly(string methodName)
    {
      if (this.PreviousSectionWriters != null)
        return;
      throw new HttpException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, WebPageResources.WebPage_CannotRequestDirectly, new object[2]
      {
        (object) this.VirtualPath,
        (object) methodName
      }));
    }

    public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer)
    {
      WebPageBase webPageBase = this;
      WebPageRenderingBase pageRenderingBase = (WebPageRenderingBase) null;
      WebPageContext pageContext1 = pageContext;
      TextWriter writer1 = writer;
      WebPageRenderingBase startPage = pageRenderingBase;
      webPageBase.ExecutePageHierarchy(pageContext1, writer1, startPage);
    }

    public void ExecutePageHierarchy(WebPageContext pageContext, TextWriter writer, WebPageRenderingBase startPage)
    {
      this.PushContext(pageContext, writer);
      if (startPage != null)
      {
        if (startPage != this)
        {
          WebPageContext nestedPageContext = WebPageContext.CreateNestedPageContext<object>(pageContext, (IDictionary<object, object>) null, (object) null, false);
          nestedPageContext.Page = startPage;
          startPage.PageContext = nestedPageContext;
        }
        startPage.ExecutePageHierarchy();
      }
      else
        base.ExecutePageHierarchy();
      this.PopContext();
    }

    public override void ExecutePageHierarchy()
    {
      if (WebPageHttpHandler.ShouldGenerateSourceHeader(this.Context))
      {
        try
        {
          string virtualPath = this.VirtualPath;
          if (virtualPath != null)
          {
            string str = this.Context.Request.MapPath(virtualPath);
            if (!StringExtensions.IsEmpty(str))
              this.PageContext.SourceFiles.Add(str);
          }
        }
        catch
        {
        }
      }
      TemplateStack.Push(this.Context, (ITemplateFile) this);
      try
      {
        this.Execute();
      }
      finally
      {
        TemplateStack.Pop(this.Context);
      }
    }

    protected virtual void InitializePage()
    {
    }

    public bool IsSectionDefined(string name)
    {
      this.EnsurePageCanBeRequestedDirectly("IsSectionDefined");
      return this.PreviousSectionWriters.ContainsKey(name);
    }

    public void PopContext()
    {
      string renderedContent = this._tempWriter.ToString();
      this.OutputStack.Pop();
      if (!string.IsNullOrEmpty(this.Layout))
      {
        string partialViewName = this.NormalizeLayoutPagePath(this.Layout);
        this.OutputStack.Push(this._currentWriter);
        this.RenderSurrounding(partialViewName, (Action<TextWriter>) (w => w.Write(renderedContent)));
        this.OutputStack.Pop();
      }
      else
        this._currentWriter.Write(renderedContent);
      this.VerifyRenderedBodyOrSections();
      this.SectionWritersStack.Pop();
    }

    public void PushContext(WebPageContext pageContext, TextWriter writer)
    {
      this._currentWriter = writer;
      this.PageContext = pageContext;
      pageContext.Page = (WebPageRenderingBase) this;
      this.InitializePage();
      this._tempWriter = (TextWriter) new StringWriter((IFormatProvider) CultureInfo.InvariantCulture);
      this.OutputStack.Push(this._tempWriter);
      this.SectionWritersStack.Push(new Dictionary<string, SectionWriter>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
      if (this.PageContext.BodyAction == null)
        return;
      this._body = this.PageContext.BodyAction;
      this.PageContext.BodyAction = (Action<TextWriter>) null;
    }

    public HelperResult RenderBody()
    {
      this.EnsurePageCanBeRequestedDirectly("RenderBody");
      if (this._renderedBody)
        throw new HttpException(WebPageResources.WebPage_RenderBodyAlreadyCalled);
      this._renderedBody = true;
      if (this._body != null)
        return new HelperResult((Action<TextWriter>) (tw => this._body(tw)));
      throw new HttpException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, WebPageResources.WebPage_CannotRequestDirectly, new object[2]
      {
        (object) this.VirtualPath,
        (object) "RenderBody"
      }));
    }

    public override HelperResult RenderPage(string path, params object[] data)
    {
      WebPageBase webPageBase = this;
      bool flag = false;
      object[] objArray = data;
      string path1 = path;
      int num = flag ? 1 : 0;
      object[] data1 = objArray;
      return webPageBase.RenderPageCore(path1, num != 0, data1);
    }

    private HelperResult RenderPageCore(string path, bool isLayoutPage, object[] data)
    {
      if (string.IsNullOrEmpty(path))
        throw ExceptionHelper.CreateArgumentNullOrEmptyException("path");
      else
        return new HelperResult((Action<TextWriter>) (writer =>
        {
          path = this.NormalizePath(path);
          WebPageBase local_0 = this.CreatePageFromVirtualPath(path, this.Context, new Func<string, bool>(this.VirtualPathFactory.Exists), this.DisplayModeProvider, this.DisplayMode);
          WebPageContext local_1 = this.CreatePageContextFromParameters(isLayoutPage, data);
          local_0.ConfigurePage(this);
          local_0.ExecutePageHierarchy(local_1, writer);
        }));
    }

    public HelperResult RenderSection(string name)
    {
      WebPageBase webPageBase = this;
      bool flag = true;
      string name1 = name;
      int num = flag ? 1 : 0;
      return webPageBase.RenderSection(name1, num != 0);
    }

    public HelperResult RenderSection(string name, bool required)
    {
      this.EnsurePageCanBeRequestedDirectly("RenderSection");
      if (this.PreviousSectionWriters.ContainsKey(name))
        return new HelperResult((Action<TextWriter>) (tw =>
        {
          if (this._renderedSections.Contains(name))
          {
            throw new HttpException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionAleadyRendered, new object[1]
            {
              (object) name
            }));
          }
          else
          {
            SectionWriter local_0 = this.PreviousSectionWriters[name];
            Dictionary<string, SectionWriter> local_1 = this.SectionWritersStack.Pop();
            bool local_2 = false;
            try
            {
              if (this.Output != tw)
              {
                this.OutputStack.Push(tw);
                local_2 = true;
              }
              local_0();
            }
            finally
            {
              if (local_2)
                this.OutputStack.Pop();
            }
            this.SectionWritersStack.Push(local_1);
            this._renderedSections.Add(name);
          }
        }));
      if (!required)
        return (HelperResult) null;
      throw new HttpException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, WebPageResources.WebPage_SectionNotDefined, new object[1]
      {
        (object) name
      }));
    }

    private void RenderSurrounding(string partialViewName, Action<TextWriter> body)
    {
      Action<TextWriter> bodyAction = this.PageContext.BodyAction;
      this.PageContext.BodyAction = body;
      WebPageBase webPageBase1 = this;
      WebPageBase webPageBase2 = this;
      bool flag = true;
      object[] objArray = new object[0];
      string path = partialViewName;
      int num = flag ? 1 : 0;
      object[] data = objArray;
      HelperResult result = webPageBase2.RenderPageCore(path, num != 0, data);
      webPageBase1.Write(result);
      this.PageContext.BodyAction = bodyAction;
    }

    private void VerifyRenderedBodyOrSections()
    {
      if (this._body == null)
        return;
      if (this.SectionWritersStack.Count > 1 && this.PreviousSectionWriters != null && this.PreviousSectionWriters.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string str in this.PreviousSectionWriters.Keys)
        {
          if (!this._renderedSections.Contains(str))
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append("; ");
            stringBuilder.Append(str);
          }
        }
        if (stringBuilder.Length <= 0)
          return;
        throw new HttpException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, WebPageResources.WebPage_SectionsNotRendered, new object[2]
        {
          (object) this.VirtualPath,
          (object) ((object) stringBuilder).ToString()
        }));
      }
      else
      {
        if (this._renderedBody)
          return;
        throw new HttpException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, WebPageResources.WebPage_RenderBodyNotCalled, new object[1]
        {
          (object) this.VirtualPath
        }));
      }
    }

    public override void Write(HelperResult result)
    {
      WebPageExecutingBase.WriteTo(this.Output, result);
    }

    public override void Write(object value)
    {
      WebPageExecutingBase.WriteTo(this.Output, value);
    }

    public override void WriteLiteral(object value)
    {
      this.Output.Write(value);
    }

    protected internal override TextWriter GetOutputWriter()
    {
      return this.Output;
    }
  }
}
