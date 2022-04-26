namespace FlightLogNet.Controllers
{
    using System.Collections.Generic;

    using FlightLogNet.Facades;
    using FlightLogNet.Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly PersonFacade personFacade;

        public UserController(ILogger<UserController> logger, PersonFacade personFacade)
        {
            this.logger = logger;
            this.personFacade = personFacade;
        }

        [HttpGet]
        public IEnumerable<PersonModel> Get()
        {
            this.logger.LogDebug("Get club members.");
            return this.personFacade.GetClubMembers();
        }
    }
}
