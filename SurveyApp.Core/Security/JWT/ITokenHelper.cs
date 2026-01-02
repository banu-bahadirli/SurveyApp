using SurveyApp.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Security.JWT;

public interface ITokenHelper
{
	AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);
}
