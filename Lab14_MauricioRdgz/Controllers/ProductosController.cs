using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab14_MauricioRdgz.Application.UseCases.Productos.Commands;
using Lab14_MauricioRdgz.Application.UseCases.Productos.Queries;
namespace Lab14_MauricioRdgz.Controllers;

[ApiController]
[Route("api/productos")]
public sealed class ProductosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllProductosQuery(), ct));

    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> Obtener(int id, CancellationToken ct)
        => Ok(await mediator.Send(new GetProductoByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Crear(CreateProductoCommand command, CancellationToken ct)
    {
        var id = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(Obtener), new { id }, new { id });
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> Editar(int id, UpdateProductoCommand command, CancellationToken ct)
    {
        await mediator.Send(command with { Id = id }, ct);
        return NoContent();
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
    {
        await mediator.Send(new DeleteProductoCommand(id), ct);
        return NoContent();
    }
}
