﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Paciente_Pacote
    {
        public int PacienteId { get; set; }
        public int PacoteId { get; set; }
        public decimal? PacoteMensal { get; set; } 
        public int? DiaPlantao { get;set; }
        public decimal? ValorPacote { get; set; }
        public decimal? SalarioCuidador { get; set; }
        public decimal? SalarioDiaCuidador { get; set; }
        public decimal? ValorPlataoCuidador { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? TaxaAdminstrativa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public Paciente Paciente { get; set; }
        public Pacote Pacote { get; set; }

    }
}
