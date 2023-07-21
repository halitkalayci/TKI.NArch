using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Delete;
public class DeleteBrandCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private IBrandRepository _brandRepository;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetAsync(i => i.Id == request.Id);
            if (brand == null)
                throw new BusinessException("Silinmek istenen marka bulunamadı.");
            await _brandRepository.DeleteAsync(brand);
        }
    }
}
