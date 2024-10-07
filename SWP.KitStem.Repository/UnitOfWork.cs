using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository;

public class UnitOfWork : IDisposable
{
    private KitStemContext _context;
    private GenericRepository<Category> _categories;
    private GenericRepository<Kit> _kits;
    private GenericRepository<Lab> _lab;

    public UnitOfWork(KitStemContext context)
    {
        _context = context;
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
    public GenericRepository<Category> Categories
    {
        get
        {
            if (this._categories == null)
            {
                this._categories = new GenericRepository<Category>(_context);
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
