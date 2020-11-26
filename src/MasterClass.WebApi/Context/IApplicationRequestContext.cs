using System;

namespace MasterClass.WebApi.Context
{
    public interface IApplicationRequestContext
    {
        Guid Id { get; }
    }
}