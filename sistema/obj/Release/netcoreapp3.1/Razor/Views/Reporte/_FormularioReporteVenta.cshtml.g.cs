#pragma checksum "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "229fe9956803f47e42912a280da33859ee887e57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reporte__FormularioReporteVenta), @"mvc.1.0.view", @"/Views/Reporte/_FormularioReporteVenta.cshtml")]
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
#line 1 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"229fe9956803f47e42912a280da33859ee887e57", @"/Views/Reporte/_FormularioReporteVenta.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8cb12e4f445e7635817efa861e2d4a381810a7c2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Reporte__FormularioReporteVenta : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmReporte"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ReporteVentas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Reporte", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btnReporte"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""card"">
    <div class=""card-header bg-dark"" id=""headingOne"">
        <h5 class=""mb-0"">
            <button class=""btn btn-link"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                <h4 class=""text-light""><i class=""fas fa-file-pdf""></i> Reportes de ventas</h4>
            </button>
        </h5>
    </div>
    <div id=""collapseOne"" class=""collapse"" aria-labelledby=""headingOne"" data-parent=""#accordion"">
        <div class=""card-body"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "229fe9956803f47e42912a280da33859ee887e575393", async() => {
                WriteLiteral(@"
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-lg-12 col-sm-12"">
                            <p>
                                Selecciona las fechas que quieres ver en tu reporte de ventas, si no seleccionas ninguna fecha se creara un
                                reporte general desde el inicio del sistema [");
#nullable restore
#line 17 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
                                                                        Write(ViewBag.fechaInicioSistema);

#line default
#line hidden
#nullable disable
                WriteLiteral("]; hasta el dia actual [");
#nullable restore
#line 17 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
                                                                                                                           Write(DateTime.Now.ToString("yyyy-MM-dd"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"].
                            </p>
                        </div>
                        <div class=""col-lg-6 col-sm-12"">
                            <div class=""form-group"">
                                <label>Desde:</label>
                                <input class=""form-control"" type=""date""");
                BeginWriteAttribute("min", " min=\"", 1343, "\"", 1376, 1);
#nullable restore
#line 23 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
WriteAttributeValue("", 1349, ViewBag.fechaInicioSistema, 1349, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("max", " max=\"", 1377, "\"", 1419, 1);
#nullable restore
#line 23 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
WriteAttributeValue("", 1383, DateTime.Now.ToString("yyyy-MM-dd"), 1383, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""Selecciona una fecha"" id=""desde"" />
                            </div>
                        </div>
                        <div class=""col-lg-6 col-sm-12"">
                            <div class=""form-group"">
                                <label>Hasta:</label>
                                <input class=""form-control"" type=""date""");
                BeginWriteAttribute("min", " min=\"", 1777, "\"", 1810, 1);
#nullable restore
#line 29 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
WriteAttributeValue("", 1783, ViewBag.fechaInicioSistema, 1783, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("max", " max=\"", 1811, "\"", 1853, 1);
#nullable restore
#line 29 "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Reporte\_FormularioReporteVenta.cshtml"
WriteAttributeValue("", 1817, DateTime.Now.ToString("yyyy-MM-dd"), 1817, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""Selecciona una fecha"" id=""hasta"" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""card-footer text-center"">
                    <button type=""submit"" class=""btn btn-primary"" id=""btnCrear"">Crear reporte <i class=""fas fa-save""></i></button>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <div style=\"display:none\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "229fe9956803f47e42912a280da33859ee887e5710600", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
