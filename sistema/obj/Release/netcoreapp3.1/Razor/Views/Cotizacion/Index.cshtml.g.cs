#pragma checksum "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Cotizacion\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "191de27be6936188b7e5eb490142477b5517710b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cotizacion_Index), @"mvc.1.0.view", @"/Views/Cotizacion/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"191de27be6936188b7e5eb490142477b5517710b", @"/Views/Cotizacion/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7079e06bb53f61544eaed8a06558250b1c9f6c4d", @"/Views/_ViewImports.cshtml")]
    public class Views_Cotizacion_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/PeticionesAJX/cotizacion.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Cotizacion\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!-- Modal Productos-->
<div class=""modal fade"" id=""modalProducto"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-xl"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-body"">
                <h3>Selecciona un producto</h3><hr />
                <div class=""row mb-2"">
                    <div class=""col-lg-6"">
                        <label>Buscar por codigo</label>
                        <div class=""input-group"">
                            <input type=""search"" class=""form-control"" id=""filtrarPorCodigo"" placeholder=""Filtrar por codigo"" autocomplete=""off"" />
                            <div class=""input-group-append"">
                                <button type=""button"" class=""btn btn-primary"" onclick=""abrirModalProducto(1)"">
                                    <i class=""fas fa-search""></i>
                                </button>
                            </div>
                        </div>
  ");
            WriteLiteral(@"                  </div>
                    <div class=""col-lg-6"">
                        <label>Buscar por descripción</label>
                        <div class=""input-group"">
                            <input type=""search"" class=""form-control"" id=""filtrarPorDescripcion"" placeholder=""Filtrar por descripción"" autocomplete=""off"" onkeyup=""abrirModalProducto(2)""/>
                            <div class=""input-group-append"">
                                <button type=""button"" class=""btn btn-primary"" onclick=""abrirModalProducto(2)"">
                                    <i class=""fas fa-search""></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id=""tablaProducto"">
                    <span>Cargando productos...</span>
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn-sm btn-secondary"" data-dismi");
            WriteLiteral(@"ss=""modal"" id=""btnCerrarProducto"">Cerrar</button>
            </div>
        </div>
    </div>
</div>


<div class=""row mt-3"">
    <!--seleccionar producto-->
    <div class=""col-lg-12 col-sm-12"">
       <div class=""card"">
           <div class=""card-body"">
               <h5>Crear cotizacíon</h5>
               <table class=""table table-responsive-sm"">
                   <tbody>
                       <tr>
                           <td width=""30%"">
                               <label>Producto:</label>
                               <div class=""input-group"">
                                   <input type=""hidden"" id=""subunidad"" value=""0"" class=""form-control"" />
                                   <input type=""hidden"" id=""iidproducto"" class=""form-control"" />
                                   <input type=""text"" readonly class=""form-control requerid"" id=""txtProducto"" placeholder=""Busca un producto"" />
                                   <div class=""input-group-append"">
                  ");
            WriteLiteral(@"                     <button type=""button"" class=""btn btn-primary"" onclick=""abrirModalProducto()"" data-toggle=""modal"" data-target=""#modalProducto"">
                                           <i class=""fas fa-search""></i>
                                       </button>
                                   </div>
                               </div>
                           </td>
                           <td>
                               <label>Existencia:</label>
                               <input type=""text"" disabled class=""form-control"" placeholder=""existencias"" id=""txtExistencias"" />
                           </td>
                           <td>
                               <label>Precio:</label>
                               <input type=""text"" readonly class=""form-control"" placeholder=""precio unitario"" id=""txtPrecioUnitario"" />
                           </td>
                           <td>
                               <label>Cantidad:</label>
                              ");
            WriteLiteral(@" <input type=""number"" class=""form-control"" placeholder=""cantidad"" name=""cantidad"" id=""txtCantidad"" min=""0"" value=""1"" onchange=""calculateDiscount()"" />
                           </td>
                           <td>
                               <label>Comisión[%]:</label>
                               <input type=""number"" class=""form-control"" placeholder=""comision"" name=""comision"" id=""txtComision"" min=""0"" onchange=""calculateDiscount()"" value=""0"" />
                           </td>
                           <td>
                               <label>Descuento[%]:</label>
                               <input type=""number"" class=""form-control"" placeholder=""descuento"" name=""descuento"" id=""txtDescuento"" min=""0"" onchange=""calculateDiscount()"" value=""0"" />
                           </td>
                           <td>
                               <label>T.comisión:</label>
                               <input type=""text"" readonly class=""form-control"" placeholder=""t.comision"" id=""txtTcomision"" /");
            WriteLiteral(@">
                           </td>
                           <td>
                               <label>T.descuento:</label>
                               <input type=""text"" readonly class=""form-control"" placeholder=""t.descuento"" id=""txtTdescuento"" />
                           </td>
                           <td>
                               <label>Total:</label>
                               <input type=""text"" readonly class=""form-control"" placeholder=""total"" id=""txtTotal"" />
                           </td>
                           <td>
                               <label>ADD</label>
                               <button type=""button"" class=""btn btn-primary"" onclick=""addProductToList()""><i class=""fas fa-plus""></i></button>
                           </td>
                       </tr>
                   </tbody>
               </table>
           </div>
       </div>
    </div>
</div>
<!--TABLA-->
<h5 class=""mt-2"">Lista productos cotizados:</h5>
<hr />
<div class=""row"">
 ");
            WriteLiteral(@"   <div class=""col-lg-9 col-sm-12"" id=""tableData""><span>Cargando...</span></div>
    <div class=""col-lg-3 col-sm-12"" id=""detailsSale"">
        <div class=""card"">
            <div class=""card-header"">
                <strong>Detalle cotización</strong>
            </div>
            <div class=""card-body"">
                <table class=""table"">
                    <tr>
                        <td>Comisión:</td>
                        <th id=""totalComision"">$0.00</th>
                    </tr>
                    <tr>
                        <td>Descuento:</td>
                        <th id=""totalDescuento"">$0.00</th>
                    </tr>
                    <tr>
                        <td>Total comisión:</td>
                        <th id=""totalComisionMenosDescuento"">$0.00</th>
                    </tr>
                    <tr>
                        <td>Total:</td>
                        <th id=""totalVenta"">$0.00</th>
                    </tr>
                </table>
     ");
            WriteLiteral(@"       </div>
            <div class=""card-footer"">
                <div class=""text-center"">
                    <button class=""btn-sm btn-danger"" onclick=""CancelCotizacion()"" id=""btnCancelarCotizacion""><i class=""far fa-window-close""></i> Cancelar</button>
                    
                    <button class=""btn-sm btn-primary"" data-toggle=""modal"" data-target=""#modalCotizacion"" id=""btnImprimir"">
                        <i class=""fas fa-file-invoice-dollar""></i> Crear cotizacion
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Modal -->
<div class=""modal fade"" id=""modalCotizacion"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header bg-dark"">
                <h4 class=""text-light"">Crear cotización</h4>
            </div>
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "191de27be6936188b7e5eb490142477b5517710b13748", async() => {
                WriteLiteral(@"
                <div class=""modal-body"">
                    <div class=""row"">
                        <div class=""col-lg-12 col-sm-12"">
                            <div class=""form-group"">
                                <label>Nombre del cliente:</label>
                                <input type=""text"" class=""form-control requerid"" placeholder=""Escribe el nombre del cliente"" id=""nombre"" name=""nombre"" autocomplete=""off""/>
                            </div>
                        </div>
                        <div class=""col-lg-12 col-sm-12"">
                            <div class=""form-group"">
                                <label>Tipo de documento:</label>
                                <select class=""form-control"" id=""tipodocumento"" name=""tipodocumento"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "191de27be6936188b7e5eb490142477b5517710b14858", async() => {
                    WriteLiteral("--Seleccione una opcion--");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "191de27be6936188b7e5eb490142477b5517710b16137", async() => {
                    WriteLiteral("Cotización");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "191de27be6936188b7e5eb490142477b5517710b17401", async() => {
                    WriteLiteral("Factura provisional");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </select>
                            </div>
                        </div>
                    </div>    
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrar"">Cerrar</button>
                    <button type=""button"" class=""btn-sm btn-primary"" id=""btnImprimir"" onclick=""saveCotizacion()"">Guardar</button>
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
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "191de27be6936188b7e5eb490142477b5517710b20176", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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