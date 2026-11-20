using MediatR;
using Lab14_MauricioRdgz.Domain.Entities;
using Lab14_MauricioRdgz.Domain.Interfaces;
namespace Lab14_MauricioRdgz.Application.UseCases.Productos.Queries;

public sealed record GetAllProductosQuery() : IRequest<IReadOnlyList<Producto>>;
public sealed record GetProductoByIdQuery(int Id) : IRequest<Producto>;

internal sealed class GetAllProductosHandler(IProductoRepository repo)
    : IRequestHandler<GetAllProductosQuery, IReadOnlyList<Producto>>
{
    public Task<IReadOnlyList<Producto>> Handle(GetAllProductosQuery r, CancellationToken ct)
        => repo.Listar(ct);
}

internal sealed class GetProductoByIdHandler(IProductoRepository repo)
    : IRequestHandler<GetProductoByIdQuery, Producto>
{
    public async Task<Producto> Handle(GetProductoByIdQuery r, CancellationToken ct)
        => await repo.Obtener(r.Id, ct) ?? throw new KeyNotFoundException("Producto no encontrado.");
}
