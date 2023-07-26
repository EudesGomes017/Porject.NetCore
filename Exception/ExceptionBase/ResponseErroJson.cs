

using System.Collections.Generic;

namespace Hvex.Exception.ExceptionBase {
    public class ResponseErroJson {
        public List<string> Messages { get; set; }
        public ResponseErroJson(string message) {
                Messages = new List<string> {
                message
            };
        }
        public ResponseErroJson(List<string> message) {
            Messages = message;
        }
    }
}
