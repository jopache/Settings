using System;
using System.Collections.Generic;
using System.Text;

namespace Settings.Common.Models
{
    public class SettingWriteModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SettingsWriteModel
    {
        public List<SettingWriteModel> SettingsToUpdate { get; set; }
    }
}
