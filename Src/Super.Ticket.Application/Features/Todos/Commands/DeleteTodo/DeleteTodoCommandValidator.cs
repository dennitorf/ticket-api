﻿using FluentValidation;

namespace Super.Ticket.Application.Features.Todos.Commands.DeleteTodo
{
    public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
    {
        public DeleteTodoCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(0)
                .WithMessage("Id is required");
        }
    }
}
