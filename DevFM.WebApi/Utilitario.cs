namespace DevFM.WebApi
{
    public class Utilitario
    {
      
        /// <summary>
        /// Calcula quantidade de anos passdos com base em duas datas, caso encontre qualquer problema retorna 0 
        /// </summary>
        /// <param name="data">Data inicial</param>
        /// <param name="now">Data final ou deixar nula para data atual</param>
        /// <returns>Retorna inteiro com quantiadde de anos</returns>
        public static int YearsOld(DateTime data, DateTime? now = null)
        {
            // Carrega a data do dia para comparação caso data informada seja nula

            now = ((now == null) ? DateTime.Now : now);

            try
            {
                int YearsOld = (now.Value.Year - data.Year);

                if (now.Value.Month < data.Month || (now.Value.Month == data.Month && now.Value.Day < data.Day))
                {
                    YearsOld--;
                }

                return (YearsOld < 0) ? 0 : YearsOld;
            }
            catch
            {
                return 0;
            }
        }
    }
}
