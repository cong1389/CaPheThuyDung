namespace App.Core.Common
{
    public interface IEntity<T>
	{
		T Id
		{
			get;
			set;
		}
	}
}