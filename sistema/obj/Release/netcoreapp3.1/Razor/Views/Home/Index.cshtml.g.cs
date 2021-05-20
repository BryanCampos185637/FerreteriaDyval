#pragma checksum "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "021ffd5d0d2af26da764e75927f8b180bec1c825"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"021ffd5d0d2af26da764e75927f8b180bec1c825", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7079e06bb53f61544eaed8a06558250b1c9f6c4d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#nullable restore
#line 1 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
   
    ViewData["Title"] = "Home Page";
 

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"text-center\">\n    <h1>Ferreteria La Terminal&reg;</h1>\n");
#nullable restore
#line 7 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
     if (ViewBag.nombre != null || ViewBag.nombre != "")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h5>BIENVENIDO AL SISTEMA ");
#nullable restore
#line 9 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
                             Write(ViewBag.nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fas fa-smile\"></i></h5>\n");
#nullable restore
#line 10 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
       if (ViewBag.configuracion == null)
       {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""row"">
                <div class=""col-lg-2 col-sm-12""></div>
                <div class=""card col-lg-8 col-sm-12"">
                    <div class=""card-header bg-dark"">
                        <h5 class=""card-title text-light""><i class=""fas fa-cogs""></i> Configuración inicial del sistema.</h5>
                    </div>
                    <div class=""card-body"">
                        <div class=""row"">
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Inicio de las facturas:</label>
                                    <input type=""number"" min=""0"" id=""txtIF"" placeholder=""Escribe el inicio de las facturas"" class=""form-control"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Hasta:</label>
 ");
            WriteLiteral(@"                                   <input type=""number"" min=""0"" id=""txtFF"" placeholder=""Escribe el fin de las facturas"" class=""form-control"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Cuantos digitos debe tener:</label>
                                    <input type=""number"" min=""0"" id=""txtDF"" placeholder=""Digitos de la factura"" class=""form-control"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Inicio de las cotizaciones:</label>
                                    <input type=""number"" min=""0"" id=""txtIC"" placeholder=""Escribe el inicio de las cotizaciones"" class=""form-control"" />
                                </div>
           ");
            WriteLiteral(@"                 </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Hasta:</label>
                                    <input type=""number"" min=""0"" id=""txtFC"" placeholder=""Escribe el fin de las cotizaciones"" class=""form-control"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Cuantos digitos debe tener:</label>
                                    <input type=""number"" min=""0"" id=""txtDC"" placeholder=""Digitos de la cotizacion"" class=""form-control"" />
                                </div>
                            </div>
                            <!--Credito fiscal-->
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                       ");
            WriteLiteral(@"             <label>Inicio del credito fiscal:</label>
                                    <input type=""number"" min=""0"" id=""txtICF"" placeholder=""Escribe el inicio del credito fiscal"" class=""form-control r"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Hasta:</label>
                                    <input type=""number"" min=""0"" id=""txtFCF"" placeholder=""Escribe el fin del credito fiscal"" class=""form-control r"" />
                                </div>
                            </div>
                            <div class=""col-lg-4 col-sm-12"">
                                <div class=""form-group"">
                                    <label>Cuantos digitos debe tener:</label>
                                    <input type=""number"" min=""0"" id=""txtDCF"" placeholder=""Digitos del credito fiscal"" class=""form-control"" />
 ");
            WriteLiteral(@"                               </div>
                            </div>
                        </div>
                    </div>
                    <div class=""card-footer"">
                        <button type=""button"" class=""btn btn-primary"" onclick=""saveConfiguracion()"">Guardar Configuración</button>
                    </div>
                </div>
            <div class=""col-lg-2 col-sm-12""></div>
        </div>
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "021ffd5d0d2af26da764e75927f8b180bec1c8259982", async() => {
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
            WriteLiteral("\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "021ffd5d0d2af26da764e75927f8b180bec1c82511031", async() => {
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
            WriteLiteral("\n");
#nullable restore
#line 86 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
       }
       else if (ViewBag.unidad == 0)
       {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""row"">
            <div class=""col-lg-4 col-sm-12""></div>
            <div class=""card col-lg-4 col-sm-12"">
                        <div class=""card-header bg-dark"">
                            <h5 class=""card-title text-light"">Recomendación de inicio de sistema.</h5>
                        </div>
                        <div class=""card-body"">
                            <p>
                                El sistema esta recién instalado, te recomiendo que agregues unidades
                                de medida para poder ingresar productos.
                            </p>
                            <div class=""form-group"">
                                <label>Nombre de la unidad:</label>
                                <input type=""text"" class=""form-control"" placeholder=""Escriba el nombre de la unidad"" name=""nombreunidad"" id=""nombreunidad"" />
                            </div>
                            <button class=""btn btn-primary"" onclick=""sendData()"">Agregar unidad</bu");
            WriteLiteral("tton>\n                        </div>\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "021ffd5d0d2af26da764e75927f8b180bec1c82513473", async() => {
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
            WriteLiteral(@"
                        <script>
                            function sendData() {
                                var nombre = $('#nombreunidad').val();
                                if (nombre.trim() != '') {
                                    var frm = new FormData();
                                    frm.append('nombreunidad', nombre.toUpperCase());
                                    frm.append('bhabilitado', 'A');
                                    $.ajax({
                                        url: '/unidadmedida/saveUnidad',
                                        type: 'POST',
                                        contentType: false,
                                        processData: false,
                                        data: frm,
                                        success: function (respuesta) {
                                            if (respuesta > 0) {
                                                Swal.fire({
                                                    ti");
            WriteLiteral(@"tle: 'Exito',
                                                    text: 'Unidad guardada',
                                                    icon: 'success',
                                                    showCancelButton: false,
                                                    confirmButtonColor: '#3085d6',
                                                    cancelButtonColor: '#d33'
                                                }).then((result) => {
                                                    if (result.isConfirmed) {
                                                        location.href = '/home/index';
                                                    } else {
                                                        location.href = '/home/index';
                                                    }
                                                })
                                            } else {
                                                messeges('error', ""Error de sistema i");
            WriteLiteral(@"ntente mas tarde"");
                                            }
                                        }
                                    })
                                } else {
                                    messeges('warning', 'No puedes dejar vacio el campo.')
                                }
                            }
                        </script>
                    </div>
            <div class=""col-lg-4 col-sm-12""></div>
        </div>
");
#nullable restore
#line 149 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
       }
       else if (ViewBag.stock == 0)
       {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""row"">
        <div class=""col-lg-4 col-sm-12""></div>
        <div class=""card col-lg-4 col-sm-12"">
            <div class=""card-header bg-dark"">
                <h5 class=""card-title text-light"">Recomendación de inicio de sistema.</h5>
            </div>
            <div class=""card-body"">
                <p>El sistema esta recién instalado, te recomiendo que agregues stock para poder ingresar productos. </p>
            <div class=""form-group"">
                <label>Nombre del stock:</label>
                <input type=""text"" class=""form-control"" placeholder=""Escriba el nombre del stock"" name=""nombrestock"" id=""nombrestock"" />
            </div>
            <button class=""btn btn-primary"" onclick=""sendData()"">Agregar stock</button>
        </div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "021ffd5d0d2af26da764e75927f8b180bec1c82518180", async() => {
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
            WriteLiteral(@"
        <script>
            function sendData() {
                var nombre = $('#nombrestock').val();
                if (nombre.trim() != '') {
                    var frm = new FormData();
                    frm.append('nombrestock', nombre.toUpperCase());
                    frm.append('bhabilitado', 'A');
                    $.ajax({
                        url: '/stock/saveStock',
                        type: 'POST',
                        contentType: false,
                        processData: false,
                        data: frm,
                        success: function (respuesta) {
                            if (respuesta > 0) {
                                Swal.fire({
                                    title: 'Exito',
                                    text: 'Stock guardado',
                                    icon: 'success',
                                    showCancelButton: false,
                                    confirmButtonColor: '#3085d6',
                           ");
            WriteLiteral(@"         cancelButtonColor: '#d33'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        location.href = '/home/index';
                                    } else {
                                        location.href = '/home/index';
                                    }
                                })
                            } else {
                                messeges('error', ""Error de sistema intente mas tarde"");
                            }
                        }
                    })
                } else {
                    messeges('warning', 'No puedes dejar vacio el campo.')
                }
            }
        </script>
        </div>
        <div class=""col-lg-4 col-sm-12""></div>
    </div> 
");
#nullable restore
#line 209 "C:\Users\Bryan J. Campos\Documents\GitHub\FerreteriaDyval\sistema\Views\Home\Index.cshtml"
       }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</div>");
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
