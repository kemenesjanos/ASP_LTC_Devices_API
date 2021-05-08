using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public enum deviceType { I2C, Sensor, Switch}
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public deviceType Type { get; set; }
        public virtual ICollection<Method> Methods { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual DescriptionTabData DescriptionTabData { get; set; }
        public string UserName { get; set; }

        public Device()
        {
            this.Methods = new List<Method>();
            this.Properties = new List<Property>();
            this.DescriptionTabData = new DescriptionTabData();
        }

    }
}
