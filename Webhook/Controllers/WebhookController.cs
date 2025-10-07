using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Webhook.Plugins;

namespace Webhook.Controllers
{
    public class WebhookController : Controller
    {
        public WebhookController() { }

        [HttpPost("/webhook")]
        // Adicione o atributo [FromServices] aqui
        public async Task<IActionResult> Webhook([FromServices] IHubContext<WebhookHub> hubContext)
        {
            using var reader = new StreamReader(HttpContext.Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            Console.WriteLine($"Webhook recebido: {requestBody}");

            await hubContext.Clients.All.SendAsync("ReceberWebhook", requestBody);

            return Ok();
        }
    }
}
