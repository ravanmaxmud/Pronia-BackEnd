namespace PrioniaApp.Services.Abstracts
{
    public interface IOrderService
    { 
        Task<string> GenerateUniqueTrackingCodeAsync();
    }
}
