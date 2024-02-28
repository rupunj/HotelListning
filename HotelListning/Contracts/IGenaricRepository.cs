
namespace HotelListning.Contracts
{
	public interface IGenaricRepository<T> where T :class
	{
		Task<T> GetAsync(int? Id);

		Task<List<T>> GetAllAsync();

		Task<T> AddAsync(T entity);

		Task DeleteAsync(int Id);

        Task UpdateAsync(T entity);

		Task<bool> Exsist(int id);
		 
	}

}

