using Newtonsoft.Json.Linq;
using System.Net;


namespace TestAppASP
{
    //класс для поиска Google
    public class GoogleSearchResult
    {
        public string Googlesearch { get; set; }

        public GoogleSearchResult(string keywords)
        {
            Googlesearch = keywords;
        }
        public async Task<string[][]> GSearch(string keywords, int startIndex = 1)
        {
            // Инициализация объекта HttpClient для отправки HTTP-запроса к Google API
            HttpClient httpClient = new HttpClient();

            // Формирование URL-адреса для запроса к Google API
            string URL = "https://www.googleapis.com/customsearch/v1"
                + "?key=AIzaSyCQKJN7HkFprK_KUEqyjNULzNTTvYNt-04"
                + "&cx=c37bbf0ada7844668"
                + "&q=" + Uri.EscapeDataString(keywords)
                + "&start=" + startIndex;

            // Отправка GET-запроса к Google API и получение ответа
            HttpResponseMessage response = await httpClient.GetAsync(URL);

            // Обработка ответа
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Разбор JSON-ответа и извлечение ссылки и описания
                dynamic searchResult = JObject.Parse(jsonResponse);

                int numResults = searchResult.items.Count;
                string[][] results = new string[numResults][];

                for (int i = 0; i < numResults; i++)
                {
                    string url = searchResult.items[i].link;
                    string description = searchResult.items[i].snippet;
                    string[] result = new string[2];
                    result[0] = url;
                    result[1] = description;
                    results[i] = result;
                }

                return results;
            }
            else
            {
                return new string[][] { };
            }
        }
    }
}

