namespace ExercisesManager.API.Configurations
{
    public class IdentityConfig
    {
        public IdentityConfig(string secret, string issuer)
        {
            IdentitySecret = secret;
            IdentityIssuer = issuer;
        }
        public string IdentityIssuer { get; }
        public string IdentitySecret { get; }
    }
}