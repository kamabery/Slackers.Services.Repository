using System;

namespace Slackers.Services.Repository
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}