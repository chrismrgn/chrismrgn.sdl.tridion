using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chrismrgn.sdl.tridion.coreservice;
using Tridion.ContentManager.CoreService.Client;
using chrismrgn.sdl.tridion.core;

namespace chrismgrn.sdl.tridion.coreservice.extensionmethods
{
    public static class SchemaDataExtensions
    {
        public static ItemType GetItemType(this SchemaData item)
        {
            return ItemType.Schema;
        }
    }
}
