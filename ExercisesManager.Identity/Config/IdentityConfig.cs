namespace ExercisesManager.Identity.Config
{
    public class IdentityConfig
    {
        public IdentityConfig(
            string host,
            string port,
            string username,
            string password,
            string database,
            string secret, 
            string issuer)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            Database = database;
            IdentitySecret = secret;
            IdentityIssuer = issuer;
        }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string IdentityIssuer { get; }
        public string IdentitySecret { get; }
        public string ConnectionString => $"User ID={Username};Password={Password};Host={Host};Port={Port};Database={Database};Pooling=true;";
    }
}