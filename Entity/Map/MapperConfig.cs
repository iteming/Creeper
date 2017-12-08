using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entity.Base;

namespace Entity.Map
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Admin, DTO_Admin>();
        }
        public static T Get<T>(object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
