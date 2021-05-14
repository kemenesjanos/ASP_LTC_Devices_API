using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            if(this.User.FindFirstValue(ClaimTypes.NameIdentifier) == GetDevice(uid).UserName || this.User.IsInRole("Admin"))
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
            item.UserName = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            logic.AddDevice(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateDevice(string oldid, [FromBody] Device item)
        {
            if (Contains(oldid) && (this.User.FindFirstValue(ClaimTypes.NameIdentifier) == GetDevice(oldid).UserName || this.User.IsInRole("Admin")))
            {
                logic.UpdateDevice(oldid, item);
            }
        }

        public bool Contains(string id)
        {
            return logic.Contains(id);
        }

    }
}
