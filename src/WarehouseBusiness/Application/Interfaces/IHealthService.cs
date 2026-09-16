using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IHealthService
    {
        public Task<string> CheckHealth();
    }
}
