namespace PrioniaApp.Areas.Client.ViewModels.Reward
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? rewardImageUrl)
        {
            Id = id;
            RewardImageUrl = rewardImageUrl;
        }

        public int Id { get; set; }
        public string? RewardImageUrl { get; set; }
    }
}
