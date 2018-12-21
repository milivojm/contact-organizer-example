using System;
using System.Collections.Generic;
using System.Text;
using ContactOrganizer.Events;
using FluentValidation.Results;

namespace ContactOrganizer.Infrastructure
{
    public interface INotificationHandler
    {
        void HandleValidationError(ValidationResult validationResult);
        void HandleEvent<T>(T domainEvent) where T : IDomainEvent;
    }
}
