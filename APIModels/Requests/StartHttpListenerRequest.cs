﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIModels.Requests {
    public class StartHttpListenerRequest {
        public string Name {  get; set; }
        public int BindPort { get; set; }

    }
}
