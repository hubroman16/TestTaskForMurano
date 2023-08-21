using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestAppASP.Models;

namespace TestAppASP.Controllers
{

    public class SController : Controller
    {
        //подключение к БД
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SearchDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        [HttpPost]
        //Обработка поискового запроса
        public async Task<IActionResult> SearchAsync(SearchRequest s)
        {
            try
            {
                // Установка соединения с базой данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверка на наличие запросов с указанным ключевым словом в базе данных
                    string sqlQuery = "SELECT COUNT(*) FROM dbo.SearchRequest WHERE Request = @Value1";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Value1", s.Request);
                    int existingRequests = (int)command.ExecuteScalar();

                    // Если запросов с указанным ключевым словом не найдено, запускаем поиск в Google и сохраняем результаты
                    if (existingRequests == 0)
                    {
                        await SaveGoogleSearchResultsAsync(s);;
                        return ListAllSearchResults(s);
                    }
                    else
                    {
                        return ListAllSearchResults(s);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Произошла ошибка при обращении к базе данных: " + ex.Message);
            }
        }
        //Вывод результатов на страницу
        public IActionResult ListAllSearchResults(SearchRequest s)
        {
            // Создание списка элементов SearchResult для хранения найденных результатов поиска
            List<SearchRequest> searchResults = new List<SearchRequest>();

            try
            {
                // Установка соединения с базой данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Выборка всех результатов поиска из базы данных
                    string sqlQuery = "SELECT * FROM dbo.SearchRequest WHERE Request = @Value1";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Value1", s.Request);
                    SqlDataReader reader = command.ExecuteReader();

                    // Цикл для чтения и добавления данных в список
                    while (reader.Read())
                    {
                        SearchRequest result = new SearchRequest
                        {
                            Description = reader["Description"].ToString(),
                            URL = reader["URL"].ToString()
                        };
                        searchResults.Add(result);
                    }

                    reader.Close();
                }
            }
            catch (Exception e)
            {
                // Обработка исключений, если возникнет ошибка
                return Content("Ошибка!");
            }

            // Возврат представления "SearchView" с передачей списка searchResults в качестве модели
            return View("ViewSearch", searchResults);
        }


        private async Task SaveGoogleSearchResultsAsync(SearchRequest s)
        {
            // Установка соединения с базой данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                GoogleSearchResult googlereq = new GoogleSearchResult(s.Request);

                // Устанавливаем переменную для подсчета количества сохраненных результатов
                int savedResultsCount = 0;

                // Цикл для сохранения результатов с первых 10 страниц Google
                for (int i = 0; i < 10 && savedResultsCount < 10; i++)
                {
                    int startIndex = i * 10 + 1;
                    string[][] results = await googlereq.GSearch(s.Request, startIndex);

                    // Сохранение каждого результата поиска
                    for (int j = 0; j < results.Length && savedResultsCount < 10; j++)
                    {
                        string[] result = results[j];
                        s.Description = result[1];
                        s.URL = result[0];

                        string sqlQuery = "INSERT INTO dbo.SearchRequest (Request, Description, URL) VALUES (@Value1, @Value2, @Value3)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        command.Parameters.AddWithValue("@Value1", s.Request);
                        command.Parameters.AddWithValue("@Value2", s.Description);
                        command.Parameters.AddWithValue("@Value3", s.URL);

                        // Выполнение команды SQL
                        command.ExecuteNonQuery();

                        // Увеличение счетчика сохраненных результатов
                        savedResultsCount++;
                    }
                }
            }
        }
    }

}

