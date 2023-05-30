using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class Paciente_PacotePostDto
    {
        public int PacoteId { get; set; }
        public decimal? PacoteMensal { get; set; }
        public int? DiaPlantao { get; set; }
        public decimal? ValorPacote { get; set; }
        public decimal? SalarioCuidador { get; set; }
        public decimal? SalarioDiaCuidador { get; set; }
        public decimal? ValorPlataoCuidador { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? TaxaAdminstrativa { get; set; }

    }
}

