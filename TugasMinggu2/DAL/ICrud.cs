namespace TugasMinggu2.DAL
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T obj);
        Task Delete(int id);
        Task<T> Update(T obj);
    }
}
