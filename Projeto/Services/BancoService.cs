using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projeto.Interfaces;
using Projeto.Models;

namespace Projeto.Services
{
    public class BancoService : IBancoService
    {
        private string baseUrl = "https://localhost:6001/Banco";

        public async Task<BancoResult> Gravar(Banco modelo)
        {

            using (var httpClient = new HttpClient())
            {
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(modelo), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{baseUrl}/Gravar", conteudo))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BancoResult>(apiResponse);
                    return result;
                }

            }
        }

        public async Task<BancoResult> Atualizar(int id, Banco modelo)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(modelo), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{baseUrl}/Atualizar/{id}", conteudo))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BancoResult>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<BancoResult> Excluir(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{baseUrl}/Excluir/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BancoResult>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<List<Banco>> BuscarTodos()
        {
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userAuth.GetToken());

                using (var response = await httpClient.GetAsync($"{baseUrl}/BuscarTodos"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Banco>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<Banco> BuscarPorId(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/BuscarPorId/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Banco>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<List<Banco>> Filtrar(string filtro)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Filtrar/{filtro}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Banco>>(apiResponse);
                    return result;
                }
            }
        }
    }
}