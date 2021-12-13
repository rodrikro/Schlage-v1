using System;
using System.Collections.Generic;
using System.Text;
using Plossum.CommandLine;
namespace Lineas.config
{
    [CommandLineManager(ApplicationName = "Schlage Lineas",
    Copyright = "Copyright (c) Miguel Gonzalez")]
    class CommandLineArguments
    {
        [CommandLineOption(Description = "Displays this help text")]
        public bool Help = false;

        [CommandLineOption(Description = "Specifies the setting to render", MinOccurs = 1)]
        public string Setting
        {
            get { return mSetting; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new InvalidOptionValueException(
                        "The setting must not be empty", false);
                mSetting = value;
            }
        }

        private string mSetting = "Notdefined";
    }
}
