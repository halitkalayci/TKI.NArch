using Application.Features.Customers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Customers.Rules;

public class CustomerBusinessRules : BaseBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task CustomerShouldExistWhenSelected(Customer? customer)
    {
        if (customer == null)
            throw new BusinessException(CustomersBusinessMessages.CustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task CustomerIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CustomerShouldExistWhenSelected(customer);
    }
}