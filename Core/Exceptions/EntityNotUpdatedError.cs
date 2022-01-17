using System;

namespace Core.Exceptions {
    public class EntityNotUpdatedError : ModelException {
        public EntityNotUpdatedError(Exception exception) : base(exception) {
            
        }
    }
}