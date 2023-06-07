namespace DevFM.Domain.Models
{
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; } = Enumerable.Empty<Usuario>();
    }
}
