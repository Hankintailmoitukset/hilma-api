using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums.EForms
{
    [EFormsEnum]
    public enum TedState
    {
        Undefined = 0,

        /// <summary>
        /// DRAFT
        /// </summary>
        Draft = 1,

        /// <summary>
        /// SUBMITTED
        /// </summary>
        Submitted = 2,

        /// <summary>
        /// STOPPED
        /// </summary>
        Stopped = 3,

        /// <summary>
        /// PUBLISHING
        /// </summary>
        Publishing = 4,

        /// <summary>
        /// PUBLISHED
        /// </summary>
        Published = 5,

        /// <summary>
        /// DELETED
        /// </summary>
        Deleted = 6,

        /// <summary>
        /// NOT_PUBLISHED
        /// </summary>
        NotPublished = 7,

        /// <summary>
        /// CONVERTED_FROM_PUBLISHED
        /// </summary>
        ConvertedFromPublished = 8,

        /// <summary>
        /// ARCHIVED
        /// </summary>
        Archived = 9,

        /// <summary>
        /// VALIDATION_FAILED
        /// </summary>
        ValidationFailed = 10,
    }
}
