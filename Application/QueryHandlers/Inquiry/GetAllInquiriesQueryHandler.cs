using Application.DTOs;
using Application.Queries.Inquiry;
using AutoMapper;
using Domain.Repositories;
using Domain.Utils;
using MediatR;

namespace Application.QueryHandlers.Inquiry
{
    public class GetAllInquiriesQueryHandler : IRequestHandler<GetAllInquiriesQuery, Result<IEnumerable<InquiryDTO>>>
    {
        private readonly IInquiryRepository inquiryRepository;
        private readonly IMapper mapper;

        public GetAllInquiriesQueryHandler(IInquiryRepository inquiryRepository, IMapper mapper)
        {
            this.inquiryRepository = inquiryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<InquiryDTO>>> Handle(GetAllInquiriesQuery request, CancellationToken cancellationToken)
        {
            var result = await inquiryRepository.GetAllAsync();
            if (result == null)
            {
                return Result<IEnumerable<InquiryDTO>>.Failure("No inquiries found");
            }

            var inquirys = result.Select(inquiry => mapper.Map<InquiryDTO>(inquiry));
            return Result<IEnumerable<InquiryDTO>>.Success(inquirys);
        }
    }
}
