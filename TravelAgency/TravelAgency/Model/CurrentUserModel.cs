namespace TravelAgency.Model
{
    public class CurrentUserModel
    {
        public CurrentUserModel()
        {

        }
        public CurrentUserModel(string username, string displayName)
        {
            Username = username;
            DisplayName = displayName;
        }

        public string Username { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
    }
}
