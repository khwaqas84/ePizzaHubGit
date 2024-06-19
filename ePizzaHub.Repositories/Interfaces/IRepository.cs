namespace ePizzaHub.Repositories.Interfaces
{
   public  interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        int SaveChanges();

        
    }
}
