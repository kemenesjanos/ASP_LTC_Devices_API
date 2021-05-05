using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class MethodRepository : IRepository<Method>
    {
        DeviceContext context = new DeviceContext();
        public void Add(Method item)
        {
            context.Methods.Add(item);
            context.SaveChanges();
        }

        public void Delete(Method item)
        {
            context.Methods.Remove(item);
            context.SaveChanges();
        }

        public void Delete(string uid)
        {
            Delete(Read(uid));
        }

        public Method Read(string uid)
        {
            return context.Methods.FirstOrDefault(t => t.Id == uid);
        }

        public IQueryable<Method> Read()
        {
            return context.Methods.AsQueryable();
        }

        public void Update(string oldid, Method newitem)
        {
            var olditem = Read(oldid);
            olditem.Name = newitem.Name;
            olditem.Description = newitem.Description;
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
}
