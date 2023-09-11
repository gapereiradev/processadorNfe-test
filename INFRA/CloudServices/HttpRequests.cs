using INFRA.CloudServices.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace INFRA.CloudServices
{
    public class HttpRequests : IHttpRequests
    {
        public async Task<string> GetRestRequestCompletaAsync(string url, string method)
        {
            string resposta = "";
            try
            {
                HttpClient client = new HttpClient();
                string urlCompleta = $"{url}/{method}";
                client.BaseAddress = new Uri(urlCompleta);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent httpContent = null;

                httpContent = new StringContent("", Encoding.UTF8, "application/json");

                var response = client.GetAsync(urlCompleta).Result;

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    resposta = await response.Content.ReadAsStringAsync();
                    if (resposta == "")
                        resposta = "200 - Sem Retorno.";
                }
                else
                {
                    resposta = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                resposta = $"Mensagem: {e.Message} - InnerException: {e.InnerException}";
            }
            return resposta;
        }

        public async Task<string> PostRestRequestCompletaBearerAsync<TEntity>(string method, string url, TEntity dados, string contentType, bool temAutorizacao, string token = "")
        {
            string resposta = "";
            try
            {
                HttpClient client = new HttpClient();
                string urlCompleta = $"{url}/{method}";
                client.BaseAddress = new Uri(urlCompleta);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(contentType));
                if (temAutorizacao)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                }
                HttpContent httpContent = null;
                if (dados != null)
                {
                    string json = "";
                    if (contentType.Equals("text/plain"))
                    {
                        json = dados.ToString();
                    }
                    else
                    {
                        json = JsonConvert.SerializeObject(dados);
                    }
                    httpContent = new StringContent(json);
                    MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue((contentType == null) ? "text/plain" : contentType);
                    httpContent.Headers.ContentType = headerValue;
                }
                else
                {
                    httpContent = new StringContent("", Encoding.UTF8, contentType);
                }

                var response = await client.PostAsync(urlCompleta, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    resposta = await response.Content.ReadAsStringAsync();
                    if (resposta == "")
                        resposta = "200 - Sem Retorno.";
                }
                else
                {
                    throw (new Exception(await response.Content.ReadAsStringAsync()));
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return resposta;
        }
    }
}