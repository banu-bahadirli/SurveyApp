using MediatR;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Brands;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>
{
	public string Name { get; set; }



	public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
	{
		private readonly IBrandRepository _brandRepository;



		public CreateBrandCommandHandler(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;


		}

		public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
		{



			Brand brand = new Brand();
			brand.Name = request.Name;
			
			brand.Id = Guid.NewGuid();

			//Brand brand2 = _mapper.Map<Brand>(request);
			//brand2.Id = Guid.NewGuid();

			await _brandRepository.AddAsync(brand);
			//await _brandRepository.AddAsync(brand2);

			CreatedBrandResponse createdBrandResponse =new CreatedBrandResponse();
			createdBrandResponse.Name=brand.Name;
			return createdBrandResponse;
		}
	}
}
