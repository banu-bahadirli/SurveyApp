using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Persistance.Repositories;

public interface IEntityTimestamps
{
	DateTime CreatedDate { get; set; }
	DateTime? UpdatedDate { get; set; }
	DateTime? DeletedDate { get; set; }

}
