

using DataLayer.Data;
using DataLayer.Models;

namespace DataLayer.Repositories {
    public class DeletedUsersRepository : Repository<DeletedUser>, IDeletedUsersRepository {
        public DeletedUsersRepository(MuseumManagementContext context) : base(context) {
        }
    }
}
