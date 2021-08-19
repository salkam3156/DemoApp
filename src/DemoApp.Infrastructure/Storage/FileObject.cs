using DemoApp.Infrastructure.Models;
using System;

namespace DemoApp.ApplicationServices.Contracts
{
    public interface IFileStorage
    {
        TransientFileModel GetTransientFileForProcessing(FileLocation location)
        {
            return new TransientFileModel(location);
        }
    }
}
