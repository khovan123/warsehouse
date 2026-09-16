using Application.Interfaces;

namespace Application.Services
{
    public class HealthService : IHealthService
    {
        public async Task<string> CheckHealth()
        {
            return await Task.FromResult("OK");
        }
    }
}
