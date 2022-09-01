#pragma checksum "C:\Users\bryan.campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\_ConfiguracionInicial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2d0949935de87e1f594cf704d9d33af8f6536bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__ConfiguracionInicial), @"mvc.1.0.view", @"/Views/Home/_ConfiguracionInicial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\bryan.campos\Documents\GitHub\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bryan.campos\Documents\GitHub\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2d0949935de87e1f594cf704d9d33af8f6536bc", @"/Views/Home/_ConfiguracionInicial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8cb12e4f445e7635817efa861e2d4a381810a7c2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home__ConfiguracionInicial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/generic.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/PeticionesAJX/configuracionInicial.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""row"">
    <div class=""card offset-lg-2 col-lg-8 col-sm-12"">
        <div class=""card-header bg-dark"">
            <h5 class=""card-title text-light""><i class=""fas fa-cogs""></i> Configuración inicial del sistema.</h5>
        </div>
        <div class=""card-body"">
            <div class=""row"">
                <!--Facturas-->
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group"">
                        <label>Inicio de las facturas:</label>
                        <input type=""number"" min=""0"" id=""txtIF"" placeholder=""Escribe el inicio de las facturas"" class=""form-control"" />
                    </div>
                </div>
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group"">
                        <label>Hasta:</label>
                        <input type=""number"" min=""0"" id=""txtFF"" placeholder=""Escribe el fin de las facturas"" class=""form-control"" />
                    </div>
                </div>
 ");
            WriteLiteral(@"               <input type=""hidden"" id=""txtDF"" class=""form-control"" value=""8"" />
                <!--cotizaciones-->
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group"">
                        <label>Inicio de las cotizaciones:</label>
                        <input type=""number"" min=""0"" id=""txtIC"" placeholder=""Escribe el inicio de las cotizaciones"" class=""form-control"" />
                    </div>
                </div>
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group"">
                        <label>Hasta:</label>
                        <input type=""number"" min=""0"" id=""txtFC"" placeholder=""Escribe el fin de las cotizaciones"" class=""form-control"" />
                    </div>
                </div>
                <input type=""hidden"" id=""txtDC"" class=""form-control"" value=""8"" />
                <!--Credito fiscal-->
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group""");
            WriteLiteral(@">
                        <label>Inicio del credito fiscal:</label>
                        <input type=""number"" min=""0"" id=""txtICF"" placeholder=""Escribe el inicio del credito fiscal"" class=""form-control r"" />
                    </div>
                </div>
                <div class=""col-lg-6 col-sm-12"">
                    <div class=""form-group"">
                        <label>Hasta:</label>
                        <input type=""number"" min=""0"" id=""txtFCF"" placeholder=""Escribe el fin del credito fiscal"" class=""form-control r"" />
                    </div>
                </div>
                <input type=""hidden"" id=""txtDCF"" class=""form-control"" value=""8""  />
            </div>
        </div>
        <div class=""card-footer"">
            <button type=""button"" class=""btn btn-primary"" onclick=""saveConfiguracion()"">Guardar Configuración</button>
        </div>
    </div>
</div>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2d0949935de87e1f594cf704d9d33af8f6536bc7134", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2d0949935de87e1f594cf704d9d33af8f6536bc8173", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
