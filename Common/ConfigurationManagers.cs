namespace PaymentGateway.Common
{
    public class ConfigurationManagers
    {
        internal static IConfiguration AppSetting
        {
            get;
        }
        static ConfigurationManagers()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
