using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Career.Models.Entities
{
	[Table("CVs")]
	public class CV
	{
		[Key]
		public int id { get; set; }

		[Required]
		public int experience { get; set; }

		[DataType(DataType.Text)]
		public string education { get; set; }

		[DataType(DataType.Text)]
		public string work_experience { get; set; }

		public User owner { get; set; }	
	}
}