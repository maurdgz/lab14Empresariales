using System.Collections.Concurrent;
using Lab14_MauricioRdgz.Domain.Entities;
using Lab14_MauricioRdgz.Domain.Interfaces;
namespace Lab14_MauricioRdgz.Infrastructure.Repositories;

public sealed class ProductoRepository : IProductoRepository
{
    private readonly ConcurrentDictionary<int, Producto> productos = new(
        new[] { Producto.Crear(1, "Teclado", 85), Producto.Crear(2, "Mouse", 45) }
            .ToDictionary(p => p.Id));
    private int ultimoId = 2;

    public Task<IReadOnlyList<Producto>> Listar(CancellationToken ct)
        => Task.FromResult<IReadOnlyList<Producto>>(productos.Values.OrderBy(p => p.Id).ToArray());

    public Task<Producto?> Obtener(int id, CancellationToken ct)
        => Task.FromResult(productos.GetValueOrDefault(id));

    public Task<int> Guardar(Producto producto, CancellationToken ct)
    {
        if (producto.Id == 0)
        {
            var id = Interlocked.Increment(ref ultimoId);
            productos[id] = producto with { Id = id };
            return Task.FromResult(id);
        }
        if (!productos.TryGetValue(producto.Id, out var anterior)
            || !productos.TryUpdate(producto.Id, producto, anterior))
            throw new KeyNotFoundException("Producto no encontrado o modificado; vuelva a consultar.");
        return Task.FromResult(producto.Id);
    }

    public Task Eliminar(int id, CancellationToken ct)
    {
        if (!productos.TryRemove(id, out _)) throw new KeyNotFoundException("Producto no encontrado.");
        return Task.CompletedTask;
    }
}
