using System;

namespace DemoApp.PublicApi.Configuration.Configuration
{
    [Serializable]
    internal sealed class Plugins
    {
        // could be supplied from an infrastructure hook etc.
        // and serialized to be used via Options Monitor abstraction
        public string ControllersAssembly { get; set; } = "DemoApp.PublicApi.Controllers"; 
    }
}
