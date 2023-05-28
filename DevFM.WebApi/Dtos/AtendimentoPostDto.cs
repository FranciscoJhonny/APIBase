namespace DevFM.WebApi.Dtos
{
    public class AtendimentoPostDto
    {
        public int CuidadorId { get; set; }
        public int CategoriaId { get; set; }
        public string TurnoId { get; set; } = string.Empty;
        public string ProfissionaoCor { get; set; } = string.Empty;
    }
}
