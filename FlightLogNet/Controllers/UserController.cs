namespace FlightLogNet.Controllers
{
    using System.Collections.Generic;

    using Facades;
    using Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class UserController(ILogger<UserController> logger, PersonFacade personFacade) 
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<PersonModel> Get()
        {
            logger.LogDebug("Get club members.");
            return personFacade.GetClubMembers();
        }
    }
}
