using System;
using AdminFerreteria.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminFerreteria.Models
{
    public partial class BDFERRETERIAContext : DbContext
    {
        public BDFERRETERIAContext()
        {
        }

        public BDFERRETERIAContext(DbContextOptions<BDFERRETERIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitacoraentrada> Bitacoraentrada { get; set; }
        public virtual DbSet<Bitacorasistema> Bitacorasistema { get; set; }
        public virtual DbSet<Bodega> Bodega { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<Cotizacion> Cotizacion { get; set; }
        public virtual DbSet<Detallecotizacion> Detallecotizacion { get; set; }
        public virtual DbSet<Detallepedido> Detallepedido { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Entrada> Entrada { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<Paginatipousuario> Paginatipousuario { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Tipousuario> Tipousuario { get; set; }
        public virtual DbSet<Unidadmedida> Unidadmedida { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ConexionSQL conexionSQL = new ConexionSQL();
                optionsBuilder.UseSqlServer(conexionSQL.local);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacoraentrada>(entity =>
            {
                entity.HasKey(e => e.Iidbotacorabodega)
                    .HasName("PK__BITACORA__FC598AE78B4F2BC4");

                entity.ToTable("BITACORAENTRADA");

                entity.HasIndex(e => e.Iidentrada);

                entity.Property(e => e.Iidbotacorabodega).HasColumnName("IIDBOTACORABODEGA");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Iidbodega).HasColumnName("IIDBODEGA");

                entity.Property(e => e.Iidentrada).HasColumnName("IIDENTRADA");

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Iidstock).HasColumnName("IIDSTOCK");

                entity.Property(e => e.Subcantidad)
                    .HasColumnName("SUBCANTIDAD")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IidentradaNavigation)
                    .WithMany(p => p.Bitacoraentrada)
                    .HasForeignKey(d => d.Iidentrada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BITACORAE__IIDEN__4F47C5E3");
            });

            modelBuilder.Entity<Bitacorasistema>(entity =>
            {
                entity.HasKey(e => e.Iidbitacorasistema)
                    .HasName("PK__BITACORA__CE5281B56D4E1264");

                entity.ToTable("BITACORASISTEMA");

                entity.Property(e => e.Iidbitacorasistema).HasColumnName("IIDBITACORASISTEMA");

                entity.Property(e => e.Descripcionbitacora)
                    .HasColumnName("DESCRIPCIONBITACORA")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fechaactividad)
                    .HasColumnName("FECHAACTIVIDAD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iidusuario).HasColumnName("IIDUSUARIO");

                entity.HasOne(d => d.IidusuarioNavigation)
                    .WithMany(p => p.Bitacorasistema)
                    .HasForeignKey(d => d.Iidusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BITACORAS__IIDUS__7E37BEF6");
            });

            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.Iidbodega)
                    .HasName("PK__BODEGA__ED00F28D0B40BCF5");

                entity.ToTable("BODEGA");

                entity.Property(e => e.Iidbodega).HasColumnName("IIDBODEGA");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Nombrebodega)
                    .IsRequired()
                    .HasColumnName("NOMBREBODEGA")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Iidcliente)
                    .HasName("PK__CLIENTE__AF86765147BA2C40");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Iidcliente).HasColumnName("IIDCLIENTE");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Giro)
                    .IsRequired()
                    .HasColumnName("GIRO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nit)
                    .IsRequired()
                    .HasColumnName("NIT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombrecompleto)
                    .IsRequired()
                    .HasColumnName("NOMBRECOMPLETO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Registro)
                    .IsRequired()
                    .HasColumnName("REGISTRO")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasKey(e => e.Iidconfiguracion)
                    .HasName("PK__CONFIGUR__9203677289C9C782");

                entity.ToTable("CONFIGURACION");

                entity.Property(e => e.Iidconfiguracion).HasColumnName("IIDCONFIGURACION");

                entity.Property(e => e.Fincotizacion).HasColumnName("FINCOTIZACION");

                entity.Property(e => e.Fincreditofiscal).HasColumnName("FINCREDITOFISCAL");

                entity.Property(e => e.Finfactura).HasColumnName("FINFACTURA");

                entity.Property(e => e.Iniciocotizacion).HasColumnName("INICIOCOTIZACION");

                entity.Property(e => e.Iniciocreditofiscal).HasColumnName("INICIOCREDITOFISCAL");

                entity.Property(e => e.Iniciofactura).HasColumnName("INICIOFACTURA");

                entity.Property(e => e.Noactualcotizacion).HasColumnName("NOACTUALCOTIZACION");

                entity.Property(e => e.Noactualcreditofiscal).HasColumnName("NOACTUALCREDITOFISCAL");

                entity.Property(e => e.Noactualfactura).HasColumnName("NOACTUALFACTURA");

                entity.Property(e => e.Nodigitoscotizacion).HasColumnName("NODIGITOSCOTIZACION");

                entity.Property(e => e.Nodigitoscreditofiscal).HasColumnName("NODIGITOSCREDITOFISCAL");

                entity.Property(e => e.Nodigitosfactura).HasColumnName("NODIGITOSFACTURA");
            });

            modelBuilder.Entity<Cotizacion>(entity =>
            {
                entity.HasKey(e => e.Iidcotizacion)
                    .HasName("PK__COTIZACI__018E120D0B0F7639");

                entity.ToTable("COTIZACION");

                entity.HasIndex(e => e.Iidusuario);

                entity.Property(e => e.Iidcotizacion).HasColumnName("IIDCOTIZACION");

                entity.Property(e => e.Bhabilitado)
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cotizacionemitida)
                    .HasColumnName("COTIZACIONEMITIDA")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Cotizacionfacturada)
                    .HasColumnName("COTIZACIONFACTURADA")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnName("FECHAVENCIMIENTO")
                    .HasColumnType("date");

                entity.Property(e => e.Iidusuario).HasColumnName("IIDUSUARIO");

                entity.Property(e => e.Nocotizacion)
                    .IsRequired()
                    .HasColumnName("NOCOTIZACION")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombrecliente)
                    .HasColumnName("NOMBRECLIENTE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.IidusuarioNavigation)
                    .WithMany(p => p.Cotizacion)
                    .HasForeignKey(d => d.Iidusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__COTIZACIO__IIDUS__6E01572D");
            });

            modelBuilder.Entity<Detallecotizacion>(entity =>
            {
                entity.HasKey(e => e.Iiddetallecotizacio)
                    .HasName("PK__DETALLEC__B66318029EB1D97E");

                entity.ToTable("DETALLECOTIZACION");

                entity.HasIndex(e => e.Iidcotizacion);

                entity.HasIndex(e => e.Iidproducto);

                entity.Property(e => e.Iiddetallecotizacio).HasColumnName("IIDDETALLECOTIZACIO");

                entity.Property(e => e.Bhabilitado)
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Comision)
                    .HasColumnName("COMISION")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Descuento)
                    .HasColumnName("DESCUENTO")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Essubproducto)
                    .IsRequired()
                    .HasColumnName("ESSUBPRODUCTO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnName("FECHAVENCIMIENTO")
                    .HasColumnType("date");

                entity.Property(e => e.Iidcotizacion).HasColumnName("IIDCOTIZACION");

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Porcentajecomision).HasColumnName("PORCENTAJECOMISION");

                entity.Property(e => e.Porcentajedescuento).HasColumnName("PORCENTAJEDESCUENTO");

                entity.Property(e => e.Precioactual)
                    .HasColumnName("PRECIOACTUAL")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("SUBTOTAL")
                    .HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.IidcotizacionNavigation)
                    .WithMany(p => p.Detallecotizacion)
                    .HasForeignKey(d => d.Iidcotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DETALLECO__IIDCO__5EBF139D");

                entity.HasOne(d => d.IidproductoNavigation)
                    .WithMany(p => p.Detallecotizacion)
                    .HasForeignKey(d => d.Iidproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DETALLECO__IIDPR__5FB337D6");
            });

            modelBuilder.Entity<Detallepedido>(entity =>
            {
                entity.HasKey(e => e.Iiddetallepedido)
                    .HasName("PK__DETALLEP__6189D32A00E3D89C");

                entity.ToTable("DETALLEPEDIDO");

                entity.HasIndex(e => e.Iidfactura);

                entity.HasIndex(e => e.Iidproducto);

                entity.Property(e => e.Iiddetallepedido).HasColumnName("IIDDETALLEPEDIDO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Comision)
                    .HasColumnName("COMISION")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Descuento)
                    .HasColumnName("DESCUENTO")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Essubproducto)
                    .IsRequired()
                    .HasColumnName("ESSUBPRODUCTO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Iidfactura).HasColumnName("IIDFACTURA");

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Porcentajecomision).HasColumnName("PORCENTAJECOMISION");

                entity.Property(e => e.Porcentajedescuento).HasColumnName("PORCENTAJEDESCUENTO");

                entity.Property(e => e.Precioactual)
                    .HasColumnName("PRECIOACTUAL")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("SUBTOTAL")
                    .HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.IidfacturaNavigation)
                    .WithMany(p => p.Detallepedido)
                    .HasForeignKey(d => d.Iidfactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DETALLEPE__IIDFA__5165187F");

                entity.HasOne(d => d.IidproductoNavigation)
                    .WithMany(p => p.Detallepedido)
                    .HasForeignKey(d => d.Iidproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DETALLEPE__IIDPR__52593CB8");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Iidempleado)
                    .HasName("PK__EMPLEADO__462140DA42AD8C35");

                entity.ToTable("EMPLEADO");

                entity.Property(e => e.Iidempleado).HasColumnName("IIDEMPLEADO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Dui)
                    .IsRequired()
                    .HasColumnName("DUI")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnName("EDAD");

                entity.Property(e => e.Empleadotieneusuario)
                    .IsRequired()
                    .HasColumnName("EMPLEADOTIENEUSUARIO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Nombrecompleto)
                    .IsRequired()
                    .HasColumnName("NOMBRECOMPLETO")
                    .IsUnicode(false);

                entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<Entrada>(entity =>
            {
                entity.HasKey(e => e.Iidentrada)
                    .HasName("PK__ENTRADA__1AE39997DB223568");

                entity.ToTable("ENTRADA");

                entity.HasIndex(e => e.Iidproducto);

                entity.Property(e => e.Iidentrada).HasColumnName("IIDENTRADA");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Condicionventa)
                    .HasColumnName("CONDICIONVENTA")
                    .IsUnicode(false);

                entity.Property(e => e.Existenciasproducto).HasColumnName("EXISTENCIASPRODUCTO");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Fechaexpedicionccf)
                    .HasColumnName("FECHAEXPEDICIONCCF")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechainiciocredito)
                    .HasColumnName("FECHAINICIOCREDITO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnName("FECHAVENCIMIENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Numeroccf)
                    .HasColumnName("NUMEROCCF")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Preciocompra)
                    .HasColumnName("PRECIOCOMPRA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Proveedor)
                    .HasColumnName("PROVEEDOR")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IidproductoNavigation)
                    .WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.Iidproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENTRADA__IIDPROD__3E52440B");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.Iidfactura)
                    .HasName("PK__FACTURA__C5D90AE94B80389E");

                entity.ToTable("FACTURA");

                entity.HasIndex(e => e.Iidusuario);

                entity.Property(e => e.Iidfactura).HasColumnName("IIDFACTURA");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cambio)
                    .HasColumnName("CAMBIO")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Descuentoglobal)
                    .HasColumnName("DESCUENTOGLOBAL")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Direccion)
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Efectivo)
                    .HasColumnName("EFECTIVO")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Facturaemitida)
                    .IsRequired()
                    .HasColumnName("FACTURAEMITIDA")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Giro)
                    .HasColumnName("GIRO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Iidusuario).HasColumnName("IIDUSUARIO");

                entity.Property(e => e.Nit)
                    .HasColumnName("NIT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nofactura)
                    .IsRequired()
                    .HasColumnName("NOFACTURA")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombrecliente)
                    .IsRequired()
                    .HasColumnName("NOMBRECLIENTE")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Original)
                    .HasColumnName("ORIGINAL")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Porcentajedescuentoglobal).HasColumnName("PORCENTAJEDESCUENTOGLOBAL");

                entity.Property(e => e.Registro)
                    .HasColumnName("REGISTRO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Tipocomprador)
                    .IsRequired()
                    .HasColumnName("TIPOCOMPRADOR")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Total)
                    .HasColumnName("TOTAL")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Totalcomision)
                    .HasColumnName("TOTALCOMISION")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Totaldescuento)
                    .HasColumnName("TOTALDESCUENTO")
                    .HasColumnType("decimal(16, 4)");

                entity.HasOne(d => d.IidusuarioNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.Iidusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FACTURA__IIDUSUA__4E88ABD4");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.Iidinventario)
                    .HasName("PK__INVENTAR__C5FD6D5CA06FC5DB");

                entity.ToTable("INVENTARIO");

                entity.HasIndex(e => e.Iidbodega);

                entity.HasIndex(e => e.Iidproducto);

                entity.HasIndex(e => e.Iidstock);

                entity.Property(e => e.Iidinventario).HasColumnName("IIDINVENTARIO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Iidbodega).HasColumnName("IIDBODEGA");

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Iidstock).HasColumnName("IIDSTOCK");

                entity.HasOne(d => d.IidbodegaNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Iidbodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__INVENTARI__IIDBO__2B0A656D");

                entity.HasOne(d => d.IidproductoNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Iidproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__INVENTARI__IIDPR__2BFE89A6");

                entity.HasOne(d => d.IidstockNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Iidstock)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__INVENTARI__IIDST__3A4CA8FD");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.Iidpagina)
                    .HasName("PK__PAGINA__8E759E4E5CDFE19B");

                entity.ToTable("PAGINA");

                entity.Property(e => e.Iidpagina).HasColumnName("IIDPAGINA");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasColumnName("ACCION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Controlador)
                    .IsRequired()
                    .HasColumnName("CONTROLADOR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Icono)
                    .HasColumnName("ICONO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasColumnName("MENSAJE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paginatipousuario>(entity =>
            {
                entity.HasKey(e => e.Iidpaginatipousuario)
                    .HasName("PK__PAGINATI__80FD03168D47A835");

                entity.ToTable("PAGINATIPOUSUARIO");

                entity.HasIndex(e => e.Iidpagina);

                entity.HasIndex(e => e.Iidtipousuario);

                entity.Property(e => e.Iidpaginatipousuario).HasColumnName("IIDPAGINATIPOUSUARIO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Iidpagina).HasColumnName("IIDPAGINA");

                entity.Property(e => e.Iidtipousuario).HasColumnName("IIDTIPOUSUARIO");

                entity.HasOne(d => d.IidpaginaNavigation)
                    .WithMany(p => p.Paginatipousuario)
                    .HasForeignKey(d => d.Iidpagina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PAGINATIP__IIDPA__4AB81AF0");

                entity.HasOne(d => d.IidtipousuarioNavigation)
                    .WithMany(p => p.Paginatipousuario)
                    .HasForeignKey(d => d.Iidtipousuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PAGINATIP__IIDTI__4BAC3F29");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Iidproducto)
                    .HasName("PK__PRODUCTO__158EDF304C3F3F3B");

                entity.ToTable("PRODUCTO");

                entity.HasIndex(e => e.Iidstock);

                entity.HasIndex(e => e.Iidunidadmedida);

                entity.Property(e => e.Iidproducto).HasColumnName("IIDPRODUCTO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Codigoproducto)
                    .IsRequired()
                    .HasColumnName("CODIGOPRODUCTO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Equivalencia)
                    .HasColumnName("EQUIVALENCIA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Existencias).HasColumnName("EXISTENCIAS");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Ganancia)
                    .HasColumnName("GANANCIA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Iidstock).HasColumnName("IIDSTOCK");

                entity.Property(e => e.Iidunidadmedida).HasColumnName("IIDUNIDADMEDIDA");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Porcentajeganancia).HasColumnName("PORCENTAJEGANANCIA");

                entity.Property(e => e.Preciocompra)
                    .HasColumnName("PRECIOCOMPRA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Precioventa)
                    .HasColumnName("PRECIOVENTA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Restantes)
                    .HasColumnName("RESTANTES")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subexistencia)
                    .HasColumnName("SUBEXISTENCIA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subganancia)
                    .HasColumnName("SUBGANANCIA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subiva)
                    .HasColumnName("SUBIVA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subporcentaje).HasColumnName("SUBPORCENTAJE");

                entity.Property(e => e.Subpreciounitario)
                    .HasColumnName("SUBPRECIOUNITARIO")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subprecioventa)
                    .HasColumnName("SUBPRECIOVENTA")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.Subunidad).HasColumnName("SUBUNIDAD");

                entity.HasOne(d => d.IidstockNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Iidstock)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUCTO__IIDSTO__160F4887");

                entity.HasOne(d => d.IidunidadmedidaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Iidunidadmedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUCTO__IIDUNI__398D8EEE");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Iidstock)
                    .HasName("PK__STOCK__5EBABD9FE67EDAB3");

                entity.ToTable("STOCK");

                entity.Property(e => e.Iidstock).HasColumnName("IIDSTOCK");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Nombrestock)
                    .IsRequired()
                    .HasColumnName("NOMBRESTOCK")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tipousuario>(entity =>
            {
                entity.HasKey(e => e.Iidtipousuario)
                    .HasName("PK__TIPOUSUA__A05A9116419E2F34");

                entity.ToTable("TIPOUSUARIO");

                entity.Property(e => e.Iidtipousuario).HasColumnName("IIDTIPOUSUARIO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Nombretipousuario)
                    .IsRequired()
                    .HasColumnName("NOMBRETIPOUSUARIO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unidadmedida>(entity =>
            {
                entity.HasKey(e => e.Iidunidadmedida)
                    .HasName("PK__UNIDADME__D8851CA5DF24E311");

                entity.ToTable("UNIDADMEDIDA");

                entity.Property(e => e.Iidunidadmedida).HasColumnName("IIDUNIDADMEDIDA");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Nombreunidad)
                    .IsRequired()
                    .HasColumnName("NOMBREUNIDAD")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Iidusuario)
                    .HasName("PK__USUARIO__26DBFF593A56E8F3");

                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.Iidempleado);

                entity.HasIndex(e => e.Iidtipousuario);

                entity.Property(e => e.Iidusuario).HasColumnName("IIDUSUARIO");

                entity.Property(e => e.Bhabilitado)
                    .IsRequired()
                    .HasColumnName("BHABILITADO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasColumnName("CONTRASEÑA")
                    .IsUnicode(false);

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("FECHACREACION")
                    .HasColumnType("date");

                entity.Property(e => e.Iidempleado).HasColumnName("IIDEMPLEADO");

                entity.Property(e => e.Iidtipousuario).HasColumnName("IIDTIPOUSUARIO");

                entity.Property(e => e.Nombreusuario)
                    .IsRequired()
                    .HasColumnName("NOMBREUSUARIO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IidempleadoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Iidempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIO__IIDEMPL__44FF419A");

                entity.HasOne(d => d.IidtipousuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Iidtipousuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIO__IIDTIPO__45F365D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
