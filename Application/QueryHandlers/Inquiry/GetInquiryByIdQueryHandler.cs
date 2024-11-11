using Application.DTOs;
using Application.Queries.Inquiry;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Inquiry
{
    public class GetInquiryByIdQueryHandler : IRequestHandler<GetInquiryByIdQuery, Result<InquiryDTO>>
    {
        private readonly IInquiryRepository inquiryRepository;
        private readonly IMapper mapper;

        public GetInquiryByIdQueryHandler(IInquiryRepository inquiryRepository, IMapper mapper)
        {
            this.inquiryRepository = inquiryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<InquiryDTO>> Handle(GetInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await inquiryRepository.GetByIdAsync(request.Id);
            if (result.IsSuccess)
            {
                return Result<InquiryDTO>.Success(mapper.Map<InquiryDTO>(result.Data));
            }
            return Result<InquiryDTO>.Failure(result.ErrorMessage);
        }
    }
}
