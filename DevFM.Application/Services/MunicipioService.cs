﻿using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Application.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioSqlReadAdapter _MunicipioSqlAdapter;

        public MunicipioService(IMunicipioSqlReadAdapter MunicipioSqlAdapter)
        {
            _MunicipioSqlAdapter = MunicipioSqlAdapter;
        }

        public async Task<IEnumerable<Municipio>> ObterMunicipioAsync()
        {
            return await _MunicipioSqlAdapter.ObterMunicipioAsync();
        }

        public async Task<Municipio> ObterMunicipioPorIdAsync(int municipioId)
        {
            return await _MunicipioSqlAdapter.ObterMunicipioPorIdAsync(municipioId);
        }

        public async Task<IEnumerable<Municipio>> ObterMunicipioPorEstadoIdAsync(int estadoId)
        {
            return await _MunicipioSqlAdapter.ObterMunicipioPorEstadoIdAsync(estadoId);
        }
    }
}
