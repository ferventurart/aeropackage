using System;
using FluentValidation;

namespace AeroPackage.Application.Packages.Commands.DeleteAttachment;

public class DeleteAttachmentCommandValidator : AbstractValidator<DeleteAttachmentCommand>
{
	public DeleteAttachmentCommandValidator()
	{
		RuleFor(r => r.Id)
			.NotEmpty();

		RuleFor(r => r.FileName)
			.NotEmpty();
	}
}

