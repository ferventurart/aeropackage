using System;
using ErrorOr;
using MediatR;

namespace AeroPackage.Application.Packages.Commands.DeleteAttachment;

public record DeleteAttachmentCommand(int Id, string FileName) : IRequest<ErrorOr<bool>>;

