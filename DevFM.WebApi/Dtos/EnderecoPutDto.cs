namespace DevFM.WebApi.Dtos
{
    public class EnderecoPutDto
    {
        public int EnderecoId { get; set; }
        public string Logradouro { get; set; } 
        public string Numero { get; set; } 
        public string Complemento { get; set; } 
        public string Cep { get; set; } 
        public string Bairro { get; set; } 
    }
}
