using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class EditController : ControllerBase
    {
        DeviceLogic dlogic;

        public EditController(DeviceLogic dlogic)
        {
            this.dlogic = dlogic;
        }

        [HttpGet]
        public void FillDb()
        {
            dlogic.FillDbWithSamples();
        }
    }
}
