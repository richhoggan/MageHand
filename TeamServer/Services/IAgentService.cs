using TeamServer.Models;

namespace TeamServer.Services {
    public interface IAgentService {

        void AddAgent(Agent listener);
        IEnumerable<Agent> GetAgents();
        Agent GetAgent(string id);
        void RemoveAgent(Agent listener);

    }

    public class AgentService : IAgentService {

        private readonly List<Agent> _agents = new();

        public void AddAgent(Agent agent) {
            _agents.Add(agent);
        }

        public Agent GetAgent(string id) {
            return GetAgents().FirstOrDefault(agent => agent.Id.Equals(id));
        }

        public IEnumerable<Agent> GetAgents() {
            return _agents;
        }

        public void RemoveAgent(Agent agent) {
            _agents.Remove(agent);
        }
    }
}
