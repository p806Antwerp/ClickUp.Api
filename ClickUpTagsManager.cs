using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickUp.Api.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ClickUp.Api
{
    public class ClickUpTagsManager
    {

        private readonly string _apiToken;
        private readonly string _spaceId;

        public ClickUpTagsManager(string apiToken, string spaceId)
        {
            _apiToken = apiToken;
            _spaceId = spaceId;
        }

        public async Task<ClickUpTagsDTO> GetTagsAsync()
        {
            var rv = new ClickUpTagsDTO();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _apiToken);
                var response = await client.GetAsync($"https://api.clickup.com/api/v2/space/{_spaceId}/tag");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    rv = JsonConvert.DeserializeObject<ClickUpTagsDTO>(content);
                }
            }
            return rv;
        }
        public async Task<string> UpdateTagAsync(string tagName, string fgColor, string bgColor)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _apiToken);

                var json = new JObject(new JProperty("tag", new JObject
                {
                    { "name", tagName },
                    { "tag_fg", fgColor },
                    { "tag_bg", bgColor }
                }));

                var postData = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://api.clickup.com/api/v2/space/{_spaceId}/tag/{tagName}", postData);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync() + response.StatusCode.ToString();
                }
                else
                {
                    return "Error: " + response.StatusCode;
                }
            }
        }
    }
}
