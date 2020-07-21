using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SB.Financa.Service
{
    public static class ApiPessoa
    {
        private static string URI_GET_PESSOA = "api/Pessoa/";

        public static async Task<List<Pessoa>> GetPessoas(string token)
        {
            return await ApiConnection<Pessoa>.GetAsync(token, URI_GET_PESSOA);
        }

    }
}
