using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


public class ProductosBLL
{
   private Contexto _contexto;
   public ProductosBLL(Contexto contexto)
    {
    _contexto = contexto;

    }
        public bool Existe(int productoId){
            return _contexto.Productos.Any(o => o.ProductoId == productoId);
        }

        private bool Insertar(Productos producto){
            _contexto.Productos.Add(producto);
            return _contexto.SaveChanges()> 0;
        }

        private bool Modificar(Productos producto){
            _contexto.Entry(producto).State = EntityState.Modified;
            return _contexto.SaveChanges()> 0;
        }

        public bool Guardar(Productos producto){
            if (!Existe(producto.ProductoId))
                return this.Insertar(producto);
            else
                return this.Modificar(producto);
        }

        public bool Eliminar(Productos producto){
            _contexto.Entry(producto).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public Productos? Buscar(int productoId){
            return _contexto.Productos
                    .Where(o => o.ProductoId == productoId)
                    .AsNoTracking()
                    .SingleOrDefault();
                    
        }
        public List<Productos> GetList(Expression<Func<Productos, bool>> Criterio){
            return _contexto.Productos
                .AsNoTracking().Where(Criterio).ToList();
        }

    }
      