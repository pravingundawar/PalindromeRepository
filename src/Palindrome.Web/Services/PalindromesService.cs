using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Palindrome.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome.Web.Services
{
    public class PalindromesService : IPalindromesService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<PalindromesService> _logger;

        private readonly string _remoteServiceBaseUrl;

        public PalindromesService(HttpClient httpClient, ILogger<PalindromesService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _remoteServiceBaseUrl = $"{_settings.Value.PalindromesServiceUrl}/api/v1/Palindromes";
        }

        public async Task<IndexViewModel> GetPalindromes(int page, int take)
        {
            var uri = $"{_remoteServiceBaseUrl}?pageIndex={page}&pageSize={take}";
            var responseString = await _httpClient.GetStringAsync(uri);
            var viewModel = JsonConvert.DeserializeObject<IndexViewModel>(responseString);

            return viewModel;
        }

        public async Task<PalindromeViewModel> AddPalindrome(PalindromeViewModel viewModel)
        {
            var uri = $"{_remoteServiceBaseUrl}";
            return await PostAsync<PalindromeViewModel>(uri, viewModel);
        }

        private async Task<T> PostAsync<T>(string url, object data) where T : class, new()
        {
            try
            {
                string content = JsonConvert.SerializeObject(data);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _httpClient.PostAsync(url, byteContent).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    _logger.LogError($"PostAsync End, url:{url}, HttpStatusCode:{response.StatusCode}, result:{result}");
                    return new T();
                }
                _logger.LogInformation($"PostAsync End, url:{url}, result:{result}");
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new System.Exception($"response :{responseContent}", ex);
                }
                throw;
            }
        }

    }

}
