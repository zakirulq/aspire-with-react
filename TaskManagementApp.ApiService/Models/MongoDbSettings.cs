namespace TaskManagementApp.ApiService.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return ConnectionString;
            }

            // Parse the base connection string and add credentials
            var uri = new Uri(ConnectionString);
            var builder = new UriBuilder(uri)
            {
                UserName = Username,
                Password = Password
            };
            
            return builder.ToString();
        }
    }
} 