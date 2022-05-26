using System.ComponentModel.DataAnnotations;

namespace AllSpice.Models
{
	public class Recipe
	{
		public int Id { get; set; }

		[Url]
		public string Picture { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Category { get; set; }
		public string CreatorId { get; set; }
		public Profile Creator { get; set; }
	}
}