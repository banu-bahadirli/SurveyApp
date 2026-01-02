using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;


public class Brand : Entity<Guid>
{
	public string Name { get; set; }




	public Brand(Guid id, string name) : this()
	{
		Id = id;
		Name = name;
	}

	public Brand()
	{
	}
}