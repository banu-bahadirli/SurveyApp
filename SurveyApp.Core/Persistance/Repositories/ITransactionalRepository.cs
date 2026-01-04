using System.Threading.Tasks;

namespace SurveyApp.Core.Persistance.Repositories
{
	public interface ITransactionalRepository
	{
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
	}
}
