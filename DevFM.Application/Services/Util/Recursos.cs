using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Application.Services.Util
{
    public class Recursos
    {
        /// <summary>
        /// Obtém o Hash md5 dado o valor de entrada
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public static string ObterHashMD5(string entrada)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(entrada));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Compara o valor de entrada com o hash fornecido
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerificarHashMD5(string entrada, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hashOfInput = ObterHashMD5(entrada);

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Valida se a senha informada atende os requisitos de cadastro de senha
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static bool ValidarNovaSenha(string senha)
        {
            string caracteresEspeciais = "!@#$%^&*()_+{}|:\"<>?-=[]\\;',./`~";
            if (!senha.Any(char.IsUpper)
                || !senha.Any(x => caracteresEspeciais.Contains(x))
                || !senha.Any(char.IsDigit)
                || !senha.Any(char.IsLower)
                )
                return false;

            return true;
        }
    }
}
