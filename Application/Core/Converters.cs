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
                "WAITING_PRODUCT_MANAGER_APPROVAL" => RequirementApproveStatus.WAITING_PRODUCT_MANAGER_APPROVAL,
                "WAITING_PROJECT_MANAGER_APPROVAL" => RequirementApproveStatus.WAITING_PROJECT_MANAGER_APPROVAL,
                "WAITING_PRODUCT_MANAGER_CHANGES" => RequirementApproveStatus.WAITING_PRODUCT_MANAGER_CHANGES,
                "WAITING_PROJECT_MANAGER_CHANGES" => RequirementApproveStatus.WAITING_PROJECT_MANAGER_CHANGES,
                "CHANGES_REQUIRED" => RequirementApproveStatus.CHANGES_REQUIRED,
                _ => RequirementApproveStatus.REJECTED
            };
        }

        public static RequirementType ConvertToRequirementType(string type)
        {
            return type switch
            {
                "USER_STORY" => RequirementType.USER_STORY,
                "BUG" => RequirementType.BUG,
                _ => RequirementType.TASK
            };
        }

        public static RequirementPriority ConvertToRequirementPriority(int priority)
        {
            return priority switch
            {
                1 => RequirementPriority.VeryLow,
                2 => RequirementPriority.Low,
                3 => RequirementPriority.Medium,
                4 => RequirementPriority.High,
                _ => RequirementPriority.VeryHigh
            };
        }
    }
}