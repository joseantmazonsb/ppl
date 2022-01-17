using System;

namespace Core.Exceptions {
    public class EntityNotRemovedError : ModelException {
        public EntityNotRemovedError(Exception exception) : base(exception) {
        }
    }
}