using Application.Features.Rentals.Constants;
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;
using Application.Services.UserService;
using Application.Services.Customers;
using Core.Security.Entities;
using Hangfire;
using Core.Mailing;

namespace Application.Features.Rentals.Commands.Create;

public class CreateRentalCommand : IRequest<CreatedRentalResponse>, ISecuredRequest
{
    public long CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public string[] Roles => new[] { Admin, Write, RentalsOperationClaims.Create };

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, CreatedRentalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;
        private readonly RentalBusinessRules _rentalBusinessRules;
        private readonly ICustomersService _customerService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public CreateRentalCommandHandler(IMapper mapper, IRentalRepository rentalRepository,
                                         RentalBusinessRules rentalBusinessRules, ICustomersService customerService, IUserService userService, IMailService mailService)
        {
            _mapper = mapper;
            _rentalRepository = rentalRepository;
            _rentalBusinessRules = rentalBusinessRules;
            _customerService = customerService;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<CreatedRentalResponse> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            //request.CustomerId = _customerService
            Rental rental = _mapper.Map<Rental>(request);

            await _rentalRepository.AddAsync(rental);

            CreatedRentalResponse response = _mapper.Map<CreatedRentalResponse>(rental);

            User user = await _userService.GetById(request.CustomerId);

            TimeSpan timeDiff = request.RentalEndDate - request.RentalStartDate;

            BackgroundJob.Schedule(() => sendThanksMail(user), TimeSpan.FromMinutes(1));
            BackgroundJob.Schedule(() => sendReminder(user), TimeSpan.FromDays(timeDiff.Days - 1));
            return response;
        }

        public void sendReminder(User user)
        {
            var mail = new Mail();
            mail.Subject = "TKI RentACar";
            mail.HtmlBody = "Kiralamanýz bir gün sonra sona eriyor.";
            mail.ToList = new List<MimeKit.MailboxAddress>()
            {
                new($"{user.FirstName} {user.LastName}", user.Email)
            };
            _mailService.SendMail(mail);
        }

        public void sendThanksMail(User user)
        {
            var mail = new Mail();
            mail.Subject = "TKI RentACar";
            mail.HtmlBody = "Bizi tercih ettiðiniz için teþekkürler.";
            mail.ToList = new List<MimeKit.MailboxAddress>()
            {
                new($"{user.FirstName} {user.LastName}", user.Email)
            };
            _mailService.SendMail(mail);
        }
    }
}