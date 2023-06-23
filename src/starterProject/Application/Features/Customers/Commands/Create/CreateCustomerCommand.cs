using Application.Features.Auth.Rules;
using Application.Features.Customers.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<CreatedCustomerResponse>, ITransactionalRequest
{
    public string CustomerNo { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string IPAddress { get; set; }
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository, CustomerBusinessRules customerBusinessRules, IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<CreatedCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserWithSameEmailShouldNotExist(request.Email);
            
            User user = _mapper.Map<User>(request); // new
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            User addedUser = await _userService.Add(user); // db'deki halini alma


            Customer customer = _mapper.Map<Customer>(request);
            customer.UserId = addedUser.Id;
            customer.CustomerNo = Guid.NewGuid().ToString();

            await _customerRepository.AddAsync(customer);

            CreatedCustomerResponse response = _mapper.Map<CreatedCustomerResponse>(customer);
            return response;
        }
    }
}