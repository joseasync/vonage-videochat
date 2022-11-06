using System.ComponentModel.DataAnnotations;

namespace Letsgetchecked.VideoChat.Vonage.Registration;

public class OpenTokOptions
{
    [Required] public int ApiKey  { get; set; }
    [Required] public string ApiSecret  { get; set; }
}