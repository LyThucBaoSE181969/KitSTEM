using SWP.KitStem.API.Data;
using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository;

public class UnitOfWork : IDisposable
{
    private DataContext _context;
    private GenericRepository<KitsCategory> _categories;
    private GenericRepository<Kit> _kits;
    private GenericRepository<KitImage> _kitImage;
    private GenericRepository<Lab> _lab;
    private GenericRepository<Level> _level;
    private GenericRepository<Cart> _cart;     
    private GenericRepository<UserOrders> _order;
    private GenericRepository<Component> _component;
                                                
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public GenericRepository<Component> Components
    {
        get
        {
            if (this._component == null)
            {
                this._component = new GenericRepository<Component>(_context);
            }
            return _component;
        }
    }

    public GenericRepository<Level> Levels
    {
        get
        {
            if (this._level == null)
            {
                this._level = new GenericRepository<Level>(_context);
            }
            return _level;
        }
    }
    public GenericRepository<KitImage> KitImage
    {
        get
        {
            if (this._kitImage == null)
            {
                this._kitImage = new GenericRepository<KitImage>(_context);
            }
            return _kitImage;
        }
    }
    public GenericRepository<UserOrders> Orders
    {
        get
        {
            if (this._order == null)
            {
                this._order = new GenericRepository<UserOrders>(_context);
            }
            return _order;
        }
    }
    public GenericRepository<Cart> Carts
    {
        get
        {
            if (this._cart == null)
            {
                this._cart = new GenericRepository<Cart>(_context);
            }
            return _cart;
        }
    }
    public GenericRepository<Lab> Labs
    {
        get
        {
            if (this._lab == null)
            {
                this._lab = new GenericRepository<Lab>(_context);
            }
            return _lab;
        }
    }

    public GenericRepository<Kit> Kits
    {
        get
        {
            if (this._kits == null)
            {
                this._kits = new GenericRepository<Kit>(_context);
            }
            return _kits;
        }
    }
    public GenericRepository<KitsCategory> Categories
    {
        get
        {
            if (this._categories == null)
            {
                this._categories = new GenericRepository<KitsCategory>(_context);
            }
            return _categories;
        }
    }



    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
