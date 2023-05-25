namespace DevFM.Domain.Models
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public string DescricaoTurno { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
