using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xbim.Ifc;

namespace XBIM_Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TaskOneController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<ElementCount> Get()
		{
			// Copied from quickstart example
			const string fileName = "SampleHouse4.ifc";

			var editor = new XbimEditorCredentials
			{
				ApplicationDevelopersName = "Jamie Hodgson",
				ApplicationFullName = "XBIM_Test",
				ApplicationIdentifier = "1234",
				ApplicationVersion = "1.0",
				EditorsFamilyName = "Hodgson",
				EditorsGivenName = "Jamie",
				EditorsOrganisationName = "Independent Architecture"
			};

			using (var model = IfcStore.Open(fileName, editor))
			{
				// Grouping on the express type and taking a count of each group
				return model.Instances
					.GroupBy(instance => instance.ExpressType)
					.Select(group => new ElementCount
					{
						ElementName = group.Key.ExpressName,
						Count = group.Count()
					})
					.ToArray();
			}
		}
	}
}
