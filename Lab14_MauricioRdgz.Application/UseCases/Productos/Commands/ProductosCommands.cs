using MediatR;
using Lab14_MauricioRdgz.Domain.Entities;
using Lab14_MauricioRdgz.Domain.Interfaces;
namespace Lab14_MauricioRdgz.Application.UseCases.Productos.Commands;

public sealed record CreateProductoCommand(string Nombre, decimal Precio) : IRequest<int>;
public sealed record UpdateProductoCommand(string Nombre, decimal Precio) : IRequest
{
    public int Id { get; init; }
}
public sealed record DeleteProductoCommand(int Id) : IRequest;

internal sealed class CreateProductoHandler(IProductoRepository repo) : IRequestHandler<CreateProductoCommand, int>
{
    public Task<int> Handle(CreateProductoCommand r, CancellationToken ct)
        => repo.Guardar(Producto.Crear(0, r.Nombre, r.Precio), ct);
}

internal sealed class UpdateProductoHandler(IProductoRepository repo) : IRequestHandler<UpdateProductoCommand>
{
    public async Task Handle(UpdateProductoCommand r, CancellationToken ct)
        => await repo.Guardar(Producto.Crear(r.Id, r.Nombre, r.Precio), ct);
}

internal sealed class DeleteProductoHandler(IProductoRepository repo) : IRequestHandler<DeleteProductoCommand>
{
    public Task Handle(DeleteProductoCommand r, CancellationToken ct) => repo.Eliminar(r.Id, ct);
}
