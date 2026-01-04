using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Persistance.Repositories
{
	public class EfTransactionalRepository<TContext> : ITransactionalRepository where TContext : DbContext
	{
		private readonly TContext _context;
		private IDbContextTransaction? _transaction;

		public EfTransactionalRepository(TContext context)
		{
			_context = context;
		}

		public async Task BeginTransactionAsync()
		{
			if (_transaction == null)
				_transaction = await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			if (_transaction != null)
			{
				await _transaction.CommitAsync();
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}

		public async Task RollbackTransactionAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}
	}
}
