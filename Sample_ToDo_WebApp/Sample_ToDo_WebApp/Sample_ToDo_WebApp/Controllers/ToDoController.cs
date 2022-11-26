using Microsoft.AspNetCore.Mvc;
using Sample_ToDo_WebApp.Models;
using Sample_ToDo_WebApp.Services;
using System.Text.Json;

namespace Sample_ToDo_WebApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IAPIService _apiService;
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IAPIService apiService, ILogger<ToDoController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ToDoModel model = new ToDoModel();

            string json = await _apiService.SendAsync("https://localhost:44395/api/todo", HttpMethod.Get);
            model.ToDoList = JsonSerializer.Deserialize<List<ToDo>>(json);

            return View(model);
        }

        //[HttpGet("Delete/{id}/fkUser")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int fkUser)
        {
            string json = await _apiService.SendAsync("https://localhost:44395/api/todo?id="+ id +"&fkUser=" + fkUser, HttpMethod.Delete);
            string response = JsonSerializer.Deserialize<string>(json);

            if (response != "ok")
            {
                _logger.LogError($"API Delete failed: {response}");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDoModel model)
        {
            //api url: /api/todo?description=demo&title=Postman&fkUser=1

            string apiUrl = "https://localhost:44395/api/todo?description=" + model.TD_DTO.Description
                            + "&title=" + model.TD_DTO.Title
                            + "&fkUser=" + model.TD_DTO.FK_User;

            string json = await _apiService.PostAsync(apiUrl);
            //string response = JsonSerializer.Deserialize<string>(json);

            return RedirectToAction("Index");
        }

        //PARTIAL VIEWS---------------------------------------------------------------------------------
        public ActionResult OnGetPartial() =>
            new PartialViewResult
            {
                ViewName = "_DetailPartial",
                ViewData = ViewData
            };
    }
}
