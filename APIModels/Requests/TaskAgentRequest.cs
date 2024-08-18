using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIModels.Requests {
    public class TaskAgentRequest {
        public string Command {  get; set; }
        public string[] Arguments { get; set; }
        public byte[] File {  get; set; }

    }
}
