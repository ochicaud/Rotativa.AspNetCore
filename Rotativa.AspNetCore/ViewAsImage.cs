﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
#elif NETCOREAPP3_1_OR_GREATER
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
#elif NETSTANDARD2_0
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
#endif


namespace Rotativa.AspNetCore
{
    public class ViewAsImage : AsImageResultBase
    {
        public ViewAsImage(ViewDataDictionary viewData = null, bool isPartialView = false, bool setBaseUrl = true)
        {
            this.WkhtmlPath = string.Empty;
            this.ViewData = viewData ?? new ViewDataDictionary(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary());
            this.ViewName = string.Empty;
            this.IsPartialView = isPartialView;
            this.SetBaseUrl = setBaseUrl;
        }

        public ViewAsImage(string viewName, ViewDataDictionary viewData = null, bool isPartialView = false, bool setBaseUrl = true)
            : this(viewData, isPartialView, setBaseUrl)
        {
            this.ViewName = viewName;
        }

        public ViewAsImage(object model, ViewDataDictionary viewData = null, bool isPartialView = false, bool setBaseUrl = true)
            : this(viewData, isPartialView, setBaseUrl)
        {
            this.ViewData.Model = model;
        }

        public ViewAsImage(string viewName, object model, ViewDataDictionary viewData = null, bool isPartialView = false, bool setBaseUrl = true)
            : this(viewData, isPartialView, setBaseUrl)
        {
            this.ViewName = viewName;
            this.ViewData.Model = model;
        }

        protected override string GetUrl(ActionContext context)
        {
            return string.Empty;
        }

        protected override async Task<byte[]> CallTheDriver(ActionContext context)
        {
            return WkhtmltoimageDriver.ConvertHtml(this.WkhtmlPath, this.GetConvertOptions(), await GetHtmlFromView(context));
        }
    }
}