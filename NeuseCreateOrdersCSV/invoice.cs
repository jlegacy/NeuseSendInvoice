using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace NeuseCreateOrdersCSV
{
    public class InvoiceHeader
    {
        public DateTime InvoiceDate { get; set; }
        public String InvoiceType { get; set; }
        public String InvoiceSubType { get; set; }
        public Int32 InvoiceDepartment { get; set; }
        public DateTime InvoiceBookMonth { get; set; }
        public Int32 InvoiceCustomer { get; set; }
        public Int32 InvoiceSalesman { get; set; }
        public String InvoiceComment { get; set; }
    }

    public class InvoiceLines
    {
        public Int32 PartUnique { get; set; }
        public Int32 Ordered { get; set; }
        public Double Price { get; set; }
        public String Description { get; set; }
    }

    public class InvoiceTenders
    {
        public String Type { get; set; }
        public Double Amount { get; set; }
    }

    public class InvoiceShipping
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String StateProvince { get; set; }
        public String Country { get; set; }
        public String ZipPostal { get; set; }
    }

    public class InvoiceBilling
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String StateProvince { get; set; }
        public String Country { get; set; }
        public String ZipPostal { get; set; }
    }

    public class SendInvoice
    {
        public InvoiceHeader InvoiceHeader { get; set; }
        public List<InvoiceLines> InvoiceLines { get; set; }
        public List<InvoiceTenders> InvoiceTenders { get; set; }
        public InvoiceShipping InvoiceShipping { get; set; }
        public InvoiceBilling InvoiceBilling { get; set; }

    }

}

