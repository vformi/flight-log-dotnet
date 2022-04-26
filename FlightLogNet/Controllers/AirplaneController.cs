namespace FlightLogNet.Controllers
{
    using System.Collections.Generic;

    using FlightLogNet.Facades;
    using FlightLogNet.Models;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [EnableCors]
    public class AirplaneController : ControllerBase
    {
        // TODO 3.1: Vystavte REST HTTPGet metodu vracející seznam klubových letadel
        // Letadla získáte voláním airplaneFacade
        // dotazované URL je /airplane
        // Odpověď by měla být kolekce AirplaneModel
    }
}
