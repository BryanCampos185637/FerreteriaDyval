
namespace AdminFerreteria.ViewMovels
{
    public class basePaginacion
    {
        public int PaginaActual { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistroPorPagina { get; set; }
        public int TotalPaginas { get; set; }
        public object Filtro { get; set; }
    }
}
