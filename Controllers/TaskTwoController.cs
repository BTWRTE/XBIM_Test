using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;

namespace XBIM_Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TaskTwoController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<RoomArea> Get()
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
				// Filter instance list to IfcSpace types and create models
				return model.Instances
					.OfType<IfcSpace>()
					.Select(room => new RoomArea
					{
						RoomName = room.Name,
						FloorArea = room.GrossFloorArea != null
							? (double?)Convert.ToDouble(room.GrossFloorArea.Value)
							: null
					})
					.ToArray();
			}
		}
	}
}
