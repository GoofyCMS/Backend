using System;

namespace Goofy.Domain.Core.Abstractions
{
    public interface IEngine
    {
        IServiceProvider Start();
    }
}
