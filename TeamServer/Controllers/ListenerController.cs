﻿using APIModels.Requests;
using Microsoft.AspNetCore.Mvc;
using TeamServer.Models;
using TeamServer.Services;

namespace TeamServer.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ListenerController : ControllerBase {

        private readonly IListenerService _listeners;

            _listeners = listeners;        public ListenerController(IListenerService listeners) {

        }

        [HttpGet]
        public IActionResult GetListeners() {
            var listeners = _listeners.GetListeners();
            return Ok(listeners);
        }

        [HttpGet("{name}")]
        public IActionResult GetListener(string name) {
            var listener = _listeners.GetListener(name);
            if (listener is null) {
                return NotFound();
            }
            return Ok(listener);
        }

        [HttpPost]
        public IActionResult StartListener([FromBody] StartHttpListenerRequest request) {
            var listener = new HttpListener(request.Name, request.BindPort);
            listener.Start();
            _listeners.AddListener(listener);
            var root = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/{listener.Name}";

            return Created(path, listener);
        }

        [HttpDelete("{name}")]
        public IActionResult StopListener(string name) {
            var listener = _listeners.GetListener(name);
            if (listener is null) {
                return NotFound();
            }
            listener.Stop();
            _listeners.RemoveListener(listener);

            return NoContent();
        }
    }
}
