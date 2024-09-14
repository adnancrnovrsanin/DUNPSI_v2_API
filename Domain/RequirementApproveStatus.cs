namespace Domain
{
    public enum RequirementApproveStatus
    {
        WAITING_PRODUCT_MANAGER_APPROVAL,
        WAITING_PROJECT_MANAGER_APPROVAL,
        WAITING_PRODUCT_MANAGER_CHANGES,
        WAITING_PROJECT_MANAGER_CHANGES,
        APPROVED,
        CHANGES_REQUIRED,
        REJECTED
    }
}