using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class Paciente_PacotePutDto
    {
        public int Paciente_PacoteId { get; set; }
        public int PacoteId { get; set; }
        public decimal? PacoteMensal { get; set; }
        public int? DiaPlantao { get; set; }
        public decimal? ValorPacote { get; set; }
        public decimal? ValorPlantaoPacote { get; set; }
        public decimal? SalarioCuidador { get; set; }
        public decimal? SalarioDiaCuidador { get; set; }
        public decimal? ValorPlantaoCuidador { get; set; }
        public string Observacao { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal? TaxaAdminstrativa { get; set; }

    }
}

