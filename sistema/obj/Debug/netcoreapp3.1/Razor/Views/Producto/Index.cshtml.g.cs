#pragma checksum "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Producto\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f99c867d8068b13947edc01a76014f45c9f76076"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Producto_Index), @"mvc.1.0.view", @"/Views/Producto/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f99c867d8068b13947edc01a76014f45c9f76076", @"/Views/Producto/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7079e06bb53f61544eaed8a06558250b1c9f6c4d", @"/Views/_ViewImports.cshtml")]
    public class Views_Producto_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/generic.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/PeticionesAJX/producto.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Producto\Index.cshtml"
   ViewData["Title"] = "Producto"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h4>Productos</h4>

<!--botonera-->
<div class=""row mb-2"">
    <div class=""col-lg-4"">
        <input type=""button"" value=""Nuevo"" class=""btn-sm btn-primary"" data-toggle=""modal"" data-target=""#modalProducto"" onclick=""deleteDataOfTheForm()"" />
    </div>
</div>
<div class=""row mb-2"">
    
    <div class=""col-lg-6"">
        <label>Buscar por codigo</label>
        <div class=""input-group"">
            <input type=""search"" class=""form-control"" id=""filtrarPorCodigo"" placeholder=""Filtrar por codigo"" autocomplete=""off"" />
            <div class=""input-group-append"">
                <button type=""button"" class=""btn btn-primary"" onclick=""callTable(1)"">
                    <i class=""fas fa-search""></i>
                </button>
            </div>
        </div>
    </div>
    <div class=""col-lg-6"">
        <label>Buscar por descripción</label>
        <div class=""input-group"">
            <input type=""search"" class=""form-control"" id=""filtrarPorDescripcion"" placeholder=""Filtrar por descripción"" autocomplete=""off"" onkeyup");
            WriteLiteral(@"=""callTable(2)"" />
            <div class=""input-group-append"">
                <button type=""button"" class=""btn btn-primary"" onclick=""callTable(2)"">
                    <i class=""fas fa-search""></i>
                </button>
            </div>
        </div>
    </div>
</div>
<!--TABLA-->
<div class=""row"">
    <div class=""col-lg-12"" id=""tableData""><span>Cargando...</span></div>
</div>
<!-- Modal producto-->
<div class=""modal fade"" id=""modalProducto"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-xl"" role=""document"">
        <div class=""modal-content"">
            <div class=""card-header bg-dark"">
                <h3 class=""text-light"">Registro producto</h3>
            </div>
            <div class=""modal-body"">
                <input type=""hidden"" name=""iidproducto"" id=""iidproducto"" class=""data form-control"" />
                <div class=""row"">
                    <div class=""form-group col-sm-12 col-lg-2"">
                        <labe");
            WriteLiteral(@"l>Código:</label>
                        <input type=""text"" class=""form-control requerid data"" autocomplete=""off"" placeholder=""Codigo del producto"" name=""codigoproducto"" id=""codigoproducto"" />
                    </div>
                    <div class=""form-group col-sm-12 col-lg-2"">
                        <label>Unidad de medida:</label>
                        <select id=""iidunidadmedida"" name=""iidunidadmedida"" autocomplete=""off"" class=""form-control requerid data""></select>
                    </div>
                    <div class=""form-group col-sm-12 col-lg-2"">
                        <label>Ubicación del producto:</label>
                        <select id=""iidstock"" name=""iidstock"" class=""form-control requerid data""></select>
                    </div>
                    <div class=""form-group col-sm-12 col-lg-3"">
                        <label>Descripción:</label>
                        <input type=""text"" class=""form-control requerid data"" autocomplete=""off"" placeholder=""Descripcion del producto"" na");
            WriteLiteral(@"me=""descripcion"" id=""descripcion"" />
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label>Precio compra sin IVA:</label>
                            <input type=""text"" class=""form-control requerid data"" autocomplete=""off"" name=""preciocompra"" id=""preciocompra"" onchange=""calculatePrice()"" />
                        </div>
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label>Precio compra con IVA:</label>
                            <input type=""text"" class=""form-control"" id=""ivaProducto"" readonly />
                        </div>
                    </div>
                    <div class=""col-lg-1 col-sm-6"">
                        <div class=""form-group"">
                            <label>%:</label>
                            <input type=""number"" value=""1"" class=""form-control data"" autocomplet");
            WriteLiteral(@"e=""off"" name=""Porcentajeganancia"" id=""gananciaObtenida"" onchange=""calculatePrice()"" />
                        </div>
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label>Utilidad obtenida:</label>
                            <input type=""text"" readonly class=""form-control data"" name=""ganancia"" id=""ganancia"" />
                        </div>
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label>Precio + utilidad:</label>
                            <input type=""text"" readonly class=""form-control"" id=""Precioutilidad"" />
                        </div>
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label>Iva 13%:</label>
                            <input type=""text"" readonly class=""form-c");
            WriteLiteral(@"ontrol data"" name=""iva"" id=""iva"" />
                        </div>
                    </div>
                    <div class=""col-lg-2 col-sm-6"">
                        <div class=""form-group"">
                            <label><strong>Precio venta con IVA:</strong></label>
                            <input type=""text"" readonly class=""form-control data"" name=""precioventa"" id=""precioventa"" />
                        </div>
                    </div>
                </div>
                <!--Equivalencias-->
                <div id=""acordionAjustes"">
                    <div class=""card"">
                        <button class=""btn btn-primary"" data-toggle=""collapse"" data-target=""#acordionFactura"" aria-expanded=""true"" aria-controls=""acordionFactura"">
                            <strong class=""text-light"">SUB PRODUCTO</strong>
                        </button>
                        <div id=""acordionFactura"" class=""collapse"" aria-labelledby=""headinFactura"" data-parent=""#acordionAjustes"">
                    ");
            WriteLiteral(@"        <div class=""card-body"">
                                <div class=""row"">
                                    <div class=""row"">
                                        <div class=""form-group col-sm-6 col-lg-6"">
                                            <label>Unidad de medida:</label>
                                            <select id=""subiidunidadmedida"" name=""subunidad"" class=""form-control sub""></select>
                                        </div>
                                        <div class=""form-group col-lg-6 col-sm-6"">
                                            <label>Equivalencia de unidad principal:</label>
                                            <input type=""number"" class=""form-control sub"" autocomplete=""off"" name=""equivalencia"" id=""equivalencia"" onchange=""calculateSubPrice()"" />
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
        ");
            WriteLiteral(@"                                        <label>Sub precio unitario:</label>
                                                <input type=""text"" class=""form-control sub"" readonly name=""subpreciounitario"" id=""subpreciocompra"" onchange=""calculateSubPrice()"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
                                                <label>%:</label>
                                                <input type=""number"" value=""1"" autocomplete=""off"" class=""form-control sub"" name=""subporcentaje"" id=""subgananciaObtenida"" onchange=""calculateSubPrice()"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
                    ");
            WriteLiteral(@"                            <label>Utilidad obtenida:</label>
                                                <input type=""number"" readonly class=""form-control sub"" name=""subganancia"" id=""subganancia"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
                                                <label>Precio + utilidad:</label>
                                                <input type=""text"" readonly class=""form-control"" id=""subPrecioutilidad"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
                                                <label>Iva 13%:</label>
                                                <input type=""numb");
            WriteLiteral(@"er"" readonly class=""form-control sub"" name=""subiva"" id=""subiva"" />
                                            </div>
                                        </div>
                                        <div class=""col-lg-2 col-sm-6"">
                                            <div class=""form-group"">
                                                <label><strong>Precio venta:</strong></label>
                                                <input type=""number"" readonly class=""form-control sub"" name=""subprecioventa"" id=""subprecioventa"" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn-sm btn-secondary"" data-dismiss=""modal"" id=""btnCerrar"">Cerrar</button>
 ");
            WriteLiteral("               <button type=\"button\" class=\"btn-sm btn-primary\" onclick=\"sendData()\">Guardar</button>\n            </div>\n        </div>\n    </div>\n</div>\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f99c867d8068b13947edc01a76014f45c9f7607615225", async() => {
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
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f99c867d8068b13947edc01a76014f45c9f7607616263", async() => {
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
