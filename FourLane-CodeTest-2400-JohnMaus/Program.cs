using System;
using System.IO;

namespace FourLane_CodeTest_2400_JohnMaus
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Xml.Serialization.XmlSerializer xmlds = new System.Xml.Serialization.XmlSerializer(typeof(InvoiceQueryRs));

			using (StreamReader sr = new StreamReader(@".\Data\Invoice.xml"))
			{
				InvoiceQueryRs invoice = (InvoiceQueryRs)xmlds.Deserialize(sr);
				System.Xml.Serialization.XmlSerializer xmls = new System.Xml.Serialization.XmlSerializer(invoice.GetType());

				xmls.Serialize(Console.Out, invoice);
			}

			Console.ReadKey();
		}
	}
}
