using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class GroupTreeContentOperationClaim : Entity<int>
{
    public int GroupTreeContentId { get; set; }
    public int OperationClaimId { get; set; }
    public virtual GroupTreeContent GroupTreeContent { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }
}