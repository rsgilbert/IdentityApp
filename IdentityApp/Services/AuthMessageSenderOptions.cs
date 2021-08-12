namespace IdentityApp.Services
{
    // summary
    //   A class to fetch the secure email key
    public class AuthMessageSenderOptions
    {
        public string SendGridUser {get;set;}
        public string SendGridKey {get;set;}
    }
}