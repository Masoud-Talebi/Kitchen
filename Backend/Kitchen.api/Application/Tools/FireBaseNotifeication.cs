using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Kitchen.api;

public class FireBaseNotifeication
{
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
    public async static Task Send(string Title, string body, string PushToken,string FirebasejsonFile)
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(FirebasejsonFile),
            });
        }
        var message = new FirebaseAdmin.Messaging.Message()
        {
            Notification = new Notification
            {
                Title = Title,
                Body = body,
            },
            Token = PushToken,
        };
        var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }
}