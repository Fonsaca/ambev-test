

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository with Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{

    private readonly DefaultContext _context;


    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a sale 
    /// </summary>
    /// <param name="sale">Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale created</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Delete a sale by ID
    /// </summary>
    /// <param name="id">Sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id);
        if (sale == null)
            return false;

        sale.SetCanceled();

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Get a sale by ID
    /// </summary>
    /// <param name="id">Sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale =  await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sale?.IsCanceled == true)
            return null;

        return sale;
    }

    /// <summary>
    /// Update a sale
    /// </summary>
    /// <param name="sale">sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale Updated</returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        foreach (var newItem in sale.Items.Where(i => i.Id == default))
        {
            newItem.Sale = sale;
            await _context.SaleItems.AddAsync(newItem);
        }

        foreach (var item in sale.Items.Where(i => i.Id != default))
        {
            _context.SaleItems.Update(item);
        }

        _context.Sales.Update(sale);

        await _context.SaveChangesAsync();

        return sale;
    }
}