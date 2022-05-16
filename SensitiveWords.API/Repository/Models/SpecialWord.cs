using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensitiveWords.API.Repository.Models;

public class SpecialWord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Word { get; set; }

    public static SpecialWord FromCsv(string csvLine)
    {
        var values = csvLine.Split(',');
        var word = new SpecialWord();
        word.Id = Convert.ToInt32(values[0]);
        word.Word = values[1];

        return word;
    }
}