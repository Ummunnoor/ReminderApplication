using ReminderApplication.DTOs.RequestModels;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace ReminderApplication.SmsServices
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        public string _smsApikey;
        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smsApikey = _configuration.GetSection("SmsConfig")["smsApikey"];
        }
        public void SendSmsAsync(SmsRequest smsRequest)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                Configuration.Default.ApiKey.Add("api-key", _smsApikey);
            }

            var apiInstance = new TransactionalSMSApi();
            string sender = "ReminderApp";
            string recipient = smsRequest.Recipient;
            string content = smsRequest.Content;
            SendTransacSms.TypeEnum type = SendTransacSms.TypeEnum.Transactional;
            string tag = "testTag";
            string webUrl = "https://example.com/notifyUrl";
            try
            {
                var sendTransacSms = new SendTransacSms(sender, recipient, content, type, tag, webUrl);
                SendSms result = apiInstance.SendTransacSms(sendTransacSms);
                Debug.WriteLine(result.ToJson());
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
        
