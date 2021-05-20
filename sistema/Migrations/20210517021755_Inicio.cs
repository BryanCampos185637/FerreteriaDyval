using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminFerreteria.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BODEGA",
                columns: table => new
                {
                    IIDBODEGA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBREBODEGA = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BODEGA__ED00F28D0B40BCF5", x => x.IIDBODEGA);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    IIDCLIENTE = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRECOMPLETO = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    DIRECCION = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    REGISTRO = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    GIRO = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    NIT = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CLIENTE__AF86765147BA2C40", x => x.IIDCLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "CONFIGURACION",
                columns: table => new
                {
                    IIDCONFIGURACION = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INICIOFACTURA = table.Column<long>(nullable: false),
                    FINFACTURA = table.Column<long>(nullable: false),
                    NOACTUALFACTURA = table.Column<long>(nullable: false),
                    INICIOCOTIZACION = table.Column<long>(nullable: false),
                    FINCOTIZACION = table.Column<long>(nullable: false),
                    NOACTUALCOTIZACION = table.Column<long>(nullable: false),
                    NODIGITOSFACTURA = table.Column<int>(nullable: false),
                    NODIGITOSCOTIZACION = table.Column<int>(nullable: false),
                    INICIOCREDITOFISCAL = table.Column<long>(nullable: false),
                    FINCREDITOFISCAL = table.Column<long>(nullable: false),
                    NOACTUALCREDITOFISCAL = table.Column<long>(nullable: false),
                    NODIGITOSCREDITOFISCAL = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CONFIGUR__9203677289C9C782", x => x.IIDCONFIGURACION);
                });

            migrationBuilder.CreateTable(
                name: "EMPLEADO",
                columns: table => new
                {
                    IIDEMPLEADO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRECOMPLETO = table.Column<string>(unicode: false, nullable: false),
                    EDAD = table.Column<int>(nullable: false),
                    TELEFONO = table.Column<int>(nullable: true),
                    DUI = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    EMPLEADOTIENEUSUARIO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EMPLEADO__462140DA42AD8C35", x => x.IIDEMPLEADO);
                });

            migrationBuilder.CreateTable(
                name: "PAGINA",
                columns: table => new
                {
                    IIDPAGINA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MENSAJE = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ACCION = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CONTROLADOR = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    ICONO = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAGINA__8E759E4E5CDFE19B", x => x.IIDPAGINA);
                });

            migrationBuilder.CreateTable(
                name: "STOCK",
                columns: table => new
                {
                    IIDSTOCK = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRESTOCK = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: false),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STOCK__5EBABD9FE67EDAB3", x => x.IIDSTOCK);
                });

            migrationBuilder.CreateTable(
                name: "TIPOUSUARIO",
                columns: table => new
                {
                    IIDTIPOUSUARIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRETIPOUSUARIO = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DESCRIPCION = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TIPOUSUA__A05A9116419E2F34", x => x.IIDTIPOUSUARIO);
                });

            migrationBuilder.CreateTable(
                name: "UNIDADMEDIDA",
                columns: table => new
                {
                    IIDUNIDADMEDIDA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBREUNIDAD = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UNIDADME__D8851CA5DF24E311", x => x.IIDUNIDADMEDIDA);
                });

            migrationBuilder.CreateTable(
                name: "PAGINATIPOUSUARIO",
                columns: table => new
                {
                    IIDPAGINATIPOUSUARIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDPAGINA = table.Column<int>(nullable: false),
                    IIDTIPOUSUARIO = table.Column<int>(nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAGINATI__80FD03168D47A835", x => x.IIDPAGINATIPOUSUARIO);
                    table.ForeignKey(
                        name: "FK__PAGINATIP__IIDPA__4AB81AF0",
                        column: x => x.IIDPAGINA,
                        principalTable: "PAGINA",
                        principalColumn: "IIDPAGINA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PAGINATIP__IIDTI__4BAC3F29",
                        column: x => x.IIDTIPOUSUARIO,
                        principalTable: "TIPOUSUARIO",
                        principalColumn: "IIDTIPOUSUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    IIDUSUARIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDEMPLEADO = table.Column<int>(nullable: false),
                    IIDTIPOUSUARIO = table.Column<int>(nullable: false),
                    NOMBREUSUARIO = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    CONTRASEÑA = table.Column<string>(unicode: false, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USUARIO__26DBFF593A56E8F3", x => x.IIDUSUARIO);
                    table.ForeignKey(
                        name: "FK__USUARIO__IIDEMPL__44FF419A",
                        column: x => x.IIDEMPLEADO,
                        principalTable: "EMPLEADO",
                        principalColumn: "IIDEMPLEADO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__USUARIO__IIDTIPO__45F365D3",
                        column: x => x.IIDTIPOUSUARIO,
                        principalTable: "TIPOUSUARIO",
                        principalColumn: "IIDTIPOUSUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTO",
                columns: table => new
                {
                    IIDPRODUCTO = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDUNIDADMEDIDA = table.Column<int>(nullable: false),
                    CODIGOPRODUCTO = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    DESCRIPCION = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    EXISTENCIAS = table.Column<long>(nullable: true),
                    PRECIOCOMPRA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    PORCENTAJEGANANCIA = table.Column<int>(nullable: false),
                    GANANCIA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    PRECIOVENTA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    IIDSTOCK = table.Column<int>(nullable: false),
                    SUBUNIDAD = table.Column<int>(nullable: true),
                    SUBPRECIOUNITARIO = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBIVA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBPORCENTAJE = table.Column<int>(nullable: true),
                    SUBGANANCIA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBPRECIOVENTA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    EQUIVALENCIA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBEXISTENCIA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    RESTANTES = table.Column<decimal>(type: "decimal(16, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCTO__158EDF304C3F3F3B", x => x.IIDPRODUCTO);
                    table.ForeignKey(
                        name: "FK__PRODUCTO__IIDSTO__160F4887",
                        column: x => x.IIDSTOCK,
                        principalTable: "STOCK",
                        principalColumn: "IIDSTOCK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PRODUCTO__IIDUNI__398D8EEE",
                        column: x => x.IIDUNIDADMEDIDA,
                        principalTable: "UNIDADMEDIDA",
                        principalColumn: "IIDUNIDADMEDIDA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COTIZACION",
                columns: table => new
                {
                    IIDCOTIZACION = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: false),
                    FECHAVENCIMIENTO = table.Column<DateTime>(type: "date", nullable: false),
                    COTIZACIONFACTURADA = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    IIDUSUARIO = table.Column<int>(nullable: false),
                    TOTAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    NOMBRECLIENTE = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    COTIZACIONEMITIDA = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    NOCOTIZACION = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COTIZACI__018E120D0B0F7639", x => x.IIDCOTIZACION);
                    table.ForeignKey(
                        name: "FK__COTIZACIO__IIDUS__6E01572D",
                        column: x => x.IIDUSUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "IIDUSUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FACTURA",
                columns: table => new
                {
                    IIDFACTURA = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDUSUARIO = table.Column<int>(nullable: false),
                    TIPOCOMPRADOR = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    NOMBRECLIENTE = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    DIRECCION = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    REGISTRO = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    GIRO = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    NIT = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    TOTAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: false),
                    FACTURAEMITIDA = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
                    NOFACTURA = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    TOTALCOMISION = table.Column<decimal>(type: "decimal(16, 4)", nullable: false),
                    TOTALDESCUENTO = table.Column<decimal>(type: "decimal(16, 4)", nullable: false),
                    PORCENTAJEDESCUENTOGLOBAL = table.Column<int>(nullable: false),
                    DESCUENTOGLOBAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: false),
                    EFECTIVO = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    CAMBIO = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    ORIGINAL = table.Column<string>(unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FACTURA__C5D90AE94B80389E", x => x.IIDFACTURA);
                    table.ForeignKey(
                        name: "FK__FACTURA__IIDUSUA__4E88ABD4",
                        column: x => x.IIDUSUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "IIDUSUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTRADA",
                columns: table => new
                {
                    IIDENTRADA = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDPRODUCTO = table.Column<long>(nullable: false),
                    EXISTENCIASPRODUCTO = table.Column<long>(nullable: false),
                    FECHAEXPEDICIONCCF = table.Column<DateTime>(type: "datetime", nullable: true),
                    FECHAINICIOCREDITO = table.Column<DateTime>(type: "datetime", nullable: true),
                    FECHAVENCIMIENTO = table.Column<DateTime>(type: "datetime", nullable: true),
                    CONDICIONVENTA = table.Column<string>(unicode: false, nullable: true),
                    NUMEROCCF = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    CANTIDAD = table.Column<long>(nullable: false),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    PROVEEDOR = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    PRECIOCOMPRA = table.Column<decimal>(type: "decimal(16, 4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ENTRADA__1AE39997DB223568", x => x.IIDENTRADA);
                    table.ForeignKey(
                        name: "FK__ENTRADA__IIDPROD__3E52440B",
                        column: x => x.IIDPRODUCTO,
                        principalTable: "PRODUCTO",
                        principalColumn: "IIDPRODUCTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INVENTARIO",
                columns: table => new
                {
                    IIDINVENTARIO = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDBODEGA = table.Column<int>(nullable: false),
                    IIDPRODUCTO = table.Column<long>(nullable: false),
                    CANTIDAD = table.Column<long>(nullable: false),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    IIDSTOCK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INVENTAR__C5FD6D5CA06FC5DB", x => x.IIDINVENTARIO);
                    table.ForeignKey(
                        name: "FK__INVENTARI__IIDBO__2B0A656D",
                        column: x => x.IIDBODEGA,
                        principalTable: "BODEGA",
                        principalColumn: "IIDBODEGA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__INVENTARI__IIDPR__2BFE89A6",
                        column: x => x.IIDPRODUCTO,
                        principalTable: "PRODUCTO",
                        principalColumn: "IIDPRODUCTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__INVENTARI__IIDST__3A4CA8FD",
                        column: x => x.IIDSTOCK,
                        principalTable: "STOCK",
                        principalColumn: "IIDSTOCK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DETALLECOTIZACION",
                columns: table => new
                {
                    IIDDETALLECOTIZACIO = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDCOTIZACION = table.Column<long>(nullable: false),
                    IIDPRODUCTO = table.Column<long>(nullable: false),
                    PRECIOACTUAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    CANTIDAD = table.Column<long>(nullable: false),
                    PORCENTAJEDESCUENTO = table.Column<int>(nullable: false),
                    DESCUENTO = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    PORCENTAJECOMISION = table.Column<int>(nullable: false),
                    COMISION = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBTOTAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    FECHAVENCIMIENTO = table.Column<DateTime>(type: "date", nullable: false),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    ESSUBPRODUCTO = table.Column<string>(unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLEC__B66318029EB1D97E", x => x.IIDDETALLECOTIZACIO);
                    table.ForeignKey(
                        name: "FK__DETALLECO__IIDCO__5EBF139D",
                        column: x => x.IIDCOTIZACION,
                        principalTable: "COTIZACION",
                        principalColumn: "IIDCOTIZACION",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__DETALLECO__IIDPR__5FB337D6",
                        column: x => x.IIDPRODUCTO,
                        principalTable: "PRODUCTO",
                        principalColumn: "IIDPRODUCTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DETALLEPEDIDO",
                columns: table => new
                {
                    IIDDETALLEPEDIDO = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDFACTURA = table.Column<long>(nullable: false),
                    IIDPRODUCTO = table.Column<long>(nullable: false),
                    PRECIOACTUAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    CANTIDAD = table.Column<long>(nullable: false),
                    PORCENTAJEDESCUENTO = table.Column<int>(nullable: false),
                    DESCUENTO = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    PORCENTAJECOMISION = table.Column<int>(nullable: false),
                    COMISION = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    SUBTOTAL = table.Column<decimal>(type: "decimal(16, 4)", nullable: true),
                    FECHACREACION = table.Column<DateTime>(type: "date", nullable: true),
                    BHABILITADO = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    ESSUBPRODUCTO = table.Column<string>(unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DETALLEP__6189D32A00E3D89C", x => x.IIDDETALLEPEDIDO);
                    table.ForeignKey(
                        name: "FK__DETALLEPE__IIDFA__5165187F",
                        column: x => x.IIDFACTURA,
                        principalTable: "FACTURA",
                        principalColumn: "IIDFACTURA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__DETALLEPE__IIDPR__52593CB8",
                        column: x => x.IIDPRODUCTO,
                        principalTable: "PRODUCTO",
                        principalColumn: "IIDPRODUCTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BITACORAENTRADA",
                columns: table => new
                {
                    IIDBOTACORABODEGA = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IIDENTRADA = table.Column<long>(nullable: false),
                    IIDPRODUCTO = table.Column<long>(nullable: false),
                    IIDBODEGA = table.Column<int>(nullable: false),
                    IIDSTOCK = table.Column<int>(nullable: false),
                    CANTIDAD = table.Column<long>(nullable: false),
                    SUBCANTIDAD = table.Column<decimal>(type: "decimal(18, 0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BITACORA__FC598AE78B4F2BC4", x => x.IIDBOTACORABODEGA);
                    table.ForeignKey(
                        name: "FK__BITACORAE__IIDEN__4F47C5E3",
                        column: x => x.IIDENTRADA,
                        principalTable: "ENTRADA",
                        principalColumn: "IIDENTRADA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BITACORAENTRADA_IIDENTRADA",
                table: "BITACORAENTRADA",
                column: "IIDENTRADA");

            migrationBuilder.CreateIndex(
                name: "IX_COTIZACION_IIDUSUARIO",
                table: "COTIZACION",
                column: "IIDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLECOTIZACION_IIDCOTIZACION",
                table: "DETALLECOTIZACION",
                column: "IIDCOTIZACION");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLECOTIZACION_IIDPRODUCTO",
                table: "DETALLECOTIZACION",
                column: "IIDPRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLEPEDIDO_IIDFACTURA",
                table: "DETALLEPEDIDO",
                column: "IIDFACTURA");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLEPEDIDO_IIDPRODUCTO",
                table: "DETALLEPEDIDO",
                column: "IIDPRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_ENTRADA_IIDPRODUCTO",
                table: "ENTRADA",
                column: "IIDPRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_FACTURA_IIDUSUARIO",
                table: "FACTURA",
                column: "IIDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTARIO_IIDBODEGA",
                table: "INVENTARIO",
                column: "IIDBODEGA");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTARIO_IIDPRODUCTO",
                table: "INVENTARIO",
                column: "IIDPRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_INVENTARIO_IIDSTOCK",
                table: "INVENTARIO",
                column: "IIDSTOCK");

            migrationBuilder.CreateIndex(
                name: "IX_PAGINATIPOUSUARIO_IIDPAGINA",
                table: "PAGINATIPOUSUARIO",
                column: "IIDPAGINA");

            migrationBuilder.CreateIndex(
                name: "IX_PAGINATIPOUSUARIO_IIDTIPOUSUARIO",
                table: "PAGINATIPOUSUARIO",
                column: "IIDTIPOUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_IIDSTOCK",
                table: "PRODUCTO",
                column: "IIDSTOCK");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTO_IIDUNIDADMEDIDA",
                table: "PRODUCTO",
                column: "IIDUNIDADMEDIDA");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_IIDEMPLEADO",
                table: "USUARIO",
                column: "IIDEMPLEADO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_IIDTIPOUSUARIO",
                table: "USUARIO",
                column: "IIDTIPOUSUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BITACORAENTRADA");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "CONFIGURACION");

            migrationBuilder.DropTable(
                name: "DETALLECOTIZACION");

            migrationBuilder.DropTable(
                name: "DETALLEPEDIDO");

            migrationBuilder.DropTable(
                name: "INVENTARIO");

            migrationBuilder.DropTable(
                name: "PAGINATIPOUSUARIO");

            migrationBuilder.DropTable(
                name: "ENTRADA");

            migrationBuilder.DropTable(
                name: "COTIZACION");

            migrationBuilder.DropTable(
                name: "FACTURA");

            migrationBuilder.DropTable(
                name: "BODEGA");

            migrationBuilder.DropTable(
                name: "PAGINA");

            migrationBuilder.DropTable(
                name: "PRODUCTO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "STOCK");

            migrationBuilder.DropTable(
                name: "UNIDADMEDIDA");

            migrationBuilder.DropTable(
                name: "EMPLEADO");

            migrationBuilder.DropTable(
                name: "TIPOUSUARIO");
        }
    }
}
