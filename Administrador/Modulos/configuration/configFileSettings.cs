using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
namespace Administrador.Modulos.configuration {
    class configFileSettings {
        public static void UpdateConnectionString(string keyName, string keyValue){
            var XmlDoc = new XmlDocument();
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach(System.Xml.XmlNode sprname in XmlDoc.DocumentElement.ChildNodes){
                if(sprname.Name == "connectionStrings"){
                    foreach(System.Xml.XmlNode sprel in sprname.ChildNodes){
                        if(sprel.Attributes[0].Value == keyName){
                            sprel.Attributes[1].Value = keyValue;
                        }
                    }
                }
            }
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
    }
}
