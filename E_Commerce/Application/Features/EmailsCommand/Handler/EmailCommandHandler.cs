using E_Commerce.Application.Base;
using E_Commerce.Application.Features.EmailsCommand.Models;
using E_Commerce.Services.Abstraction.IEmailServices;
using MediatR;

namespace E_Commerce.Application.Features.EmailsCommand.Handler
{
    public class EmailCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IEmailServices _emailsService;
        #endregion
        #region Constructors
        public EmailCommandHandler(IEmailServices emailsService)
        {
            _emailsService = emailsService;
        }
        #endregion


        #region Handle Functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Massege, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>("Send Email Failed");
        }

        #endregion
    }
}
