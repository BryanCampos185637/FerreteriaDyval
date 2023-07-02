#pragma checksum "C:\Users\R5 5600G\Desktop\Proyectos\FerreteriaDyval\sistema\Views\Configuracion\_FormularioRegistro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c4721d2fe6cd4836e3de604e339449772720aae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Configuracion__FormularioRegistro), @"mvc.1.0.view", @"/Views/Configuracion/_FormularioRegistro.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c4721d2fe6cd4836e3de604e339449772720aae", @"/Views/Configuracion/_FormularioRegistro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8cb12e4f445e7635817efa861e2d4a381810a7c2", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Configuracion__FormularioRegistro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div id=""acordionAjustes"" class=""col-12"">
    <!--factura-->
    <div class=""card"">
        <div class=""card-header bg-dark"" id=""headinFactura"">
            <h5 class=""mb-0"">
                <button class=""btn btn-link"" data-toggle=""collapse"" data-target=""#acordionFactura"" aria-expanded=""true"" aria-controls=""acordionFactura"">
                    <strong class=""text-light"">Ajustar la factura</strong>
                </button>
            </h5>
        </div>
        <div id=""acordionFactura"" class=""collapse"" aria-labelledby=""headinFactura"" data-parent=""#acordionAjustes"">
            <div class=""card-body"">
                <div class=""row"">
                    <!--factura-->
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>Inicio de las facturas:</label>
                            <input type=""number"" min=""0"" id=""txtIF"" placeholder=""Escribe el inicio de las facturas"" class=""form-control r"" />
         ");
            WriteLiteral(@"               </div>
                    </div>
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>Hasta:</label>
                            <input type=""number"" min=""0"" id=""txtFF"" placeholder=""Escribe el fin de las facturas"" class=""form-control r"" autocomplete=""off"" />
                        </div>
                    </div>
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>No. Actual factura:</label>
                            <input type=""number"" disabled id=""txtAF"" placeholder=""Escribe el fin de las facturas"" class=""form-control"" autocomplete=""off"" />
                        </div>
                    </div>
                    <input type=""hidden"" id=""txtDF"" class=""form-control"" />
                </div>
            </div>
        </div>
    </div>
    <!--Cotizacion-->
    <div class=""card"">
        <div clas");
            WriteLiteral(@"s=""card-header bg-dark"" id=""headinCotizacion"">
            <h5 class=""mb-0"">
                <button class=""btn btn-link"" data-toggle=""collapse"" data-target=""#acordionCotizacion"" aria-expanded=""true"" aria-controls=""acordionCotizacion"">
                    <strong class=""text-light"">Ajustar la cotización</strong>
                </button>
            </h5>
        </div>
        <div id=""acordionCotizacion"" class=""collapse"" aria-labelledby=""headinCotizacion"" data-parent=""#acordionAjustes"">
            <div class=""card-body"">
                <div class=""row"">
                    <!--Cotizacion-->
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>Inicio de las cotizaciones:</label>
                            <input type=""number"" min=""0"" id=""txtIC"" placeholder=""Escribe el inicio de las cotizaciones"" class=""form-control r"" autocomplete=""off"" />
                        </div>
                    </div>
         ");
            WriteLiteral(@"           <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>Hasta:</label>
                            <input type=""number"" min=""0"" id=""txtFC"" placeholder=""Escribe el fin de las cotizaciones"" class=""form-control r"" autocomplete=""off"" />
                        </div>
                    </div>
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>No. Actual cotización:</label>
                            <input type=""number"" disabled id=""txtAC"" placeholder=""Escribe el fin de las facturas"" class=""form-control"" autocomplete=""off"" />
                        </div>
                    </div>
                    <input id=""txtDC"" class=""form-control"" type=""hidden"" />
                </div>
            </div>
        </div>
    </div>
    <!--Credito fiscal-->
    <div class=""card"">
        <div class=""card-header bg-dark"" id=""headinCredito"">
    ");
            WriteLiteral(@"        <h5 class=""mb-0"">
                <button class=""btn btn-link"" data-toggle=""collapse"" data-target=""#acordionCredito"" aria-expanded=""true"" aria-controls=""acordionCredito"">
                    <strong class=""text-light"">Ajustar crédito fiscal</strong>
                </button>
            </h5>
        </div>
        <div id=""acordionCredito"" class=""collapse"" aria-labelledby=""headinCredito"" data-parent=""#acordionAjustes"">
            <div class=""card-body"">
                <div class=""row"">
                    <!--Credito fiscal-->
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>Inicio del crédito fiscal:</label>
                            <input type=""number"" min=""0"" id=""txtICF"" placeholder=""Escribe el inicio del credito fiscal"" class=""form-control r"" autocomplete=""off"" />
                        </div>
                    </div>
                    <div class=""col-lg-4 col-sm-12"">
               ");
            WriteLiteral(@"         <div class=""form-group"">
                            <label>Hasta:</label>
                            <input type=""number"" min=""0"" id=""txtFCF"" placeholder=""Escribe el fin del credito fiscal"" class=""form-control r"" autocomplete=""off"" />
                        </div>
                    </div>
                    <div class=""col-lg-4 col-sm-12"">
                        <div class=""form-group"">
                            <label>No. Actual crédito fiscal:</label>
                            <input type=""number"" disabled id=""txtACF"" placeholder=""Escribe el fin de las facturas"" class=""form-control"" autocomplete=""off"" />
                        </div>
                    </div>
                    <input type=""hidden"" id=""txtDCF"" class=""form-control"" />
                </div>
            </div>
        </div>
    </div>
</div>");
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
