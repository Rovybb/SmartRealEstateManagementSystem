using Application.Commands.Inquiry;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.CommandHandlers.Inquiry
{
    public class CreateInquiryCommandHandler : IRequestHandler<CreateInquiryCommand, Result<Guid>>
    {
        private readonly IInquiryRepository inquiryRepository;
        private readonly IMapper mapper;

        public CreateInquiryCommandHandler(IInquiryRepository inquiryRepository, IMapper mapper)
        {
            this.inquiryRepository = inquiryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateInquiryCommand request, CancellationToken cancellationToken)
        {
            var result = await inquiryRepository.CreateAsync(mapper.Map<Domain.Entities.Inquiry>(request));
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
