using System;
using System.Collections;
using System.Text;
using OPCAutomation;

namespace Lineas.services
{
    public class Opc
    {
        private string valOpcServer;
        private string valOpcTopic;

        private OPCServer opcServer = new OPCServer();
        private OPCGroups opcGroups;
        private OPCGroup opcDefaultGroup;
        private OPCItems opcItems;

        private Hashtable idToCounter = new Hashtable();
        private Hashtable counterToHandle = new Hashtable();
        private Hashtable idToHandle = new Hashtable();

        private int counter = 0;
        private bool connected = false;
        private bool simulation = false;

        public Opc(string server, string topic) {
            valOpcServer = server;
            valOpcTopic = topic;
        }

        public void Connect() {
            if (opcServer.ServerState != (int)OPCServerState.OPCRunning) {
                try {
                    opcServer.Connect(valOpcServer);
                    opcGroups = opcServer.OPCGroups;
                    opcGroups.DefaultGroupIsActive = true;
                    opcDefaultGroup = opcGroups.Add("TestGroup");
                    opcDefaultGroup.UpdateRate = 1000;
                    opcDefaultGroup.IsSubscribed = true;
                    opcItems = opcDefaultGroup.OPCItems;
                    connected = true;
                } catch(Exception ex){
                    connected = false;
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        public void Simulate() {
            System.Console.WriteLine("OPC SImulation");
            this.simulation = true;
        }

        public bool isSimulation() {
            return this.simulation;
        }

        public int AddItem(string id, string tagName) {
            int returnValue = -1;
            try {
                if (!idToCounter.Contains(id)) {
                    OPCItem tmpOPCItem = opcItems.AddItem("[" + valOpcTopic + "]" + tagName, counter);
                    idToCounter.Add(id, counter);
                    counterToHandle.Add(counter, tmpOPCItem.ServerHandle);
                    idToHandle.Add(id, tmpOPCItem);
                    returnValue = counter;
                    counter = counter + 1;
                }
            }
            catch (Exception ex) {
                returnValue = -1;
                System.Console.WriteLine("Error conectar ITEM " + id + ": " + ex.Message);
                return returnValue;
            }
            return returnValue;
        }

        public bool IsConnected() {
            if (this.isSimulation())
                return true;
            return opcServer.ServerState == (int)OPCServerState.OPCRunning;
        }

        public Object GetItemValue(string id) {
            if (idToHandle.Contains(id)) {
                OPCItem tmpOPCItem = (OPCItem)idToHandle[id];
                return tmpOPCItem.Value;
            }

            System.Console.WriteLine("Error, ITEM " + id + " no existe");
            return null;
        }
        public void SetItemValue(string id, object value)
        {
            if (idToHandle.Contains(id)) {
                OPCItem tmpOPCItem = (OPCItem)idToHandle[id];
                tmpOPCItem.Write(value);
            } else {
                System.Console.WriteLine("Error, ITEM " + id + " no existe");
            }
            
        }
    }
}
