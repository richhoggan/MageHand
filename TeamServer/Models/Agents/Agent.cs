﻿namespace TeamServer.Models {
    public class Agent {

        public AgentMetadata Metadata { get; }

        public DateTime LastSeen { get; private set; }

        public Agent(AgentMetadata metadata) {
            Metadata = metadata;
        }

        public void CheckIn() {
            LastSeen = DateTime.UtcNow;
        }

        public void GetPendingTasks() {

        }
    }
}
