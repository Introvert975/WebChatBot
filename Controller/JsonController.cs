using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using static WebChatBot.Pages.IndexModel;
namespace WebChatBot.controller
{
    [Route("JsonController")]
    public class JsonController: Controller
    {
        [HttpGet("GetJson")]
        public IActionResult GetJson()
        {

           
            try 
            {
                // Путь к файлу JSON
                var chatHistoryPath = Path.Combine(Environment.CurrentDirectory, "Data/ChatHistory.json");

                // Чтение истории чата из файла
                var chatHistoryJson = System.IO.File.ReadAllText(chatHistoryPath);
                var chatHistory = JsonSerializer.Deserialize<Dictionary<string, List<UnitHistory>>>(chatHistoryJson);
                return Json(chatHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        } 
    }
}
