using System;

namespace MasterClass.WebApi.Context
{
    public class ApplicationRequestContext : IApplicationRequestContext
    {
        public ApplicationRequestContext()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; }
    }
}