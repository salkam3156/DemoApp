using DemoApp.ApplicationCore.Entities;
using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using MediatR;
using System.Collections.Generic;

namespace DemoApp.ApplicationServices.Features.Products
{
    public sealed class GetProductsQuery : IRequest<Result<IEnumerable<Product>, DataAccessResult>>
    {
    }
}
