using SAP.Middleware.Connector;
using System;

namespace SapExplorer.Core
{
    public class SapConnection : IDisposable
    {
        private RfcRepository repository;
        private RfcDestination destination;
        private string destinationName;
        private bool isOpen = false;

        public RfcRepository Repository
        {
            get
            {
                this.EnsureConnectionIsOpen();
                return this.repository;
            }
        }

        public RfcDestination Destination
        {
            get
            {
                return this.destination;
            }
        }

        public SapConnection(string destinationName)
        {
            this.destinationName = destinationName;
        }

        private void EnsureConnectionIsOpen()
        {
            if (!isOpen)
            {
                try
                {
                    this.destination = RfcDestinationManager.GetDestination(destinationName);
                    this.repository = this.destination.Repository;
                    this.isOpen = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not connect to SAP.", ex);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
