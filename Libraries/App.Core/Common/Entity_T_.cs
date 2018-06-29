namespace App.Core.Common
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>
	{
		public virtual T Id
		{
			get;
			set;
		}

		protected Entity()
		{
		}
	}
}