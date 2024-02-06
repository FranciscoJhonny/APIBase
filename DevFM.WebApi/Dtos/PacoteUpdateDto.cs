namespace DevFM.WebApi.Dtos
{
    public class PacoteUpdateDto
    {
        public int PacoteId { get; set; }
        public string DescricaoPacote { get; set; }
        public decimal ValorPacote { get; set; }
        public bool Ativo {  get; set; }
    }
}
