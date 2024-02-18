using Core.Specifications;
using FluentValidation;
using System;

namespace FantasticProps.Validators
{
    public class ProduListValidator : AbstractValidator<ProductListRequest>
    {
        public ProduListValidator()
        {
            RuleFor(request => request.PageIndex).GreaterThan(0).WithMessage("PageIndex should be greater then 0");
            RuleFor(request => request.PageSize).GreaterThan(0).LessThanOrEqualTo(50);
            RuleFor(request => request.Sort).NotEmpty(); // Add more specific rules if necessary.
                                                         // For Search, BrandId, and TypeId, add any specific validation rules you need.
                                                         // For example, ensuring Search is not null or too long:
            RuleFor(request => request.Search).NotEmpty().MaximumLength(100);
            // Validate GUIDs if provided
            When(request => request.BrandId.HasValue, () =>
            {
                RuleFor(request => request.BrandId.Value).NotEqual(Guid.Empty);
            });
            When(request => request.TypeId.HasValue, () =>
            {
                RuleFor(request => request.TypeId.Value).NotEqual(Guid.Empty);
            });
        }
    }
}
