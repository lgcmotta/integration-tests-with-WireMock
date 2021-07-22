using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WireMock.Api.Clients;

namespace WireMock.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoClient _toDoClient;

        public ToDoController(IToDoClient toDoClient)
        {
            _toDoClient = toDoClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _toDoClient.GetToDos());

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetToDoAsync([FromRoute] int id) => Ok(await _toDoClient.GetToDo(id));
    }
}