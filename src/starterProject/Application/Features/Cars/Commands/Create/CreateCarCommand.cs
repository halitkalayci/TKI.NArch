using Application.Features.Cars.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Mailing;
using Domain.Entities;
using Hangfire;
using Infrastructure.FileUpload.Adapters;
using Infrastructure.Payment.Adapters;
using Infrastructure.Payment.Services;
using MailKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.Create;
public class CreateCarCommand : IRequest<CreatedCarDto>
{
    // Kullanıcıdan bu komut için talep ettiğim bilgiler.
    public int Kilometer { get; set; }
    public string Plate { get; set; }
    public string Image { get; set; }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarDto>
    {
        // Bağımlılıklar
        private IMapper _mapper;
        private ICarRepository _carRepository;
        private IPosServiceAdapter _posServiceAdapter;
        private CarBusinessRules _carBusinessRules;
        private Core.Mailing.IMailService _mailService;
        private IFileUploadAdapter _fileUploadAdapter;

        public CreateCarCommandHandler(IMapper mapper, ICarRepository carRepository, CarBusinessRules carBusinessRules, IPosServiceAdapter posServiceAdapter, Core.Mailing.IMailService mailService, IFileUploadAdapter fileUploadAdapter)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _posServiceAdapter = posServiceAdapter;
            _mailService = mailService;
            _fileUploadAdapter = fileUploadAdapter;
        }

        public async Task<CreatedCarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarWithSamePlateShouldNotExist(request.Plate);

            Car mappedCar = _mapper.Map<Car>(request);
            mappedCar.Image = await _fileUploadAdapter.UploadImage(request.Image);
            Car addedCar = _carRepository.Add(mappedCar);

            CreatedCarDto dto = _mapper.Map<CreatedCarDto>(addedCar);

            // Payment alma ihtiyacım
            // Sıkı Bağımlılık
            // StripePayment stripePayment = new StripePayment();
            // stripePayment.MakePayment("123", "612", "12/27");
            _posServiceAdapter.Pay("", 123, DateTime.Now);
            var jobId = BackgroundJob.Schedule(() => sendMail(), TimeSpan.FromSeconds(30));
            // Loglama => Continuations
            return dto;
        }
        // Hangfirein çağıracağı metotlar public olmalı.
        public void sendMail()
        {
            var mail = new Mail();
            mail.Subject = "Yeni araba eklendi";
            mail.HtmlBody = "Yeni araba eklemesi yapıldı. Paneli kontrol ediniz.";
            mail.ToList = new List<MimeKit.MailboxAddress>()
            {
                    new MimeKit.MailboxAddress("Halit Kalaycı","halit@kodlama.io")
            };
            _mailService.SendMail(mail);
        }
    }
}
