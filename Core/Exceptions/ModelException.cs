using System;

namespace Core.Exceptions {
    public abstract class ModelException : Exception {
        protected ModelException() {}
        protected ModelException(string message) : base(message) {}
        protected ModelException(Exception exception) 
            : base("Unable to perform operation due to an internal error.", exception) {}
    }
}