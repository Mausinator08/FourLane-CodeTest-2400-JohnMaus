using System;
using System.Collections.Generic;
using System.Linq;

// Xml Serialization Namespace
using System.Xml.Serialization;

namespace FourLane_CodeTest_2400_JohnMaus
{
	// Define the class representing the root Xml element, InvoiceQueryRs.
	[Serializable]
	[XmlRoot(ElementName = "InvoiceQueryRs")]
	public class InvoiceQueryRs
	{
		#region Attributes

		[XmlAttribute("statusCode")]
		public int statusCode { get; set; }

		[XmlAttribute("statusSeverity")]
		public string statusSeverity { get; set; }

		[XmlAttribute("statusMessage")]
		public string statusMessage { get; set; }

		[XmlAttribute("retCount")]
		public int retCount { get; set; }

		[XmlAttribute("iteratorRemainingCount")]
		public int iteratorRemainingCount { get; set; }

		[XmlAttribute("iteratorID")]
		public Guid iteratorID { get; set; }

		#endregion

		// Provide a constructor for initial instantiation of nested elements' objects. (Such as if their are more than one, then a List<> of that class type for that element.)
		public InvoiceQueryRs()
		{
			invoiceRets = new List<InvoiceRet>();
		}

		#region Elements

		// In case there is more than one invoice.
		[XmlElement(ElementName = "InvoiceRet")]
		public List<InvoiceRet> invoiceRets { get; set; }

		public InvoiceRet GetInvoiceRet(uint txnID)
		{
			return invoiceRets.FirstOrDefault(i => i.txnID == txnID);
		}

		#endregion
	}

	// InvoiceRet is the only immediate child element in InvoiceQueryRs
	public class InvoiceRet
	{
		public InvoiceRet()
		{
			customerRef = new CustomerRef();
			classRef = new ClassRef();
			invoiceLineRets = new List<InvoiceLineRet>();
			invoiceLineGroupRets = new List<InvoiceLineGroupRet>();
			dataExtRets = new List<DataExtRet>();
		}

		[XmlElement(ElementName = "TxnID")]
		public uint txnID { get; set; }

		[XmlElement(ElementName = "TimeCreated")]
		public DateTime timeCreated { get; set; }

		[XmlElement(ElementName = "TimeModified")]
		public DateTime timeModified { get; set; }

		[XmlElement(ElementName = "EditSequence")]
		public string editSequence { get; set; }

		[XmlElement(ElementName = "TxnNumber")]
		public int txnNumber { get; set; }

		// This element has its own child properties
		[XmlElement(ElementName = "CustomerRef")]
		public CustomerRef customerRef { get; set; }

		// This element has its own child properties
		[XmlElement(ElementName = "ClassRef")]
		public ClassRef classRef { get; set; }

		// This element has its own child properties (There may be more than one, so making a list)
		[XmlElement(ElementName = "InvoiceLineRet")]
		public List<InvoiceLineRet> invoiceLineRets { get; set; }

		// Just a helper function to get one invoice line by txnLineID
		public InvoiceLineRet GetInvoiceLineRet(uint txnLineID)
		{
			return invoiceLineRets.FirstOrDefault(e => e.txnLineID == txnLineID);
		}

		// This element has its own child properties (There may be more than one, so making a list)
		[XmlElement(ElementName = "InvoiceLineGroupRet")]
		public List<InvoiceLineGroupRet> invoiceLineGroupRets { get; set; }

		public InvoiceLineGroupRet GetInvoiceLineGroupRet(uint txnLineID)
		{
			return invoiceLineGroupRets.FirstOrDefault(i => i.txnLineID == txnLineID);
		}

		// This element has its own child properties (There may be more than one, so making a list)
		[XmlElement(ElementName = "DataExtRet")]
		public List<DataExtRet> dataExtRets { get; set; }

		public DataExtRet GetDataExtRet(Guid ownerID)
		{
			return dataExtRets.FirstOrDefault(d => d.ownerID == ownerID);
		}
	}

	// Is child of the InvoiceRef element.
	public class CustomerRef
	{
		[XmlElement(ElementName = "ListID")]
		public uint listID { get; set; }

		[XmlElement(ElementName = "FullName")]
		public string fullName { get; set; }
	}

	// Is child of InvoiceRef, InvoiceLineItemRef.
	public class ClassRef
	{
		[XmlElement(ElementName = "ListID")]
		public uint listID { get; set; }

		[XmlElement(ElementName = "FullName")]
		public string fullName { get; set; }
	}

	// Is child of InvoiceRef, InvoiceLineGroupRet
	public class InvoiceLineRet
	{
		public InvoiceLineRet()
		{
			itemRef = new ItemRef();
			overrideUOMSetRef = new OverrideUOMSetRef();
			classRef = new ClassRef();
			dataExtRets = new List<DataExtRet>();
		}

		[XmlElement(ElementName = "TxnLineID")]
		public uint txnLineID { get; set; }

		[XmlElement(ElementName = "ItemRef")]
		public ItemRef itemRef { get; set; }

		[XmlElement(ElementName = "Desc")]
		public string desc { get; set; }

		// Making a double in case UnitOfMeasure can cause decimals for inventory quantity, like chemicals in a barrel for a car wash.
		[XmlElement(ElementName = "Quantity")]
		public double quantity { get; set; }

		[XmlElement(ElementName = "UnitOfMeasure")]
		public string unitOfMeasure { get; set; }

		[XmlElement(ElementName = "OverrideUOMSetRef")]
		public OverrideUOMSetRef overrideUOMSetRef { get; set; }

		[XmlElement(ElementName = "Rate")]
		public decimal rate { get; set; }

		[XmlElement(ElementName = "RatePercent")]
		public decimal ratePercent { get; set; }

		[XmlElement(ElementName = "ClassRef")]
		public ClassRef classRef { get; set; }

		[XmlElement(ElementName = "Amount")]
		public double amount { get; set; }

		[XmlElement(ElementName = "Other1")]
		public string other1 { get; set; }

		[XmlElement(ElementName = "Other2")]
		public string other2 { get; set; }

		[XmlElement(ElementName = "DataExtRet")]
		public List<DataExtRet> dataExtRets { get; set; }

		public DataExtRet GetDataExtRet(Guid ownerID)
		{
			return dataExtRets.FirstOrDefault(d => d.ownerID == ownerID);
		}
	}

	// Is child of InvoiceRef
	public class InvoiceLineGroupRet
	{
		public InvoiceLineGroupRet()
		{
			itemGroupRef = new ItemGroupRef();
			overrideUOMSetRef = new OverrideUOMSetRef();
			invoiceLineRets = new List<InvoiceLineRet>();
			dataExtRets = new List<DataExtRet>();
		}

		[XmlElement(ElementName = "TxnLineID")]
		public uint txnLineID { get; set; }

		[XmlElement(ElementName = "ItemGroupRef")]
		public ItemGroupRef itemGroupRef { get; set; }

		[XmlElement(ElementName = "Desc")]
		public string desc { get; set; }

		// Making a double in case UnitOfMeasure can cause decimals for inventory quantity, like chemicals in a barrel for a car wash.
		[XmlElement(ElementName = "Quantity")]
		public double quantity { get; set; }

		[XmlElement(ElementName = "UnitOfMeasure")]
		public string unitOfMeasure { get; set; }

		[XmlElement(ElementName = "OverrideUOMSetRef")]
		public OverrideUOMSetRef overrideUOMSetRef { get; set; }

		[XmlElement(ElementName = "IsPrintItemsInGroup")]
		public bool isPrintItemsInGroup { get; set; }

		[XmlElement(ElementName = "TotalAmount")]
		public double totalAmount { get; set; }

		[XmlElement(ElementName = "InvoiceLineRet")]
		public List<InvoiceLineRet> invoiceLineRets { get; set; }

		public InvoiceLineRet GetInvoiceLineRet(uint txnLineID)
		{
			return invoiceLineRets.FirstOrDefault(i => i.txnLineID == txnLineID);
		}

		[XmlElement(ElementName = "DataExtRet")]
		public List<DataExtRet> dataExtRets { get; set; }

		public DataExtRet GetDataExtRet(Guid ownerID)
		{
			return dataExtRets.FirstOrDefault(d => d.ownerID == ownerID);
		}
	}

	// The DataExtRet types
	public enum DataExtType { AMTTYPE = 0, DATETIMETYPE = 1, INTTYPE = 2, PERCENTTYPE = 3, PRICETYPE = 4, QUANTYPE = 5, STR1024TYPE = 6, STR255TYPE = 7 }

	// Is child of InvoiceRef, InvoiceLineRet, InvoiceLineGroupRet
	public class DataExtRet
	{
		[XmlElement(ElementName = "OwnerID")]
		public Guid ownerID { get; set; }

		[XmlElement(ElementName = "DataExtName")]
		public string dataExtName { get; set; }

		[XmlElement(ElementName = "DataExtType")]
		public DataExtType dataExtType { get; set; }

		[XmlElement(ElementName = "DataExtValue")]
		public string dataExtValue { get; set; }
	}

	public class ItemRef
	{
		[XmlElement(ElementName = "ListID")]
		public uint listID { get; set; }

		[XmlElement(ElementName = "FullName")]
		public string fullName { get; set; }
	}

	public class ItemGroupRef
	{
		[XmlElement(ElementName = "ListID")]
		public uint listID { get; set; }

		[XmlElement(ElementName = "FullName")]
		public string fullName { get; set; }
	}

	public class OverrideUOMSetRef
	{
		[XmlElement(ElementName = "ListID")]
		public uint listID { get; set; }

		[XmlElement(ElementName = "FullName")]
		public string fullName { get; set; }
	}
}
