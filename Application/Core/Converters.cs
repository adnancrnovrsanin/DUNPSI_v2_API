using Domain;

namespace Application.Core
{
    public static class Converters
    {
        public static RequirementApproveStatus ConvertToRequirementApproveStatus(string status)
        {
            return status switch
            {
                "APPROVED" => RequirementApproveStatus.APPROVED,
                "WAITING_PRODUCT_MANAGER" => RequirementApproveStatus.WAITING_PRODUCT_MANAGER,
                "WAITING_PROJECT_MANAGER" => RequirementApproveStatus.WAITING_PROJECT_MANAGER,
                "CHANGES_REQUIRED" => RequirementApproveStatus.CHANGES_REQUIRED,
                _ => RequirementApproveStatus.REJECTED
            };
        }
    }
}