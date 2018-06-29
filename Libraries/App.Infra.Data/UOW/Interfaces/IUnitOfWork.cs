namespace App.Infra.Data.UOW.Interfaces
{
	public interface IUnitOfWork
	{
		int Commit();
	}
}