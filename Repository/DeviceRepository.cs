using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class DeviceRepository : IRepository<Device>
    {
        DeviceContext context = new DeviceContext();
        public void Add(Device item)
        {
            item.Id = null;
            item.DescriptionTabData.Id = null;
            context.Devices.Add(item);
            context.SaveChanges();
        }

        public bool Contains(string uid)
        {
            return context.Devices.Where(x => x.Id == uid).Count() == 1;
        }

        public void Delete(Device item)
        {
            context.Devices.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string uid)
        {
            Delete(Read(uid));
        }

        public Device Read(string uid)
        {
            return context.Devices.FirstOrDefault(t => t.Id == uid);
        }

        public IQueryable<Device> Read()
        {
            return context.Devices.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string oldid, Device newitem)
        {
            var olditem = Read(oldid);
            olditem.DescriptionTabData = newitem.DescriptionTabData;
            olditem.Type = newitem.Type;

            olditem.Methods.Clear();
            foreach (var item in newitem.Methods)
            {
                olditem.Methods.Add(item);
            }

            olditem.Properties.Clear();
            foreach (var item in newitem.Properties)
            {
                olditem.Properties.Add(item);
            }

            context.SaveChanges();
        }
    }
}
