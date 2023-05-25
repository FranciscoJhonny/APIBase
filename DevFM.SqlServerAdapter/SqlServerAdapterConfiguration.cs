using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.SqlServerAdapter
{
    public class SqlServerAdapterConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }

        public double TimeCacheInSeconds { get; internal set; } = 30;
    }
}
