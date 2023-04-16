using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums.EForms
{
    [EFormsEnum]
    public enum HilmaState
    {
        Undefined = 0,

        /// <summary>
        /// Work in progress
        /// </summary>
        Draft = 1,

        /// <summary>
        /// Notice is pending publication from TED
        /// </summary>
        WaitingToBePublished = 2,

        /// <summary>
        /// Notice has been published in TED and Hilma
        /// </summary>
        Published = 3,

        /// <summary>
        /// Notice has been published in Hilma and then rejected in TED
        /// </summary>
        PublishedThenRejected = 4,
    }
}
