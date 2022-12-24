using NArcBackEnd.Core.Entities;

namespace NArcBackEnd.Entities.Concrete
{
    public class UserOperationClaim : BaseEntity, IEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
