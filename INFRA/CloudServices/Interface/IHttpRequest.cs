using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRA.CloudServices.Interface
{
    public interface IHttpRequests
    {
        Task<string> GetRestRequestCompletaAsync(string url, string method);
        Task<string> PostRestRequestCompletaBearerAsync<TEntity>(string method, string url, TEntity dados, string contentType, bool temAutorizacao, string token = "");

    }
}