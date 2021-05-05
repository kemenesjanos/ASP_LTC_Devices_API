using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class DeviceLogic
    {
        IRepository<Device> DeviceRepo;

        public DeviceLogic(IRepository<Device> DeviceRepo)
        {
            this.DeviceRepo = DeviceRepo;
        }

        //crud methods

        public void AddDevice(Device item)
        {
            this.DeviceRepo.Add(item);
        }

        public void DeleteDevice(string uid)
        {
            this.DeviceRepo.Delete(uid);
        }

        public IQueryable<Device> GetAllDevice()
        {
            return DeviceRepo.Read();
        }

        public Device GetDevice(string uid)
        {
            return DeviceRepo.Read(uid);
        }

        public void UpdateDevice(string uid, Device newitem)
        {
            DeviceRepo.Update(uid, newitem);
        }

        //non-crud methods/properties

        //public double AVGVideosCount
        //{
        //    get
        //    {
        //        return GetAllDevice().Average(t => t.Videos.Count());
        //    }
        //}

        //public void AddVideoToDevice(Video video, string DeviceId)
        //{
        //    GetDevice(DeviceId).Videos.Add(video);
        //    DeviceRepo.Save();
        //}
        //public void RemoveVideoFromDevice(Video video, string DeviceId)
        //{
        //    GetDevice(DeviceId).Videos.Remove(video);
        //    videoRepo.Delete(video.Uid);
        //    DeviceRepo.Save();
        //}

        public void FillDbWithSamples()
        {
            Method m1 = new Method() { Description = "m1 des", Name = "m1" };
            Method m2 = new Method() { Description = "m2 des", Name = "m2" };
            Method m3 = new Method() { Description = "m3 des", Name = "m3" };

            Property p1 = new Property() { Description = "d1 des", Name = "d1" };
            Property p2 = new Property() { Description = "d2 des", Name = "d2" };

            Device d1 = new Device() { Type = deviceType.I2C, DescriptionTabData = new DescriptionTabData() {Description="Display.", ShortDescription="Disp", Name="Display", Example="alma"} };
            Device d2 = new Device() { Type = deviceType.Switch, DescriptionTabData = new DescriptionTabData() { Description = "Switch.", ShortDescription = "switch", Name = "Switch", Example = "körte" } };

            GetDevice(d1.Id).Methods.Add(m1);
            GetDevice(d1.Id).Methods.Add(m2);
            GetDevice(d2.Id).Methods.Add(m3);
            GetDevice(d2.Id).Properties.Add(p1);
            GetDevice(d2.Id).Properties.Add(p2);

            DeviceRepo.Save();

        }

    }
}
