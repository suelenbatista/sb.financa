using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection.Metadata.Ecma335;

namespace SB.Financa.Service
{
    public static class ApiConnection<TEntity> where TEntity : class
    {
        public static async Task<List<TEntity>> GetAsync(string token, string uriEndPoint)
        {
            try
            {
                List<TEntity> oLstResult = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("https://localhost:5001/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Token da Requisicao
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));

                    //GET 
                    HttpResponseMessage objResponseMsg = await client.GetAsync(uriEndPoint);

                    if (objResponseMsg.IsSuccessStatusCode)
                    {
                        oLstResult = await objResponseMsg.Content.ReadAsAsync<List<TEntity>>();
                    }
                    else
                    {
                        throw new Exception(string.Format("[Erro - GetAsync] - Erro na chamada do recurso {0}. Status Code Retornado {1}. Objeto de Resposta {2}.", 
                                            uriEndPoint, objResponseMsg.StatusCode, objResponseMsg.Content.ToString()));
                    }
                }

                return oLstResult;
            }
            catch  {
                throw;
            }
        }
    }
}