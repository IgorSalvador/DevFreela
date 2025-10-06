using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.Create;

public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }

    public CreateUserCommand(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public User ToEntity() => new(FullName, Email, BirthDate);
}