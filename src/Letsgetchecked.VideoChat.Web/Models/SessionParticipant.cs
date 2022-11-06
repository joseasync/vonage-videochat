namespace Letsgetchecked.VideoChat.Web.Models;

public class SessionParticipant
{
    public SessionParticipant(string sessionId, string token, int apiKey)
    {
        SessionId = sessionId;
        Token = token;
        ApiKey = apiKey;
    }

    public string SessionId { get; set; }
    public string Token { get; set; }
    public int ApiKey { get; set; }
}