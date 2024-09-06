using AutoMapper;
using Company.Data.Entites;
using Company.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Mapping
{
	public class DepartmentProfile : Profile
	{
		public DepartmentProfile() 
		{
			//Make Mapping With Department And DepartmentDto Automatic And Reverse Mapping If You Need Throw ReverseMap()
			CreateMap<Department, DepartmentDto>().ReverseMap();
		}
	}
}
