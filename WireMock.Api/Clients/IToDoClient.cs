using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WireMock.Api.Models;

namespace WireMock.Api.Clients
{
    public interface IToDoClient : IDisposable
    {
        Task<IEnumerable<ToDo>> GetToDos();

        Task<ToDo> GetToDo(int id);
    }
}   
