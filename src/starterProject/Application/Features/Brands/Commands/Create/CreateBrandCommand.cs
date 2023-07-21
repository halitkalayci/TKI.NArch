using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.FileUpload.Adapters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create;
public class CreateBrandCommand : IRequest
{
    public string Name { get; set; }
    public string Logo { get; set; }


    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;
        private IFileUploadAdapter _fileUploadAdapter;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileUploadAdapter fileUploadAdapter)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _fileUploadAdapter = fileUploadAdapter;
        }

        public async Task Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);
            brand.Logo = await _fileUploadAdapter.UploadImage(request.Logo);
            await _brandRepository.AddAsync(brand);
        }
    }
}
