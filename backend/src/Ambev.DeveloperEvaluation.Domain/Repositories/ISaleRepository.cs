using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Create a sale 
        /// </summary>
        /// <param name="sale">Sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale created</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a sale
        /// </summary>
        /// <param name="sale">sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale Updated</returns>
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a sale by ID
        /// </summary>
        /// <param name="id">Sale ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a sale by ID
        /// </summary>
        /// <param name="id">Sale ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
