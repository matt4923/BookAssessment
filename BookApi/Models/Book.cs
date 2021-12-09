using System;

namespace BookApi.Models {
	public class Book {
		public Guid ID { get; set; }
		public string Publisher { get; set; }
		public string Title { get; set; }
		public string AuthorLastName { get; set; }
		public string AuthorFirstName { get; set; }
		public decimal Price { get; set; }
		public int PublicationYear { get; set; } 
		public string Url { get; set; } 
		public string MLACitation => $"{AuthorLastName}, {AuthorFirstName}. \"{Title}\", {Publisher}, {PublicationYear}";
		public string ChicagoCitation => $"{AuthorLastName}, {AuthorFirstName}. {PublicationYear}. \"{Title}.\" {Url}";

	}
}
