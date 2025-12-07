using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IHealthService
    {
        Task<string> CheckHealth();
    }
}
