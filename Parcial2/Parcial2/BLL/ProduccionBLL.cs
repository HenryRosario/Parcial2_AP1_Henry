using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#nullable disable
public class ProduccionBLL
{
    private Contexto _contexto;

    public ProduccionBLL(Contexto _contexto)
    {
        this._contexto = _contexto;
    }
    public bool Existe(int ProduccionId)
    {
        return _contexto.Produccion.Any(p => p.ProduccionId == ProduccionId);
    }
    public bool Insertar(Produccion produccion)
    {
        var paso = false;
        try
        {
            Productos producto;
            foreach (var detalle in produccion.ProduccionDetalle)
            {
                producto = _contexto.Productos.SingleOrDefault(p => p.ProductoId == detalle.ProductoId);
                if (producto != null)
                {
                    producto.Existencia -= detalle.Cantidad;
                    _contexto.Entry(producto).State = EntityState.Modified;
                    _contexto.Entry(detalle).State = EntityState.Added;
                }
            }
            var Produccion = _contexto.Productos.SingleOrDefault(p => p.ProductoId == produccion.ProductoId);
            if (produccion != null)
            {
                Produccion.Existencia += produccion.Cantidad;
                _contexto.Entry(produccion).State = EntityState.Modified;
            }
            _contexto.Entry(produccion).State = EntityState.Added;
            paso = _contexto.SaveChanges() > 0;
            _contexto.Entry(produccion).State = EntityState.Detached;
        }
        catch (Exception)
        {
            return false;
        }
        return paso;
    }
    public bool Modificar(Produccion produccion)
    {
        var paso = false;
        try
        {
            var ProduccionAntiguo = Buscar(produccion.ProduccionId);
            Productos? producto;
            if (ProduccionAntiguo != null)
            {
                foreach (var detalle in ProduccionAntiguo.ProduccionDetalle)
                {
                    producto = _contexto.Productos.Find(detalle.DetalleId);

                    if (producto != null)
                        producto.Existencia += detalle.Cantidad;
                }
                var produccionAntiguo = _contexto.Productos.Find(ProduccionAntiguo.ProductoId);
                if (produccionAntiguo != null)
                {
                    produccionAntiguo.Existencia -= ProduccionAntiguo.Cantidad;
                    _contexto.Entry(produccionAntiguo).State = EntityState.Modified;
                }
            }
            _contexto.Database.ExecuteSqlRaw($"DELETE FROM ProduccionDetalle WHERE ProduccionId = {produccion.ProduccionId}");
            foreach (var New in produccion.ProduccionDetalle)
            {
                producto = _contexto.Productos.Find(New.DetalleId);
                if (producto != null)
                    producto.Existencia -= New.Cantidad;
                _contexto.Entry(New).State = EntityState.Added;
            }
            var ProduccionNuevo = _contexto.Productos.Find(produccion.ProductoId);
            if (ProduccionNuevo != null)
            {
                ProduccionNuevo.Existencia += produccion.Cantidad;
                _contexto.Entry(ProduccionNuevo).State = EntityState.Modified;
            }
            _contexto.Entry(produccion).State = EntityState.Modified;
            paso = _contexto.SaveChanges() > 0;
            _contexto.Entry(produccion).State = EntityState.Detached;

        }
        catch (Exception)
        {
            return false;
        }
        return paso;
    }
    public bool Guardar(Produccion produccion)
    {
        try
        {
            if (!Existe(produccion.ProduccionId))
                return Insertar(produccion);
            else
                return Modificar(produccion);
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool Eliminar(Produccion produccion)
    {
        var paso = false;
        try
        {
            Productos? producto;
            foreach (var detalle in produccion.ProduccionDetalle)
            {
                producto = _contexto.Productos.SingleOrDefault(p => p.ProductoId == detalle.ProductoId);
                if (producto != null)
                {
                    producto.Existencia += detalle.Cantidad;
                    _contexto.Entry(producto).State = EntityState.Modified;
                }
            }
            var Produccion = _contexto.Productos.SingleOrDefault(p => p.ProductoId == produccion.ProductoId);
            if (produccion != null)
            {
                Produccion.Existencia -= produccion.Cantidad;
                _contexto.Entry(produccion).State = EntityState.Modified;
            }
            _contexto.Entry(produccion).State = EntityState.Deleted;
            paso = _contexto.SaveChanges() > 0;
            _contexto.Entry(produccion).State = EntityState.Detached;

        }
        catch (Exception)
        {
            return false;
        }
        return paso;
    }
    public Produccion Buscar(int ProduccionId)
    {
        return _contexto.Produccion.Include(p => p.ProduccionDetalle).Where(p => p.ProduccionId == ProduccionId).AsNoTracking().SingleOrDefault();
    }
    public List<Produccion> GetList(Expression<Func<Produccion, bool>> criterio)
    {
        return _contexto.Produccion.Include(p => p.ProduccionDetalle).AsNoTracking().Where(criterio).ToList();
    }
}