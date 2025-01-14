using FirstApi.Services.Interfaces;

namespace FirstApi.Services.Implementation
{
    public class LocalMailService : IMailService
    {
        private readonly string? _mailTo = string.Empty;
        private readonly string? _mailFrom = string.Empty;
        // private readonly zadim ke ye moqe kasi natone taqir bede
        //? gozashtam bara inke to ctor paein dasht warning midad ke momkene null bashe
        //bad mirim appsetting.json onja meqdar midim va chon to haste core hast nemikhad program.cs beri
        //faqat inja ctor bezan Iconfig
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailsetting:mailto"];
            _mailFrom = configuration["mailsetting:mailfrom"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to {_mailTo} , "
                + $"with {nameof(LocalMailService)}");
            Console.WriteLine($"subject {subject}");
            Console.WriteLine($"message {message}");
        }
    }
}
