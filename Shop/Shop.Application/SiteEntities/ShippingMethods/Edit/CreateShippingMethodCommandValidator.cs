using FluentValidation;
using Shop.Application.SiteEntities.ShippingMethods.Create;

namespace Shop.Application.SiteEntities.ShippingMethods.Edit;

public class CreateShippingMethodCommandValidator : AbstractValidator<CreateShippingMethodCommand>
{
    public CreateShippingMethodCommandValidator()
    {
        RuleFor(f => f.Title)
            .NotNull().NotEmpty();
    }
}