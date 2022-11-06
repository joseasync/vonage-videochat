using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Letsgetchecked.VideoChat.Web.Models;
using OpenTokSDK;

namespace Letsgetchecked.VideoChat.Web.Controllers;

public class SessionController : Controller
{
    private readonly ILogger<SessionController> _logger;
    private readonly Dictionary<string, Session> _publicSessions;

    public SessionController(ILogger<SessionController> logger, Dictionary<string, Session> publicSessions)
    {
        _logger = logger;
        _publicSessions = publicSessions;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult SessionSetup(string? id)
    {
        Participant participant = new();
        participant.SessionId = id ?? "";
        return View(participant);
    }
    
    [HttpPost]
    public IActionResult SessionRoom([FromForm] Participant participant)
    {
        if (!_publicSessions.TryGetValue(participant.SessionId, out Session session) ||
            string.IsNullOrEmpty(participant.Name))
        { return Error(); }
        
        string token = session.GenerateToken(role: Role.MODERATOR, data: $"name={participant.Name}",expireTime:0);   
        SessionParticipant spSessionParticipant = new SessionParticipant(session.Id, token, 47600331);

        // SessionParticipant sps = new(participant.SessionId, "token", 123);
        
        return View(spSessionParticipant);
        // return View(sps);
    }
    
    [HttpPost]
    public IActionResult CreateSession([FromServices] OpenTok openTok)
    {
        var session = openTok.CreateSession();
        _publicSessions.Add(session.Id, session);
        // var id = "xpto";
        // return Ok(id);
        return Ok(session.Id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}