using System;

namespace Core.Exceptions {
    public class EntityNotInsertedError : ModelException {
        public EntityNotInsertedError(Exception exception) : base(exception) {
        }
    }
}