using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SB.Financa.Service
{
    public static class APILogin
    {
        public static string POST_LOGIN = "api/login";

        public static async Task<string> PostLogin(Usuario usuario)
        {
            String tokenResult = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //POST
                HttpResponseMessage objResponseMsg = await client.PostAsJsonAsync(POST_LOGIN, usuario);
                if (objResponseMsg.IsSuccessStatusCode)
                {
                    tokenResult = (await objResponseMsg.Content.ReadAsAsync<UsuarioLogado>()).Token;
                }
            }

            return tokenResult;
        }
    }
}
