using Amazon.Util.Internal;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Infrastructure.FileUpload.Adapters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Update;
public class UpdateBrandCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>,ILoggableRequest
    {
        private IMapper _mapper;
        private IBrandRepository _brandRepository;
        private IFileUploadAdapter _fileUploadAdapter;
        public UpdateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository, IFileUploadAdapter fileUploadAdapter)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _fileUploadAdapter = fileUploadAdapter;
        }

        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brandToUpdate = await _brandRepository.GetAsync(i => i.Id == request.Id);
            if(brandToUpdate == null)
            {
                throw new BusinessException("Güncellenmeye çalışan marka bulunamadı.");
            }

            brandToUpdate.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Logo))
            {
                brandToUpdate.Logo = await _fileUploadAdapter.UploadImage(request.Logo);
            }
            await _brandRepository.UpdateAsync(brandToUpdate);
        }
    }
}
