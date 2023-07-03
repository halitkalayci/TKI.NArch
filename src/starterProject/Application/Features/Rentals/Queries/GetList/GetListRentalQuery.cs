using Application.Features.Rentals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;

namespace Application.Features.Rentals.Queries.GetList;

public class GetListRentalQuery : IRequest<GetListResponse<GetListRentalListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListRentalQueryHandler : IRequestHandler<GetListRentalQuery, GetListResponse<GetListRentalListItemDto>>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public GetListRentalQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRentalListItemDto>> Handle(GetListRentalQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListRentalListItemDto> response = _mapper.Map<GetListResponse<GetListRentalListItemDto>>(rentals);
            return response;
        }
    }
}