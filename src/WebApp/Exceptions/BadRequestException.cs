using System;

namespace WebApp.Exceptions {
    class BadRequestException : Exception {
        public BadRequestException(string message) : base(message)
        { }
    }
}