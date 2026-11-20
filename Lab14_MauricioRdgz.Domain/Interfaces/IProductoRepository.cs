using Lab14_MauricioRdgz.Domain.Entities;
namespace Lab14_MauricioRdgz.Domain.Interfaces;

public interface IProductoRepository
{
    Task<IReadOnlyList<Producto>> Listar(CancellationToken ct);
    Task<Producto?> Obtener(int id, CancellationToken ct);
    Task<int> Guardar(Producto producto, CancellationToken ct);
    Task Eliminar(int id, CancellationToken ct);
}
