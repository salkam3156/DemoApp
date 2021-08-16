
namespace DemoApp.ApplicationCore.Entities
{
    //TODO: flesh out, introduce validation in ctor
    public sealed record Product(int Id, string Name, float Price, string Description);
}
