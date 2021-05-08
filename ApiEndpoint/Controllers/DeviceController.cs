using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [ApiController]
    [Authorize]
    [Route("{controller}")]
    public class DeviceController : ControllerBase
    {
        DeviceLogic logic;

        public DeviceController(DeviceLogic logic)
        {
            this.logic = logic;
        }



        [HttpDelete("{uid}")]
        public void DeleteDevice(string uid)
        {
            if(User.Identity.Name == logic.GetDevice(uid).UserName || User.IsInRole("Admin"))
            {
                logic.DeleteDevice(uid);
            }
        }

        [HttpGet("{uid}")]
        public Device GetDevice(string uid)
        {
            return logic.GetDevice(uid);
        }

        [HttpGet]
        public IEnumerable<Device> GetAllDevice()
        {
            return logic.GetAllDevice();
        }

        [HttpPost]
        public void AddDevice([FromBody] Device item)
        {
            item.UserName = User.Identity.Name;
            logic.AddDevice(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateDevice(string oldid, [FromBody] Device item)
        {
            if (User.Identity.Name == logic.GetDevice(oldid).UserName || User.IsInRole("Admin"))
            {
                logic.UpdateDevice(oldid, item);
            }
        }

        [HttpPut("{copyId}")]
        public void CopyDevice(string copyId)
        {
            logic.CopyDevice(copyId);
        }

    }
}
