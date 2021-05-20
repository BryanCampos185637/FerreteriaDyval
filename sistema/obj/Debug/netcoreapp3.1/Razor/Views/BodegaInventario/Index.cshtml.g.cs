#pragma checksum "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\BodegaInventario\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17c2cda1580ef0120b77877e8934d62c00e989f3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BodegaInventario_Index), @"mvc.1.0.view", @"/Views/BodegaInventario/Index.cshtml")]
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
#line 1 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\_ViewImports.cshtml"
using AdminFerreteria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17c2cda1580ef0120b77877e8934d62c00e989f3", @"/Views/BodegaInventario/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7079e06bb53f61544eaed8a06558250b1c9f6c4d", @"/Views/_ViewImports.cshtml")]
    public class Views_BodegaInventario_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/PeticionesAJX/bodegaInventario.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\BodegaInventario\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<h4>Bodega e inventario</h4>
<div class=""row"">
    <div class=""col-lg-6"">
        <div class=""card"">
            <div class=""card-body"">
                <button class=""btn btn-primary"" data-toggle=""modal"" data-target=""#modalGenerico"">Nueva bodega</button>
                <button class=""btn btn-primary"" onclick=""CallTable('bodega')"">Mostrar bodegas</button>
                <button class=""btn btn-primary"" onclick=""CallTable('inventario')"">Mostrar inventario</button>
                <button class=""btn btn-primary"" onclick=""listarProductos()"" id=""btnSalaDeVenta"">Inventario sala venta</button>
            </div>
        </div>
    </div>
    <div class=""col-lg-12 mt-3"" id=""tablaGenerica""></div>

    <!-- Modal BODEGA-->
    <div class=""modal fade"" id=""modalGenerico"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog modal-lg"" role=""document"">
            <div class=""modal-content"">
                <div class=""card-header bg-dark"">
            WriteLiteral("\n                    <h3 class=\"text-light\">Registro Bodega</h3>\r\n                </div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "17c2cda1580ef0120b77877e8934d62c00e989f35323", async() => {
                WriteLiteral(@"
                    <div class=""modal-body"">
                        <input type=""hidden"" class=""form-control data"" id=""iidbodega"" name=""iidbodega"" />
                        <div class=""form-group"">
                            <label>Nombre:</label>
                            <input autocomplete=""off"" type=""text"" class=""form-control data requerid"" placeholder=""Escribe el nombre de la bodega"" id=""nombrebodega"" name=""nombrebodega"" />
                        </div>
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrar"">Cerrar</button>
                        <button type=""button"" class=""btn-sm btn-primary"" id=""btnGuardar"">Guardar</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <!-- Modal MOVER PRODUCTO-->
    <div class=""modal fade"" id=""modalMoverProducto"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog modal-xl"" role=""document"">
            <div class=""modal-content"">
                <div class=""card-header bg-dark"">
                    <h3 class=""text-light"">Mover productos</h3>
                </div>
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "17c2cda1580ef0120b77877e8934d62c00e989f37923", async() => {
                WriteLiteral(@"
                    <div class=""modal-body"">
                        <input id=""txtiidinventario"" name=""iidinventario"" type=""hidden"" />
                        <input id=""txtiidbodega"" name=""iidbodega"" type=""hidden"" />
                        <input id=""txtiidproducto"" type=""hidden"" />
                        <div class=""row"">
                            <div class=""form-group col-lg-3"">
                                <label>Producto:</label>
                                <input type=""text"" class=""form-control"" id=""producto"" name=""nombreproducto"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Bodega actual:</label>
                                <input type=""text"" class=""form-control"" id=""bodega"" name=""nombrebodega"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Stock:</label>
             ");
                WriteLiteral(@"                   <input type=""text"" class=""form-control"" id=""stock"" name=""stock"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Existencia:</label>
                                <input type=""text"" class=""form-control"" id=""existencia"" name=""existencia"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Cantidad a mover:</label>
                                <input type=""number"" autocomplete=""off"" min=""1"" class=""form-control"" id=""cantidadMover"" value=""1"" name=""cantidad"" />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Mover al lugar:</label>
                                <select id=""cbxBodega"" class=""form-control"" onchange=""verificarSiEsBodega()""></select>
                            </div>
                ");
                WriteLiteral(@"            <div class=""form-group col-lg-3"" id=""divStock""></div>
                        </div>
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrar2"">Cerrar</button>
                        <button type=""button"" class=""btn-sm btn-primary"" onclick=""moverCantidadProducto()"">Guardar</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <!-- Modal editar cantidad PRODUCTO-->
    <div class=""modal fade"" id=""modalEditarCantidad"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog modal-xl"" role=""document"">
            <div class=""modal-content"">
                <div class=""card-header bg-dark"">
                    <h3 class=""text-light"">Editar exitencia producto</h3>
                </div>
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "17c2cda1580ef0120b77877e8934d62c00e989f312369", async() => {
                WriteLiteral(@"
                    <div class=""modal-body"">
                        <input id=""txtinventario"" name=""iidinventario"" type=""hidden"" />
                        <div class=""row"">
                            <div class=""form-group col-lg-3"">
                                <label>Producto:</label>
                                <input type=""text"" class=""form-control"" id=""txtproducto"" name=""nombreproducto"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Bodega actual:</label>
                                <input type=""text"" class=""form-control"" id=""txtbodega"" name=""nombrebodega"" readonly />
                            </div>
                            <div class=""form-group col-lg-3"">
                                <label>Stock:</label>
                                <input type=""text"" class=""form-control"" id=""txtstock"" name=""stock"" readonly />
                            </div>
               ");
                WriteLiteral(@"             <div class=""form-group col-lg-3"">
                                <label>Existencia:</label>
                                <input type=""number"" min=""0"" class=""form-control"" id=""txtcantidad"" name=""existencia"" />
                            </div>
                        </div>
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrar3"">Cerrar</button>
                        <button type=""button"" class=""btn-sm btn-primary"" onclick=""cambiarCantidadProducto()"">Guardar</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>

    <!-- Modal existencia-->
    <div class=""modal fade"" id=""modalExistencia"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog modal-lg"" role=""document"">
            <div class=""modal-content"">
                <div class=""card-header bg-dark"">
                    <h3 class=""text-light"">Modificar existencias en sala de venta</h3>
                </div>
                <div class=""modal-body"">
                    <input type=""hidden"" id=""Existiidproducto"" />
                    <div class=""row"">
                        <div class=""form-group col-sm-12 col-lg-3"">
                            <label>Código:</label>
                            <input type=""text"" class=""form-control"" id=""txtcodigoproducto"" readonly />
                        </div>
                        <div class=""form-group col-sm-12 col-lg-6"">
                            <label>Descripción:</label>
            ");
            WriteLiteral(@"                <input type=""text"" class=""form-control"" id=""txtdescripcion"" readonly />
                        </div>
                        <div class=""form-group col-sm-12 col-lg-3"">
                            <label>Existencia:</label>
                            <input type=""number"" class=""form-control"" id=""txtexistencia"" />
                        </div>
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrarExistencia"">Cerrar</button>
                        <button type=""button"" class=""btn-sm btn-primary"" onclick=""modificarExistencia()"">Guardar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "17c2cda1580ef0120b77877e8934d62c00e989f317340", async() => {
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
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591