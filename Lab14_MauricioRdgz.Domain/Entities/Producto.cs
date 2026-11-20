namespace Lab14_MauricioRdgz.Domain.Entities;

public sealed record Producto(int Id, string Nombre, decimal Precio)
{
    public static Producto Crear(int id, string nombre, decimal precio)
    {
        if (string.IsNullOrWhiteSpace(nombre) || nombre.Trim().Length > 100)
            throw new ArgumentException("El nombre debe tener entre 1 y 100 caracteres.");
        if (precio <= 0) throw new ArgumentException("El precio debe ser mayor que cero.");
        return new Producto(id, nombre.Trim(), precio);
    }
}
