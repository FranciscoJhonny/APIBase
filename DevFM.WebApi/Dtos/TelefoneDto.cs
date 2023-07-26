namespace DevFM.WebApi.Dtos
{
    public class TelefoneDto
    {
        public int TelefoneId { get; set; } 
        public string NumeroTelefone { get; set; } = string.Empty;
        public int TipoTelefoneId { get; set; }
        public string DescricaoTipoTelefone { get; set; } = string.Empty;
    }
}
